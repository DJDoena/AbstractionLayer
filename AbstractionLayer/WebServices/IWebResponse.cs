using System;
using System.IO;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    public interface IWebResponse : IDisposable
    {
        String ResponseUri { get; }

        Stream GetResponseStream();

        void Close();
    }
}