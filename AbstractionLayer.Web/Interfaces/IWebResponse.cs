using System;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Interface to access WebResponse.
    /// </summary>
    public interface IWebResponse : IDisposable
    {
        /// <summary>
        ///  When overridden in a descendant class, gets or sets the content length of data being received.
        /// </summary>
        long ContentLength { get; set; }

        /// <summary>
        /// When overridden in a derived class, gets or sets the content type of the data being received.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// When overridden in a derived class, gets a collection of header name-value pairs associated with this request.
        /// </summary>
        System.Net.WebHeaderCollection Headers { get; }

        /// <summary>
        ///  When overridden in a derived class, gets the URI of the Internet resource that actually responded to the request.
        /// </summary>
        string ResponseUri { get; }

        /// <summary>
        /// When overridden by a descendant class, closes the response stream.
        /// </summary>
        void Close();

        /// <summary>
        ///  When overridden in a descendant class, returns the data stream from the Internet resource.
        /// </summary>
        /// <returns>An instance of the System.IO.Stream class for reading data from the Internet resource.</returns>
        System.IO.Stream GetResponseStream();
    }
}