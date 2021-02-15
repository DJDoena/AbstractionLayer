namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
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

            IsDisposed = false;
        }

        private bool IsDisposed { get; set; }

        #region IWebResponse

        public string ResponseUri => _actual.ResponseUri.AbsoluteUri;

        public System.IO.Stream GetResponseStream() => _actual.GetResponseStream();

        public void Close()
        {
            Dispose();
        }

        #endregion

        #region IDisposable 

        public void Dispose()
        {
            if (!IsDisposed)
            {
                _actual.Close();
                _actual.Dispose();

                IsDisposed = true;
            }
        }

        #endregion
    }
}