using System.Globalization;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Interface to access web services.
    /// </summary>
    public interface IWebServices
    {
        /// <summary>
        /// Creates a WebRequest.
        /// </summary>
        /// <param name="targetUrl">The URL</param>
        /// <param name="proxy">The proxy server</param>
        /// <param name="ci">The language in which the data is requested</param>
        /// <returns>The WebRequest</returns>
        IWebRequest CreateWebRequest(string targetUrl, System.Net.IWebProxy proxy = null, CultureInfo ci = null);

        /// <summary>
        /// Creates a WebClient
        /// </summary>
        /// <param name="proxy">The proxy server</param>
        /// <returns>The WebClient</returns>
        IWebClient CreateWebClient(System.Net.IWebProxy proxy = null);
    }
}