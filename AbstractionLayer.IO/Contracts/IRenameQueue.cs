using System;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// A mass renamer for files that ensures that no target file name conflicts occur.
/// </summary>
public interface IRenameQueue : IIOServiceItem
{
    /// <summary>
    /// Initializes the renaming queue.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Resets the renaming queue, clearing all pending rename operations.
    /// </summary>
    void Clear();

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">The source file to be renamed.</param>
    /// <param name="targetFileName">The target file name.</param>
    void Add(IFileInfo sourceFile
        , string targetFileName);

    /// <summary>
    /// Executes the actual renaming.
    /// </summary>
    /// <param name="rollbackBehaviour">Specifies what to do when a rename operation fails.</param>
    /// <param name="progress">Optional progress reporter to track the rename operation.</param>
    /// <returns>The result of the rename operation.</returns>
    IRenameResult Commit(RenameRollbackBehaviour rollbackBehaviour = RenameRollbackBehaviour.Automatic
        , IProgress<IRenameProgress> progress = null);
}