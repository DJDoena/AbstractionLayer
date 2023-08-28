using System;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary />
    public delegate bool CanExecuteDelegate();

    /// <summary>
    /// Base class for commands without parameters.
    /// </summary>
    public abstract class AbstractRelayCommand : System.Windows.Input.ICommand
    {
        private CanExecuteDelegate CanExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        protected AbstractRelayCommand(CanExecuteDelegate canExecuteCallback = null)
        {
            this.CanExecuteCallback = canExecuteCallback;
        }

        #region ICommand

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">not used</param>
        /// <returns>true if this command can be executed; otherwise, false</returns>
        public bool CanExecute(object parameter)
            => this.CanExecuteCallback == null || this.CanExecuteCallback();

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">not used</param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.CanExecuteCallback != null)
                {
                    System.Windows.Input.CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (this.CanExecuteCallback != null)
                {
                    System.Windows.Input.CommandManager.RequerySuggested -= value;
                }
            }
        }

        #endregion
    }
}