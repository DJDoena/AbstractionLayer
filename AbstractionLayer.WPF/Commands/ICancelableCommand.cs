using System.Threading;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary>
    /// Interface for commands that can be cancelled.
    /// </summary>
    public interface ICancelableCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Signals to a <see cref="CancellationToken"/> that it should be canceled.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }
    }
}