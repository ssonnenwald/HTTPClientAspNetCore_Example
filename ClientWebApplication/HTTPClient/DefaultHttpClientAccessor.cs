using System.Net.Http;

namespace ClientWebApplication.HTTPClient
{
    /// <summary>
    /// The HTTP Client.
    /// </summary>
    public class DefaultHttpClientAccessor : IHttpClientAccessor
    {
        public HttpClient HttpClient { get; }

        // Constructor
        public DefaultHttpClientAccessor()
        {
            HttpClient = new HttpClient();
        }
    }

    /// <summary>
    /// The interface for the HTTP Client.
    /// </summary>
    public interface IHttpClientAccessor
    {
        HttpClient HttpClient { get; }
    }
}
