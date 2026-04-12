namespace DoenaSoft.AbstractionLayer.Implementations;

/// <summary>
/// Represents details of a single rename operation in a rename queue.
/// </summary>
internal sealed record RenameResultDetail : IRenameResultDetail
{
    /// <summary>
    /// Gets the original source file name.
    /// </summary>
    public string SourceFileName { get; }

    /// <summary>
    /// Gets the target file name that the source was renamed to (or attempted to be renamed to).
    /// </summary>
    public string TargetFileName { get; }

    /// <summary>
    /// Gets the error message if the rename failed, or null if successful.
    /// </summary>
    public string Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameResultDetail"/> class for a rename operation.
    /// </summary>
    /// <param name="sourceFile">The original source file name.</param>
    /// <param name="targetFile">The target file name.</param>
    /// <param name="error">The error message if the rename failed, or null if successful.</param>
    public RenameResultDetail(string sourceFile
        , string targetFile
        , string error)
    {
        this.SourceFileName = sourceFile;
        this.TargetFileName = targetFile;
        this.Error = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameResultDetail"/> class for a successful rename operation.
    /// </summary>
    /// <param name="sourceFile">The original source file name.</param>
    /// <param name="targetFile">The target file name.</param>
    public RenameResultDetail(string sourceFile
        , string targetFile)
        : this(sourceFile, targetFile, null)
    {
    }
}
