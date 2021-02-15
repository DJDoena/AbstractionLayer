namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    using System.Globalization;

    /// <summary>
    /// Standard implementation of <see cref="IWebRequest"/> for <see cref="System.Net.WebRequest"/>.
    /// </summary>
    internal sealed class WebRequest : IWebRequest
    {
        private readonly System.Net.WebRequest _actual;

        #region IWebRequest

        public WebRequest(string targetUrl, CultureInfo ci)
        {
            _actual = System.Net.WebRequest.Create(targetUrl);

            if (ci != null)
            {
                _actual.Headers.Add("Accept-Language: " + ci.Name);
            }

            _actual.Proxy = System.Net.WebRequest.GetSystemWebProxy();

            _actual.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        }

        public IWebResponse GetResponse()
        {
            var actual = _actual.GetResponse();

            var result = actual != null
                ? new WebResponse(actual)
                : null;

            return result;
        }

        #endregion
    }
}