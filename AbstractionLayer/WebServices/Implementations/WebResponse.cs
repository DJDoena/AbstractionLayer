namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    using System;
    using System.IO;

    internal sealed class WebResponse : IWebResponse
    {
        private System.Net.WebResponse Actual { get; }

        public WebResponse(System.Net.WebResponse actual)
        {
            Actual = actual;

            IsDisposed = false;
        }

        private Boolean IsDisposed { get; set; }

        #region IWebResponse

        public String ResponseUri
            => (Actual.ResponseUri.AbsoluteUri);

        public Stream GetResponseStream()
            => (Actual.GetResponseStream());

        public void Close()
        {
            Dispose();
        }

        #endregion

        #region IDisposable 

        public void Dispose()
        {
            if (IsDisposed == false)
            {
                Actual.Close();

                IsDisposed = true;
            }
        }

        #endregion
    }
}