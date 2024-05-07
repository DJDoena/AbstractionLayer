using System;
using DoenaSoft.AbstractionLayer.UI.Contracts;

namespace DoenaSoft.AbstractionLayer.Threading
{
    /// <summary>
    /// Class to invoke methods from a secondary thread back onto the UI thread.
    /// </summary>
    public sealed class Synchronizer : ISynchronizer
    {
        private System.Windows.Threading.Dispatcher Dispatcher { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatcher">The dispatcher to invoke methods on the UI thread</param>
        public Synchronizer(System.Windows.Threading.Dispatcher dispatcher)
        {
            this.Dispatcher = dispatcher;
        }

        #region ISynchronizer

        /// <summary>
        /// Invokes a function on the UI thread.
        /// </summary>
        /// <param name="func">The function</param>
        public T Invoke<T>(Func<T> func)
            => this.Dispatcher.CheckAccess()
                ? func()
                : this.Dispatcher.Invoke(func);

        /// <summary>
        /// Invokes an action on the UI thread.
        /// </summary>
        /// <param name="action">The action</param>
        public void Invoke(Action action)
        {
            if (this.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                this.Dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// Executes the specified delegate asynchronously with the specified arguments on the thread that the Dispatcher was created on.
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>
        /// An object, which is returned immediately after <see cref="BeginInvoke(Action)"/> is called, that can be used to interact with the delegate as it is pending execution in the event queue.
        /// </returns>
        public IDispatcherOperation BeginInvoke(Action action)
        {
            var actual = this.Dispatcher.BeginInvoke(action);

            return new DispatcherOperation(actual);
        }

        #endregion
    }
}