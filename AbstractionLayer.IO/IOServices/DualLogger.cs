using System;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    /// <summary>
    /// Standard implementation of <see cref="ILogger"/> for dual logging to <see cref="Console"/> and a file.
    /// </summary>
    public sealed class DualLogger : ILogger
    {
        private readonly ILogger _consoleLogger;

        private readonly ILogger _fileLogger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">The file name to which to log</param>
        public DualLogger(string fileName)
        {
            _consoleLogger = new ConsoleLogger();

            _fileLogger = new FileLogger(fileName, null);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break and another empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, params object[] parameters)
        {
            _consoleLogger.WriteLine(message, parameters);

            _fileLogger.WriteLine(message, parameters);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, bool suppressFreeLine, params object[] parameters)
        {
            _consoleLogger.WriteLine(message, suppressFreeLine, parameters);

            _fileLogger.WriteLine(message, suppressFreeLine, parameters);
        }

        /// <summary>
        /// Reads user input.
        /// </summary>
        /// <returns>The user input</returns>
        public string ReadLine()
        {
            var input = _consoleLogger.ReadLine();

            _fileLogger.WriteLine($"Input: {input}");

            return input;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _consoleLogger.Dispose();

            _fileLogger.Dispose();
        }

        /// <summary>
        /// Write the message with its parameters with the expectation that a user input will soon be required.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLineForInput(string message, params object[] parameters)
        {
            _consoleLogger.WriteLineForInput(message, parameters);

            _fileLogger.WriteLineForInput(message, parameters);
        }
    }
}