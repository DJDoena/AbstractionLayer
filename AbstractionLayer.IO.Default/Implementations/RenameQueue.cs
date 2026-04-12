using DoenaSoft.AbstractionLayer.Implementations;
using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// A mass renamer for files that ensures that no target file name conflicts occur.
/// </summary>
public sealed class RenameQueue : IOServiceItem, IRenameQueue
{
    private readonly object _lock;

    private readonly ILogger _logger;

    private Dictionary<string, string> _renames;

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameQueue"/> class.
    /// </summary>
    /// <param name="ioServices">The IO services; will use <see cref="IOServices"/> when not set.</param>
    /// <param name="logger">Optional logger for tracking rename operations.</param>
    public RenameQueue(IIOServices ioServices = null
        , ILogger logger = null)
        : base(ioServices ?? new IOServices())
    {
        _logger = logger;

        _lock = new object();

        this.Initialize();
    }

    /// <summary>
    /// Initializes the renaming queue.
    /// </summary>
    public void Initialize()
    {
        lock (_lock)
        {
            if (_renames != null)
            {
                if (_renames.Count > 0)
                {
                    throw new InvalidOperationException("Rename was already started.");
                }
            }
            else
            {
                _renames = [];
            }
        }
    }

    /// <summary>
    /// Resets the renaming queue.
    /// </summary>
    public void Clear()
    {
        lock (_lock)
        {
            _renames = [];
        }
    }

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFileName">the source file name</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(string sourceFileName
        , string targetFileName)
    {
        if (string.IsNullOrWhiteSpace(sourceFileName))
        {
            throw new ArgumentException("Source file name cannot be null or empty.", nameof(sourceFileName));
        }

        if (string.IsNullOrWhiteSpace(targetFileName))
        {
            throw new ArgumentException("Target file name cannot be null or empty.", nameof(targetFileName));
        }

        this.Add(this.IOServices.GetFile(sourceFileName), targetFileName);
    }

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">the source file</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(SIO.FileInfo sourceFile
        , string targetFileName)
    {
        if (sourceFile == null)
        {
            throw new ArgumentNullException(nameof(sourceFile));
        }

        this.Add(this.IOServices.GetFile(sourceFile.FullName), targetFileName);
    }

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">the source file</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(IFileInfo sourceFile
        , string targetFileName)
    {
        if (sourceFile == null)
        {
            throw new ArgumentNullException(nameof(sourceFile));
        }

        if (string.IsNullOrWhiteSpace(targetFileName))
        {
            throw new ArgumentException("Target file name cannot be null or empty.", nameof(targetFileName));
        }

        this.EnsureInit();

        var sourceFileName = this.IOServices.Path.GetFullPath(sourceFile.FullName);

        targetFileName = this.IOServices.Path.GetFullPath(targetFileName);

        if (sourceFileName == targetFileName)
        {
            return;
        }

        if (this.IOServices.File.Exists(targetFileName))
        {
            throw new InvalidOperationException($"Target file '{targetFileName}' already exists on disk!");
        }

        lock (_lock)
        {
            if (_renames.ContainsKey(targetFileName))
            {
                throw new InvalidOperationException($"Target file '{targetFileName}' of source file '{sourceFileName}' is already target of source file '{_renames[targetFileName]}'");
            }

            _renames.Add(targetFileName, sourceFileName);
        }
    }

    /// <summary>
    /// Executes the actual renaming.
    /// </summary>
    /// <param name="rollbackBehaviour">Specifies what to do when a rename operation fails.</param>
    /// <param name="progress">Optional progress reporter to track the rename operation.</param>
    /// <returns>The result of the rename operation.</returns>
    public IRenameResult Commit(RenameRollbackBehaviour rollbackBehaviour = RenameRollbackBehaviour.Automatic
        , IProgress<IRenameProgress> progress = null)
    {
        lock (_lock)
        {
            this.EnsureInit();

            var count = _renames.Count;
            var completedRenames = new Stack<IRenameResultDetail>();
            var successfulRenames = new List<IRenameResultDetail>();
            var failedRenames = new List<IRenameResultDetail>();

            try
            {
                var index = 0;

                foreach (var kvp in _renames)
                {
                    var sourceFileName = kvp.Value;
                    var targetFileName = kvp.Key;

                    var sourceFile = this.IOServices.GetFile(sourceFileName);
                    var targetFile = this.IOServices.GetFile(targetFileName);

                    // Report progress before rename
                    progress?.Report(new RenameProgress(count, index, sourceFileName, targetFileName));

                    _logger?.WriteLine($@"{sourceFile.FolderName}\{sourceFile.Name} -> {targetFile.Name}");

                    try
                    {
                        sourceFile.MoveTo(targetFileName);

                        var detail = new RenameResultDetail(targetFileName, sourceFileName);

                        completedRenames.Push(detail);

                        successfulRenames.Add(detail);

                        this.IOServices.File.SetAttributes(targetFileName, SIO.FileAttributes.Archive);

                        index++;

                        // Report progress after rename
                        progress?.Report(new RenameProgress(count, index, null, null));
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = $"Rename operation failed for '{sourceFileName}' to '{targetFileName}': {ex.Message}";

                        _logger?.WriteLine($"Error renaming '{sourceFileName}' to '{targetFileName}': {ex.Message}");

                        failedRenames.Add(new RenameResultDetail(sourceFileName, targetFileName, ex.Message));

                        var rollbackResult = this.HandleRollback(rollbackBehaviour, completedRenames, successfulRenames, failedRenames, errorMessage, ex);

                        return rollbackResult;
                    }
                }

                return new RenameResult(successfulRenames);
            }
            finally
            {
                _renames = null;
            }
        }
    }

    private IRenameResult HandleRollback(RenameRollbackBehaviour rollbackBehaviour
        , Stack<IRenameResultDetail> completedRenames
        , List<IRenameResultDetail> successfulRenames
        , List<IRenameResultDetail> failedRenames
        , string errorMessage
        , Exception ex)
    {
        switch (rollbackBehaviour)
        {
            case RenameRollbackBehaviour.Automatic:
                {
                    // Automatically rollback and return result
                    var rollbackErrors = this.Rollback(completedRenames);

                    // For automatic rollback, move all successful renames to failed list
                    foreach (var successRename in successfulRenames)
                    {
                        failedRenames.Add(new RenameResultDetail(successRename.SourceFileName, successRename.TargetFileName, "Rolled back due to subsequent error"));
                    }

                    return new RenameResult(completedRenames.Count, errorMessage, rollbackErrors, [], failedRenames);
                }
            case RenameRollbackBehaviour.Manual:
                {
                    // Return result without rollback, caller must handle
                    _logger?.WriteLine($"Manual rollback mode: {completedRenames.Count} rename(s) completed before error. No automatic rollback performed.");

                    return new RenameResult(0, errorMessage, [], successfulRenames, failedRenames);
                }
            case RenameRollbackBehaviour.None:
                {
                    // Throw exception, leave files in current state
                    throw new InvalidOperationException(errorMessage, ex);
                }
            default:
                {
                    throw new InvalidOperationException($"Unknown rollback behaviour: {rollbackBehaviour}");
                }
        }
    }

    private List<string> Rollback(Stack<IRenameResultDetail> completedRenames)
    {
        _logger?.WriteLine($"Rolling back {completedRenames.Count} completed rename operation(s)...");

        var rollbackErrors = new List<string>();

        while (completedRenames.Count > 0)
        {
            var kvp = completedRenames.Pop();

            var sourceFileName = kvp.SourceFileName;
            var targetFileName = kvp.TargetFileName;

            try
            {
                _logger?.WriteLine($"Rollback: {targetFileName} -> {sourceFileName}");

                var target = this.IOServices.GetFile(targetFileName);

                target.MoveTo(sourceFileName);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error during rollback of '{targetFileName}' to '{sourceFileName}': {ex.Message}";

                _logger?.WriteLine(errorMessage);

                rollbackErrors.Add(errorMessage);
            }
        }

        if (rollbackErrors.Count > 0)
        {
            _logger?.WriteLine($"Warning: {rollbackErrors.Count} error(s) occurred during rollback. Some files may not have been restored.");
        }
        else
        {
            _logger?.WriteLine("Rollback completed successfully. All files restored to original state.");
        }

        return rollbackErrors;
    }

    private void EnsureInit()
    {
        lock (_lock)
        {
            if (_renames == null)
            {
                throw new InvalidOperationException("Rename was not started.");
            }
        }
    }
}