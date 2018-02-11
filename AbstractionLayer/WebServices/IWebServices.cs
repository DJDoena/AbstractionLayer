namespace DoenaSoft.AbstractionLayer.WebServices
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Interface to access web services.
    /// </summary>
    public interface IWebServices
    {
        /// <summary>
        /// Creates a WebRequest.
        /// </summary>
        /// <param name="targetUrl">The URL</param>
        /// <param name="ci">The language in which the data is requested</param>
        /// <returns>The WebRequest</returns>
        IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci = null);
    }
}