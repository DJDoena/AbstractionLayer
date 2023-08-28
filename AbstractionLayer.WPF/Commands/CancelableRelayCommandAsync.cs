using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary />
    public delegate void CancelableExecuteDelegate(CancellationToken cancellationToken);

    /// <summary>
    /// An asynchronous command which can be cancelled.
    /// </summary>
    public sealed class CancelableRelayCommandAsync : AbstractRelayCommand, ICancelableCommand
    {
        private CancelableExecuteDelegate ExecuteCallback { get; }

        /// <summary>
        /// Signals to a <see cref="CancellationToken"/> that it should be canceled.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public CancelableRelayCommandAsync(CancelableExecuteDelegate executeCallback
            , CanExecuteDelegate canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            this.ExecuteCallback = executeCallback ?? throw (new ArgumentNullException(nameof(executeCallback)));
        }

        #region ICommand

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public override void Execute(object parameter)
        {
            this.CancellationTokenSource = null;

            if (this.CanExecute(parameter))
            {
                this.CancellationTokenSource = new CancellationTokenSource();

                var ct = this.CancellationTokenSource.Token;

                var task = Task.Run(() => ExecuteCallback(ct), ct);

                task.ContinueWith(t => System.Windows.Input.CommandManager.InvalidateRequerySuggested(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        #endregion
    }
}