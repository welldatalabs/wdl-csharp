using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace wdl.sdk.core
{
    public static class HttpHelper
    {
        internal static HttpClient _client = GetNewHttpClient();

        public static Uri BaseUri = new Uri(@"https://api.welldatalabs.com");

        /// <summary>
        /// HttpClient is designed to be re-used across requests.  HttpClient should not be disposed or wrapped in a using statement.
        /// This prevents HttpClient from opening multiple sockets on the client machine.
        /// Creating a new client for each request has a lot of overhead and causes a decrease in performance.
        /// Setting ConnectionClose = true will set the keep-alive header to false and close the connection after each request.  This will have a small
        /// performance impact but plays much nicer with load balancers.
        /// </summary>
        private static HttpClient GetNewHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.ConnectionClose = false;    //this will set http-keepalive header to true
            client.Timeout = new TimeSpan(0, 0, 60);
            return client;
        }


        /// <summary>
        /// Give our services ability to execute HttpClient.GetStringAsync using our static HttpClient
        /// </summary>
        public static async Task<string> GetAsync(string uri, string apiKey, string version)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            _client.DefaultRequestHeaders.Add("api-version", version);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();  // Throw if not a success code.
                var jsonString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                    string errorMessage;
                    error.TryGetValue("message", out errorMessage);
                    throw new Exception(errorMessage ?? "API returned a status code of " + response.StatusCode); 
                }
                return jsonString;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Helper to build requestUri for each server endpoint and include optional querystring parameters
        /// </summary>
        public static string GetRequestUri(Uri baseUri, string endPoint, Dictionary<string, string> parameters = null)
        {
            // Build QueryString if parameters are available
            var queryString = parameters != null ? parameters.ToQueryString() : string.Empty;

            // Return Uri formatted to include base, endpoint and optional querystring
            var requestUri = string.Format("{0}{1}{2}", baseUri, endPoint, queryString);

            return requestUri;
        }


        /// <summary>
        /// Used by GetRequestUri to build a querystring from our service endpoint parameters.  
        /// </summary>
        private static string ToQueryString(this Dictionary<string,string> parameters)
        {
            if (parameters == null) return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (string key in parameters.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                string value;
                if (!parameters.TryGetValue(key, out value))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(value)) continue;

                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
            }

            return sb.ToString();
        }

        /// <summary>
        /// This asynchronously returns a stream of the data that you can read from. Used by the PerSecData api.
        /// NOTE: Like all streams it is CRITICAL that you close the stream. If left open, it will count against your number of concurrent connections.
        /// See PerSecDataService for example usage.
        /// </summary>
        /// <returns></returns>
        public static async Task<Stream> GetStreamAsync(string uri, string apiKey, string version)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _client.DefaultRequestHeaders.Add("api-version", version);

            try {
                return await _client.GetStreamAsync(uri);
            } catch (Exception ex) {
                return await Task.FromResult(new MemoryStream($"ERROR: {ex}".Select(x => (byte)x).ToArray()));
            }
        }
    }
}
