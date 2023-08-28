namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Standard implementation of <see cref="IWebClient"/> for <see cref="System.Net.WebClient"/>.
    /// </summary>
    internal sealed class WebClient : IWebClient
    {
        private readonly System.Net.WebClient _actual;

        internal WebClient()
        {
            _actual = new System.Net.WebClient();
        }

        #region IWebClient

        public System.Net.ICredentials Credentials
        {
            get => _actual.Credentials;
            set => _actual.Credentials = value;
        }


        public System.Net.WebHeaderCollection Headers
        {
            get => _actual.Headers;
            set => _actual.Headers = value;
        }

        public System.Net.IWebProxy Proxy
        {
            get => _actual.Proxy;
            set => _actual.Proxy = value;
        }

        public System.Net.WebHeaderCollection ResponseHeaders => _actual.ResponseHeaders;

        public string BaseAddress
        {
            get => _actual.BaseAddress;
            set => _actual.BaseAddress = value;
        }

        public event System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted
        {
            add => _actual.DownloadFileCompleted += value;
            remove => _actual.DownloadFileCompleted -= value;
        }

        public event System.Net.DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add => _actual.DownloadProgressChanged += value;
            remove => _actual.DownloadProgressChanged -= value;
        }

        public byte[] DownloadData(string address)
            => _actual.DownloadData(address);

        public void DownloadFile(string address, string fileName)
            => _actual.DownloadFile(address, fileName);

        #endregion
    }
}