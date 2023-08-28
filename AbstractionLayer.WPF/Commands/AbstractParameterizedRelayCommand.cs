using System;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary />
    public delegate bool ParameterizedCanExecuteDelegate(object parameter);

    /// <summary>
    /// Base class for commands with parameters.
    /// </summary>
    public abstract class AbstractParameterizedRelayCommand : System.Windows.Input.ICommand
    {
        private ParameterizedCanExecuteDelegate CanExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        protected AbstractParameterizedRelayCommand(ParameterizedCanExecuteDelegate canExecuteCallback = null)
        {
            CanExecuteCallback = canExecuteCallback;
        }

        #region ICommand 

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null</param>
        /// <returns>true if this command can be executed; otherwise, false</returns>
        public Boolean CanExecute(object parameter)
            => this.CanExecuteCallback == null || this.CanExecuteCallback(parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null</param>
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