using System.Globalization;
using System.Threading.Tasks;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Standard implementation of <see cref="IWebRequest"/> for <see cref="System.Net.WebRequest"/>.
    /// </summary>
    internal sealed class WebRequest : IWebRequest
    {
        private readonly System.Net.WebRequest _actual;

        #region IWebRequest

        public WebRequest(string targetUrl, System.Net.IWebProxy proxy, CultureInfo ci)
        {
            _actual = System.Net.WebRequest.Create(targetUrl);

            if (ci != null)
            {
                _actual.Headers.Add("Accept-Language: " + ci.Name);
            }

            if (proxy != null)
            {
                _actual.Proxy = proxy;
            }
            else
            {
                _actual.Proxy = System.Net.WebRequest.GetSystemWebProxy();
                _actual.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            }
        }

        public async Task<IWebResponse> GetResponseAsync()
        {
            var actual = await _actual.GetResponseAsync().ConfigureAwait(false);

            var result = actual != null
                ? new WebResponse(actual)
                : null;

            return result;
        }

        #endregion
    }
}