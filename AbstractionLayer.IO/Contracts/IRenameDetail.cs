namespace DoenaSoft.AbstractionLayer;

/// <summary>
/// Represents details of a single rename operation in a rename queue.
/// </summary>
public interface IRenameResultDetail
{
    /// <summary>
    /// Gets the original source file name.
    /// </summary>
    string SourceFileName { get; }

    /// <summary>
    /// Gets the target file name that the source was renamed to (or attempted to be renamed to).
    /// </summary>
    string TargetFileName { get; }

    /// <summary>
    /// Gets the error message if the rename failed, or null if successful.
    /// </summary>
    string Error { get; }
}
