namespace DoenaSoft.AbstractionLayer.WebServices
{
    using System;
    using System.Globalization;

    public interface IWebServices
    {
        IWebRequest CreateWebRequest(String targetUrl
            , CultureInfo ci = null);
    }
}