namespace DoenaSoft.AbstractionLayer.WebServices
{
    using System;

    /// <summary>
    /// Interface to access WebResponse.
    /// </summary>
    public interface IWebResponse : IDisposable
    {
        /// <summary>
        /// Returns the ResponseUri.
        /// </summary>
        string ResponseUri { get; }

        /// <summary>
        /// Returns the ResponseStream.
        /// </summary>
        /// <returns>The ResponseStream</returns>
        System.IO.Stream GetResponseStream();

        /// <summary>
        /// Closes the WebResponse.
        /// </summary>
        void Close();
    }
}