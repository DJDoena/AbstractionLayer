using System.Collections.Generic;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Represents the result of a rename queue operation.
/// </summary>
internal sealed record RenameResult : IRenameResult
{
    /// <summary>
    /// The number of files successfully renamed.
    /// </summary>
    public int SuccessCount { get; }

    /// <summary>
    /// Indicates whether the operation completed successfully without errors.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Indicates whether a rollback occurred due to an error.
    /// </summary>
    public bool RolledBack { get; }

    /// <summary>
    /// The number of files that were rolled back.
    /// </summary>
    public int RolledBackCount { get; }

    /// <summary>
    /// The exception that caused the rollback, if any.
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// Gets a read-only list of any errors that occurred during rollback.
    /// </summary>
    public IReadOnlyList<string> RollbackErrors { get; }

    /// <summary>
    /// Gets a read-only list of successfully renamed files (source -> target).
    /// When RolledBack is true with Automatic behavior, this will be empty.
    /// </summary>
    public IReadOnlyList<IRenameResultDetail> SuccessfulRenames { get; }

    /// <summary>
    /// Gets a read-only list of files that failed to rename (source -> target -> error).
    /// When RolledBack is true with Automatic behavior, includes all attempted renames.
    /// </summary>
    public IReadOnlyList<IRenameResultDetail> FailedRenames { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameResult"/> class for a successful operation.
    /// </summary>
    /// <param name="successfulRenames">The list of successfully renamed files.</param>
    public RenameResult(IReadOnlyList<IRenameResultDetail> successfulRenames)
    {
        this.SuccessCount = successfulRenames.Count;
        this.Success = true;
        this.RolledBack = false;
        this.RolledBackCount = 0;
        this.ErrorMessage = null;
        this.RollbackErrors = [];
        this.SuccessfulRenames = successfulRenames;
        this.FailedRenames = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenameResult"/> class for a failed operation.
    /// </summary>
    /// <param name="rolledBackCount">The number of files that were rolled back.</param>
    /// <param name="errorMessage">The error message that caused the failure.</param>
    /// <param name="rollbackErrors">Any errors that occurred during rollback.</param>
    /// <param name="successfulRenames">Files that were successfully renamed (empty for automatic rollback).</param>
    /// <param name="failedRenames">Files that failed to rename.</param>
    public RenameResult(int rolledBackCount
        , string errorMessage
        , IReadOnlyList<string> rollbackErrors
        , IReadOnlyList<IRenameResultDetail> successfulRenames
        , IReadOnlyList<IRenameResultDetail> failedRenames)
    {
        this.SuccessCount = successfulRenames.Count;
        this.Success = false;
        this.RolledBack = rolledBackCount > 0;
        this.RolledBackCount = rolledBackCount;
        this.ErrorMessage = errorMessage;
        this.RollbackErrors = rollbackErrors ?? [];
        this.SuccessfulRenames = successfulRenames ?? [];
        this.FailedRenames = failedRenames ?? [];
    }
}