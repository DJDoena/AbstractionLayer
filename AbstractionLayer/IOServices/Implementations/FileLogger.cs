namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System.Text;

    /// <summary>
    /// Standard implementation of <see cref="ILogger"/> for a file.
    /// </summary>
    public sealed class FileLogger : ILogger
    {
        private readonly System.IO.StreamWriter _streamWriter;

        private readonly ILogger _inputLogger;

        private bool _isDisposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">The file name to which to log</param>
        /// <param name="inputLogger">A logger that is able to receive user input</param>
        public FileLogger(string fileName, ILogger inputLogger)
        {
            _streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.GetEncoding(1252));

            _inputLogger = inputLogger;

            _isDisposed = false;
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break and another empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, params object[] parameters)
        {
            this.WriteLine(message, false, parameters);
        }

        /// <summary>
        /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLine(string message, bool suppressFreeLine, params object[] parameters)
        {
            message = string.Format(message, parameters);

            _streamWriter.WriteLine(message);

            if (suppressFreeLine == false)
            {
                _streamWriter.WriteLine();
            }
        }

        /// <summary>
        /// Reads user input.
        /// The user input is requested from the <see cref="_inputLogger"/>.
        /// </summary>
        /// <returns>The user input</returns>
        public string ReadLine()
        {
            if (_inputLogger != null)
            {
                var input = _inputLogger.ReadLine();

                this.WriteLine($"Input: {input}");

                return input;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();

                _inputLogger?.Dispose();

                _isDisposed = true;
            }
        }

        /// <summary>
        /// Write the message with its parameters with the expectation that a user input will soon be required.
        /// The message is also forwarded to the <see cref="_inputLogger"/>.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The message parameter</param>
        public void WriteLineForInput(string message, params object[] parameters)
        {
            this.WriteLine(message, true, parameters);

            if (_inputLogger != null)
            {
                _inputLogger.WriteLineForInput(message, parameters);
            }
        }
    }
}