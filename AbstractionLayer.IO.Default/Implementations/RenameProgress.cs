namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Represents progress information for a rename queue operation.
/// </summary>
internal sealed record RenameProgress : IRenameProgress
{
    /// <summary>
    /// The total number of files to rename.
    /// </summary>
    public int Total { get; }

    /// <summary>
    /// The number of files that have been renamed so far.
    /// </summary>
    public int Completed { get; }

    /// <summary>
    /// The current source file being renamed.
    /// </summary>
    public string CurrentSourceFile { get; }

    /// <summary>
    /// The current target file name.
    /// </summary>
    public string CurrentTargetFile { get; }

    /// <summary>
    /// The percentage of completion (0-100).
    /// </summary>
    public double PercentComplete => this.Total > 0 ? (this.Completed * 100.0 / this.Total) : 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameProgress"/> class.
    /// </summary>
    /// <param name="total">The total number of files to rename.</param>
    /// <param name="completed">The number of files that have been renamed so far.</param>
    /// <param name="currentSourceFile">The current source file being renamed.</param>
    /// <param name="currentTargetFile">The current target file name.</param>
    public RenameProgress(int total
        , int completed
        , string currentSourceFile
        , string currentTargetFile)
    {
        this.Total = total;
        this.Completed = completed;
        this.CurrentSourceFile = currentSourceFile;
        this.CurrentTargetFile = currentTargetFile;
    }
}