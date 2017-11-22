namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;

    public sealed class ConsoleLogger : ILogger
    {
        public void WriteLine(String message
            , params Object[] parameters)
        {
            WriteLine(message, false, parameters);
        }

        public void WriteLine(String message
            , Boolean suppressFreeLine
            , params Object[] parameters)
        {
            message = String.Format(message, parameters);

            Console.WriteLine(message);

            if (suppressFreeLine == false)
            {
                Console.WriteLine();
            }
        }

        public String ReadLine()
        {
            String input;

            input = Console.ReadLine();

            return (input);
        }

        public void Dispose()
        { }

        public void WriteLineForInput(String message
            , params Object[] parameters)
        {
            WriteLine(message, true, parameters);
        }
    }
}