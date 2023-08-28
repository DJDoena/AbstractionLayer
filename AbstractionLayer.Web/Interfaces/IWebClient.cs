namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Interface to access WebClient.
    /// </summary>
    public interface IWebClient
    {
        /// <summary>
        /// Gets or sets the network credentials that are sent to the host and used to authenticate the request.
        /// </summary>
        System.Net.ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets a collection of header name/value pairs associated with the request.
        /// </summary>
        System.Net.WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets the proxy used by this System.Net.WebClient object.
        /// </summary>
        System.Net.IWebProxy Proxy { get; set; }

        /// <summary>
        /// Gets a collection of header name/value pairs associated with the response.
        /// </summary>
        System.Net.WebHeaderCollection ResponseHeaders { get; }

        /// <summary>
        /// Gets or sets the base URI for requests made by a System.Net.WebClient.
        /// </summary>
        string BaseAddress { get; set; }

        /// <summary>
        /// Occurs when an asynchronous file download operation completes.
        /// </summary>
        event System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted;

        /// <summary>
        /// Occurs when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        event System.Net.DownloadProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        /// Downloads the resource as a System.Byte array from the URI specified.
        /// </summary>
        /// <param name="address">The URI from which to download data.</param>
        /// <returns>A <see cref="System.Byte"/>  array containing the downloaded resource.</returns>
        byte[] DownloadData(string address);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName"></param>
        void DownloadFile(string address, string fileName);
    }
}