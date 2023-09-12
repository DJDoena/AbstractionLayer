using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    /// <summary>
    /// Standard implementation of <see cref="ILogger"/> for <see cref="Console"/>.
    /// </summary>
    public sealed class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Writes the message with its parameters and creates a line break and another empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, params object[] parameters)
            => this.WriteLine(message, false, parameters);

        /// <summary>
        /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, bool suppressFreeLine, params object[] parameters)
        {
            message = string.Format(message, parameters);

            Console.WriteLine(message);

            if (suppressFreeLine == false)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Reads user input.
        /// </summary>
        /// <returns>The user input</returns>
        public string ReadLine()
            => Console.ReadLine();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Write the message with its parameters with the expectation that a user input will soon be required.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLineForInput(string message, params object[] parameters)
            => this.WriteLine(message, true, parameters);
    }
}