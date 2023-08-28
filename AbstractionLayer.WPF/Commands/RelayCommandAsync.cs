using System.Threading.Tasks;
using System.Windows.Input;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary>
    /// An asynchronous command.
    /// </summary>
    public sealed class RelayCommandAsync : RelayCommand
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public RelayCommandAsync(ExecuteDelegate executeCallback
            , CanExecuteDelegate canExecuteCallback = null)
            : base(executeCallback, canExecuteCallback)
        { }

        #region ICommand

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var task = Task.Run(() => this.ExecuteCallback());

                task.ContinueWith(t => CommandManager.InvalidateRequerySuggested(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        #endregion
    }
}