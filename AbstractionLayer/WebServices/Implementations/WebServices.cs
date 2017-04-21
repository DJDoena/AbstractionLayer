using System;
using System.Globalization;

namespace DoenaSoft.AbstractionLayer.WebServices.Implementations
{
    public sealed class WebServices : IWebServices
    {
        public IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci)
            => (new WebRequest(targetUrl, ci));
    }
}