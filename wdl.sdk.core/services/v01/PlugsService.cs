using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using wdl.sdk.common.models.v01;
using wdl.sdk.common.serialization;

namespace wdl.sdk.core.services.v01
{
    /// <summary>
    /// https://api.welldatalabs.com/docs/v1/jobheaders
    /// </summary>
    public class PlugsService
    {
        private const string _endpoint = "plugs";
        private const string _version = "1";
        private string _apiKey;
        private Uri _baseUri;


        /// <summary>
        /// Initialize PlugService with api authorization token.
        /// </summary>
        /// <param name="apiKey">API Key provided by Well Data Labls.</param>
        /// <param name="baseUri">Optional baseUri</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public PlugsService(string apiKey, string baseUri = null)
        {
            _apiKey = apiKey;
            _baseUri = string.IsNullOrWhiteSpace(baseUri) ? HttpHelper.BaseUri : new Uri(baseUri);
        }


        /// <summary>
        /// Get plug data for a specific job id.  
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <param name="fromStageNumber">Optional From Stage Number filter</param>
        /// <param name="toStageNumber">Optional To Stage Number filter</param>/// 
        /// <returns>Collection of Plug objects.</returns>
        public IEnumerable<Plug> Get(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            try
            {
                return GetAsync(id, fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Get plug data for a specific job id.  
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <param name="fromStageNumber">Optional From Stage Number filter</param>
        /// <param name="toStageNumber">Optional To Stage Number filter</param>/// 
        /// <returns>Collection of Plug objects.</returns>
        public async Task<IEnumerable<Plug>> GetAsync(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, string.Format("{0}/{1}", _endpoint, id), parameters);
            var plugJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return plugJson.DeserializeJson<IEnumerable<Plug>>();
        }

	}
}
