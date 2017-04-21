using System;
using System.Globalization;

namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    internal sealed class WebRequest : IWebRequest
    {
        private readonly System.Net.WebRequest Actual;

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