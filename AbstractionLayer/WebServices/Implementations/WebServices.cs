namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    using System;
    using System.Globalization;

    public sealed class WebServices : IWebServices
    {
        public IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci)
            => (new WebRequest(targetUrl, ci));
    }
}