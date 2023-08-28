﻿using System;

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
            Dispatcher = dispatcher;
        }

        #region ISynchronizer

        /// <summary>
        /// Invokes a function on the UI thread.
        /// </summary>
        /// <param name="func">The function</param>
        public T InvokeOnUIThread<T>(Func<T> func)
            => Dispatcher.CheckAccess() ? func() : Dispatcher.Invoke(func);

        /// <summary>
        /// Invokes an action on the UI thread.
        /// </summary>
        /// <param name="action">The action</param>
        public void InvokeOnUIThread(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Dispatcher.Invoke(action);
            }
        }

        #endregion
    }
}