using System.Collections.Generic;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Represents the result of a rename queue operation.
/// </summary>
public interface IRenameResult
{
    /// <summary>
    /// The number of files successfully renamed.
    /// </summary>
    int SuccessCount { get; }

    /// <summary>
    /// Indicates whether the operation completed successfully without errors.
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Indicates whether a rollback occurred due to an error.
    /// </summary>
    bool RolledBack { get; }

    /// <summary>
    /// The number of files that were rolled back.
    /// </summary>
    int RolledBackCount { get; }

    /// <summary>
    /// The exception that caused the rollback, if any.
    /// </summary>
    string ErrorMessage { get; }

    /// <summary>
    /// Gets a read-only list of any errors that occurred during rollback.
    /// </summary>
    IReadOnlyList<string> RollbackErrors { get; }

    /// <summary>
    /// Gets a read-only list of successfully renamed files (source -> target).
    /// When RolledBack is true with Automatic behavior, this will be empty.
    /// </summary>
    IReadOnlyList<IRenameResultDetail> SuccessfulRenames { get; }

    /// <summary>
    /// Gets a read-only list of files that failed to rename (source -> target -> error).
    /// When RolledBack is true with Automatic behavior, includes all attempted renames.
    /// </summary>
    IReadOnlyList<IRenameResultDetail> FailedRenames { get; }
}