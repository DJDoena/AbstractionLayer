namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface ILogger : IDisposable
    {
        void WriteLine(String message
            , params Object[] parameters);

        void WriteLine(String message
            , Boolean suppressFreeLine
            , params Object[] parameters);

        void WriteLineForInput(String message
            , params Object[] parameters);

        String ReadLine();
    }
}