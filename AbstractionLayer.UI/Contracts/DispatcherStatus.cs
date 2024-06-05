namespace DoenaSoft.AbstractionLayer.UI.Contracts
{
    /// <summary>
    /// Describes the possible values for the status of a <see cref="IDispatcherOperation"/>.
    /// </summary>
    public enum DispatcherStatus : byte
    {
        /// <summary>
        /// The operation is pending and is still in the Dispatcher queue.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The operation has aborted.
        /// </summary>
        Aborted = 1,

        /// <summary>
        /// The operation is completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The operation started executing, but has not completed.
        /// </summary>
        Executing = 3,
    }
}
