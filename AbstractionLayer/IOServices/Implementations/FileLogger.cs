namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Text;

    /// <summary>
    /// Standard implementation of <see cref="ILogger"/> for a file.
    /// </summary>
    public sealed class FileLogger : ILogger
    {
        private System.IO.StreamWriter StreamWriter { get; }

        private ILogger InputLogger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">The file name to which to log</param>
        /// <param name="inputLogger">A logger that is able to receive user input</param>
        public FileLogger(String fileName
            , ILogger inputLogger)
        {
            StreamWriter = new System.IO.StreamWriter(fileName, false, Encoding.GetEncoding(1252));

            InputLogger = inputLogger;
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break and another empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(String message
            , params Object[] parameters)
        {
            WriteLine(message, false, parameters);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(String message
            , Boolean suppressFreeLine
            , params object[] parameters)
        {
            message = String.Format(message, parameters);

            StreamWriter.WriteLine(message);

            if (suppressFreeLine == false)
            {
                StreamWriter.WriteLine();
            }
        }

        /// <summary>
        /// Reads user input.
        /// The user input is requested from the <see cref="InputLogger"/>.
        /// </summary>
        /// <returns>The user input</returns>
        public String ReadLine()
        {
            if (InputLogger != null)
            {
                String input;

                input = InputLogger.ReadLine();

                WriteLine($"Input: {input}");

                return (input);
            }

            return (null);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            StreamWriter.Close();
            StreamWriter.Dispose();

            if (InputLogger != null)
            {
                InputLogger.Dispose();
            }
        }

        /// <summary>
        /// Write the message with its parameters with the expectation that a user input will soon be required.
        /// The message is also forwarded to the <see cref="InputLogger"/>.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLineForInput(String message
            , params Object[] parameters)
        {
            WriteLine(message, true, parameters);

            if (InputLogger != null)
            {
                InputLogger.WriteLineForInput(message, parameters);
            }
        }
    }
}