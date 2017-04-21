using System;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    public sealed class DualLogger : ILogger
    {
        private readonly ILogger ConsoleLogger;

        private readonly ILogger FileLogger;

        public DualLogger(String fileName)
        {
            ConsoleLogger = new ConsoleLogger();

            FileLogger = new FileLogger(fileName, null);
        }

        public void WriteLine(String message
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLine(message, parameters);

            FileLogger.WriteLine(message, parameters);
        }

        public void WriteLine(String message
            , Boolean suppressFreeLine
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLine(message, suppressFreeLine, parameters);

            FileLogger.WriteLine(message, suppressFreeLine, parameters);
        }

        public String ReadLine()
        {
            String input = ConsoleLogger.ReadLine();

            FileLogger.WriteLine($"Input: {input}");

            return (input);
        }

        public void Dispose()
        {
            ConsoleLogger.Dispose();

            FileLogger.Dispose();
        }

        public void WriteLineForInput(String message
            , params Object[] parameters)
        {
            ConsoleLogger.WriteLineForInput(message, parameters);

            FileLogger.WriteLineForInput(message, parameters);
        }
    }
}