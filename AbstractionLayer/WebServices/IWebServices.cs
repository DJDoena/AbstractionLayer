using System;
using System.Globalization;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    public interface IWebServices
    {
        IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci = null);
    }
}
