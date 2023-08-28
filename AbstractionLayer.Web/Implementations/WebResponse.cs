namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Standard implementation of <see cref="IWebResponse"/> for <see cref="System.Net.WebResponse"/>.
    /// </summary>
    internal sealed class WebResponse : IWebResponse
    {
        private readonly System.Net.WebResponse _actual;

        public WebResponse(System.Net.WebResponse actual)
        {
            _actual = actual;

            this.IsDisposed = false;
        }

        private bool IsDisposed { get; set; }

        #region IWebResponse

        public long ContentLength
        {
            get => _actual.ContentLength;
            set => _actual.ContentLength = value;
        }

        public string ContentType
        {
            get => _actual.ContentType;
            set => _actual.ContentType = value;
        }

        public System.Net.WebHeaderCollection Headers => _actual.Headers;

        public string ResponseUri
            => _actual.ResponseUri.AbsoluteUri;

        public void Close()
            => this.Dispose();

        public System.IO.Stream GetResponseStream()
            => _actual.GetResponseStream();

        #endregion

        #region IDisposable 

        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                _actual.Close();
                _actual.Dispose();

                this.IsDisposed = true;
            }
        }

        #endregion
    }
}