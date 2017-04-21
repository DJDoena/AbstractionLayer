using System;
using System.Text;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    public sealed class FileLogger : ILogger
    {
        private readonly System.IO.StreamWriter StreamWriter;

        private readonly ILogger InputLogger;

        public FileLogger(String fileName
            , ILogger inputLogger)
        {
            StreamWriter = new System.IO.StreamWriter(fileName, false, Encoding.GetEncoding(1252));

            InputLogger = inputLogger;
        }

        public void WriteLine(String message
            , params Object[] parameters)
        {
            WriteLine(message, false, parameters);
        }

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

        public void Dispose()
        {
            StreamWriter.Close();
            StreamWriter.Dispose();

            if (InputLogger != null)
            {
                InputLogger.Dispose();
            }
        }

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