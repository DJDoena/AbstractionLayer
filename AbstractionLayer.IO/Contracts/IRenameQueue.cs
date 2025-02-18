namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// A mass renamer for files that ensures that no target file name conflicts occur.
/// </summary>
public interface IRenameQueue
{
    /// <summary>
    /// The master interface.
    /// </summary>
    IIOServices IOServices { get; }

    /// <summary>
    /// Initializes the renaming queue.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Adds a file to the queue and ensures that the target file does not exist on disc nor is the target file name of a previous rename.
    /// </summary>
    /// <param name="sourceFile">the source file</param>
    /// <param name="targetFileName">the target file name</param>
    void Add(IFileInfo sourceFile, string targetFileName);

    /// <summary>
    /// Executes the actual renaming.
    /// </summary>
    ulong Commit();
}