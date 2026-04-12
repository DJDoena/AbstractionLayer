namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Represents progress information for a rename queue operation.
/// </summary>
public interface IRenameProgress
{
    /// <summary>
    /// The total number of files to rename.
    /// </summary>
    int Total { get; }

    /// <summary>
    /// The number of files that have been renamed so far.
    /// </summary>
    int Completed { get; }

    /// <summary>
    /// The current source file being renamed.
    /// </summary>
    string CurrentSourceFile { get; }

    /// <summary>
    /// The current target file name.
    /// </summary>
    string CurrentTargetFile { get; }

    /// <summary>
    /// The percentage of completion (0-100).
    /// </summary>
    double PercentComplete { get; }
}
