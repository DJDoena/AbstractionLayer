namespace DoenaSoft.AbstractionLayer;

/// <summary>
/// Specifies the rollback behavior when a rename operation fails.
/// </summary>
public enum RenameRollbackBehaviour
{
    /// <summary>
    /// Automatically rollback all completed renames when an error occurs and return a result with error information.
    /// </summary>
    Automatic,

    /// <summary>
    /// Do not automatically rollback. The caller is responsible for handling the partial state.
    /// </summary>
    Manual,

    /// <summary>
    /// Do not rollback on error. Leave files in their current state and throw an exception.
    /// </summary>
    None,
}
