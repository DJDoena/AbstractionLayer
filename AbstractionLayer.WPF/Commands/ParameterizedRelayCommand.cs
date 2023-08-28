using System;

namespace DoenaSoft.AbstractionLayer.Commands
{
    /// <summary />
    public delegate void ParameterizedExecuteDelegate(object parameter);

    /// <summary>
    /// A command which can have a command parameter.
    /// </summary>
    public sealed class ParameterizedRelayCommand : AbstractParameterizedRelayCommand
    {
        private ParameterizedExecuteDelegate ExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public ParameterizedRelayCommand(ParameterizedExecuteDelegate executeCallback
            , ParameterizedCanExecuteDelegate canExecuteCallback = null)
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
                this.ExecuteCallback(parameter);
            }
        }

        #endregion     
    }
}