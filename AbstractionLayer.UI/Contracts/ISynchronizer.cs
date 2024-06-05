using System;

namespace DoenaSoft.AbstractionLayer.UI.Contracts
{
    /// <summary>
    /// Interface to invoke methods from a secondary thread back onto the UI thread.
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        /// Invokes an action on the UI thread.
        /// </summary>
        /// <param name="action">The action</param>
        void Invoke(Action action);

        /// <summary>
        /// Invokes a function on the UI thread.
        /// </summary>
        /// <param name="func">The function</param>
        T Invoke<T>(Func<T> func);

        /// <summary>
        /// Executes the specified delegate asynchronously with the specified arguments on the thread that the Dispatcher was created on.
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>
        /// An object, which is returned immediately after <see cref="BeginInvoke(Action)"/> is called, that can be used to interact with the delegate as it is pending execution in the event queue.
        /// </returns>
        IDispatcherOperation BeginInvoke(Action action);
    }
}