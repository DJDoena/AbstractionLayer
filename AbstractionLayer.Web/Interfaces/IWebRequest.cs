using System.Threading.Tasks;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Interface to access WebRequest.
    /// </summary>
    public interface IWebRequest
    {
        /// <summary>
        /// Returns a WebResponse.
        /// </summary>
        /// <returns>The WebResponse</returns>
        Task<IWebResponse> GetResponseAsync();
    }
}