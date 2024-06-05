using System.Threading.Tasks;

namespace DoenaSoft.AbstractionLayer.UI.Contracts
{
    /// <summary>
    /// Represents an object that is used to interact with an operation that has been posted to the Dispatcher queue.
    /// </summary>
    public interface IDispatcherOperation
    {
        /// <summary>
        /// Gets the result of the operation after it has completed.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Gets the current status of the operation.
        /// </summary>
        DispatcherStatus Status { get; }

        /// <summary>
        /// Gets a <see cref="System.Threading.Tasks.Task"/> that represents the current operation.
        /// </summary>
        Task Task { get; }
    }
}