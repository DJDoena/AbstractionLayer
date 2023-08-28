using System.Globalization;
using System.Net;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Standard implementation of <see cref="IWebServices"/>.
    /// </summary>
    public sealed class WebServices : IWebServices
    {
        /// <summary>
        /// Creates a WebRequest.
        /// </summary>
        /// <param name="targetUrl">The URL</param>
        /// <param name="proxy">The proxy server</param>
        /// <param name="ci">The language in which the data is requested</param>
        /// <returns>The WebRequest</returns>
        public IWebRequest CreateWebRequest(string targetUrl, IWebProxy proxy = null, CultureInfo ci = null)
            => new WebRequest(targetUrl, proxy, ci);

        /// <summary>
        /// Creates a WebClient
        /// </summary>
        /// <param name="proxy">The proxy server</param>
        /// <returns>The WebClient</returns>
        public IWebClient CreateWebClient(IWebProxy proxy = null)
        {
            throw new System.NotImplementedException();
        }
    }
}