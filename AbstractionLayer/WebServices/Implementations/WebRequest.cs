namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Standard implementation of <see cref="IWebRequest"/> for <see cref="System.Net.WebRequest"/>.
    /// </summary>
    internal sealed class WebRequest : IWebRequest
    {
        private System.Net.WebRequest Actual { get; }

        #region IWebRequest

        public WebRequest(String targetUrl
            , CultureInfo ci)
        {
            Actual = System.Net.WebRequest.Create(targetUrl);

            if (ci != null)
            {
                Actual.Headers.Add("Accept-Language: " + ci.Name);
            }

            Actual.Proxy = System.Net.WebRequest.GetSystemWebProxy();

            Actual.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        }

        public IWebResponse GetResponse()
        {
            System.Net.WebResponse actual = Actual.GetResponse();

            return ((actual != null) ? (new WebResponse(actual)) : null);
        }

        #endregion
    }
}