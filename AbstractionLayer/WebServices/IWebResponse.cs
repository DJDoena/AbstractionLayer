namespace DoenaSoft.AbstractionLayer.WebServices
{
    using System;
    using System.IO;

    /// <summary>
    /// Interface to access WebResponse.
    /// </summary>
    public interface IWebResponse : IDisposable
    {
        /// <summary>
        /// Returns the ResponseUri.
        /// </summary>
        String ResponseUri { get; }

        /// <summary>
        /// Returns the ResponseStream.
        /// </summary>
        /// <returns>The ResponseStream</returns>
        Stream GetResponseStream();

        /// <summary>
        /// Closes the WebResponse.
        /// </summary>
        void Close();
    }
}