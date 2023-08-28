using System;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary>
    /// Basic command.
    /// </summary>
    public class RelayCommand : AbstractRelayCommand
    {
        /// <summary />
        public delegate void ExecuteDelegate();

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        protected ExecuteDelegate ExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public RelayCommand(ExecuteDelegate executeCallback
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
            if (this.CanExecute(parameter))
            {
                this.ExecuteCallback();
            }
        }

        #endregion
    }
}