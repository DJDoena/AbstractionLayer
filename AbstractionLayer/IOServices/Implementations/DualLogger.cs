namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;

    /// <summary>
    /// Standard implementation of <see cref="ILogger"/> for dual logging to <see cref="Console"/> and a file.
    /// </summary>
    public sealed class DualLogger : ILogger
    {
        private ILogger ConsoleLogger { get; }

        private ILogger FileLogger { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">The file name to which to log</param>
        public DualLogger(String fileName)
        {
            ConsoleLogger = new ConsoleLogger();

            FileLogger = new FileLogger(fileName, null);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break and another empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(String message
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLine(message, parameters);

            FileLogger.WriteLine(message, parameters);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(String message
            , Boolean suppressFreeLine
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLine(message, suppressFreeLine, parameters);

            FileLogger.WriteLine(message, suppressFreeLine, parameters);
        }

        /// <summary>
        /// Reads user input.
        /// </summary>
        /// <returns>The user input</returns>
        public String ReadLine()
        {
            String input = ConsoleLogger.ReadLine();

            FileLogger.WriteLine($"Input: {input}");

            return (input);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ConsoleLogger.Dispose();

            FileLogger.Dispose();
        }

        /// <summary>
        /// Write the message with its parameters with the expectation that a user input will soon be required.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLineForInput(String message
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLineForInput(message, parameters);

            FileLogger.WriteLineForInput(message, parameters);
        }
    }
}