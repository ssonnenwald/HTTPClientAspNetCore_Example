using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApplication.HTTPClient
{
    /// <summary>
    /// The HTTP Client Caller.
    /// </summary>
    public static class ClientHTTPCaller
    {
        /// <summary>
        /// Send an HTTP POST to the Web API.
        /// </summary>
        /// <param name="httpClientAccessor">Instance of HTTPClient.</param>
        /// <param name="route">The route to the API method.</param>
        /// <param name="requestBody">The body of the request.</param>
        /// <returns>Returns the response from the request.</returns>
        public static string SendPost(IHttpClientAccessor httpClientAccessor, string route, string requestBody)
        {
            string Response = string.Empty;
            HttpResponseMessage responseMessage = null;

            try
            {
                // Setup the HTTP Client with the base address to use.
                HttpClient httpClient = httpClientAccessor.HttpClient;

                // Setup the request as a HTTP Post method.
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, route)
                {
                    // Add the HTTP Request body.
                    Content = new StringContent(requestBody, 
                        Encoding.UTF8, 
                        MediaTypeNames.Application.Json)
                };

                // Send the HTTP Client request.
                responseMessage = httpClient.SendAsync(request).Result;

                // Get the response from the HTTP Client request.
                Response = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                // Validate we get a response back from the bank.
                if (responseMessage.StatusCode == HttpStatusCode.OK && Response != null)
                {
                    // We have a good response so write it to our diagnostics log.
                    System.Diagnostics.Debug.WriteLine(Response);
                }
                else
                {
                    // The response status code for the request was not a 200 OK.
                    throw new HttpRequestException("Request Failed.  Error:  " + responseMessage.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                // Pass the exception back.
                throw;
            }

            // Return the response.
            return Response;
        }
    }
}
