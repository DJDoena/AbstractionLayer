namespace DoenaSoft.AbstractionLayer.WebServices
{
    using System;
    using System.IO;

    public interface IWebResponse : IDisposable
    {
        String ResponseUri { get; }

        Stream GetResponseStream();

        void Close();
    }
}