using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace wdl.core
{
    public static class HttpHelper
    {
        public static Uri BaseUri = new Uri(@"https://api.welldatalabs.com");

        /// <summary>
        /// HttpClient is designed to be re-used across requests.  HttpClient should not be disposed or wrapping with a using stagement.  This prevents
        /// HttpClient from opeing multiple sockiets on the client machine.
        /// Creating a new client for each request has a lot of overhead and causes a decrease in performance.
        /// Setting ConnectionClose = true will set the keep-alive header to false and close the connection after each request.  This will have a small
        /// performance impact but plays much nicer with load balancers.
        /// </summary>
        public static HttpClient GetHttClient(string apiKey, TimeSpan? timeout = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = BaseUri;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.ConnectionClose = true;    //this will set http-keepalive header to false
            client.Timeout = timeout ?? new TimeSpan(0, 0, 30);
            return client;
        }
    }
}
