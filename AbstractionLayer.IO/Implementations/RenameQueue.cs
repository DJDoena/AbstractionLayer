﻿using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// A mass renamer for files that ensures that no target file name conflicts occur.
/// </summary>
public sealed class RenameQueue : IRenameQueue
{
    private readonly object _lock;

    private Dictionary<string, string> _renames;

    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    /// <summary />
    /// <param name="ioServices">the IO services; will use <see cref="IOServices"/> when not set</param>
    public RenameQueue(IIOServices ioServices = null)
    {
        this.IOServices = ioServices ?? new IOServices();

        _lock = new object();
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
                throw new InvalidOperationException("Rename was already started.");
            }

            _renames = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFileName">the source file name</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(string sourceFileName, string targetFileName)
        => this.Add(this.IOServices.GetFile(sourceFileName), targetFileName);

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">the source file</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(SIO.FileInfo sourceFile, string targetFileName)
        => this.Add(this.IOServices.GetFile(sourceFile.FullName), targetFileName);

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">the source file</param>
    /// <param name="targetFileName">the target file name</param>
    public void Add(IFileInfo sourceFile, string targetFileName)
    {
        this.EnsureInit();

        var sourceFileName = this.IOServices.Path.GetFullPath(sourceFile.FullName);

        targetFileName = this.IOServices.Path.GetFullPath(targetFileName);

        if (sourceFileName == targetFileName)
        {
            return;
        }

        if (this.IOServices.File.Exists(targetFileName))
        {
            throw new Exception($"Target file '{targetFileName}' already exists on disk!");
        }

        lock (_lock)
        {
            try
            {
                _renames.Add(targetFileName, sourceFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Target file '{targetFileName}' of source file '{sourceFileName}' is already target of source file '{_renames[targetFileName]}'", ex);
            }
        }
    }

    /// <summary>
    /// Executes the actual renaming.
    /// </summary>
    public ulong Commit()
    {
        lock (_lock)
        {
            this.EnsureInit();

            try
            {
                foreach (var kvp in _renames)
                {
                    var sourceFile = this.IOServices.GetFile(kvp.Value);

                    var targetFile = this.IOServices.GetFile(kvp.Key);

                    Console.WriteLine($@"{sourceFile.FolderName}\{sourceFile.Name} -> {targetFile.Name}");

                    sourceFile.MoveTo(targetFile.FullName);

                    this.IOServices.File.SetAttributes(targetFile.FullName, SIO.FileAttributes.Archive);
                }

                return (ulong)_renames.Count;
            }
            finally
            {
                _renames = null;
            }
        }
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