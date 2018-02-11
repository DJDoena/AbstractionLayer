namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Standard implementation of <see cref="IWebServices"/>.
    /// </summary>
    public sealed class WebServices : IWebServices
    {
        /// <summary>
        /// Creates a WebRequest.
        /// </summary>
        /// <param name="targetUrl">The URL</param>
        /// <param name="ci">The language in which the data is requested</param>
        /// <returns>The WebRequest</returns>
        public IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci)
            => new WebRequest(targetUrl, ci);
    }
}