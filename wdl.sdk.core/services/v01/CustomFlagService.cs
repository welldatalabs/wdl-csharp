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
    /// https://api.welldatalabs.com/docs/v1/customflags
    /// </summary>
    public class CustomFlagService
    {
        private const string _endpoint = "customflags";
        private const string _version = "1";
        private string _apiKey;
        private Uri _baseUri;


        /// <summary>
        /// Initialize CustomFlagService with api authorization token.
        /// </summary>
        /// <param name="apiKey">API Key provided by Well Data Labls.</param>
        /// <param name="baseUri">Optional baseUri</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public CustomFlagService(string apiKey, string baseUri = null)
        {
            _apiKey = apiKey;
            _baseUri = string.IsNullOrWhiteSpace(baseUri) ? HttpHelper.BaseUri : new Uri(baseUri);
        }


        /// <summary>
        /// Get The first 1000 custom flags.
        /// </summary>
        /// <returns>Collection of CustomFlag objects.</returns>
        public IEnumerable<CustomFlag> GetAll()
        {
            try
            {
                return GetAllAsync().Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Get The first 1000 custom flags.
        /// </summary>
        /// <returns>Collection of CustomFlag objects.</returns>
        public async Task<IEnumerable<CustomFlag>> GetAllAsync()
        {
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint);
            var customFlagJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return customFlagJson.DeserializeJson<IEnumerable<CustomFlag>>();
        }


        /// <summary>
        /// Get custom flag data for a specific job id, well id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id, Well Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public IEnumerable<CustomFlag> Get(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
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
        /// Get custom flag data for a specific job id, well id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id, Well Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public async Task<IEnumerable<CustomFlag>> GetAsync(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, string.Format("{0}/{1}", _endpoint, id), parameters);
            var customFlagJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return customFlagJson.DeserializeJson<IEnumerable<CustomFlag>>();
        }


        /// <summary>
        /// Synchronous version of the method that gets custom flag data for flags that have changed 
        /// within a given UTC range.  If toChangeUtc is null, will return date for all flags 
        /// modified since fromChangeUtc.  If fromChangeUtc is null, will return data for all
        /// flags modified before toChangeUtc. 
        /// </summary>
        /// <param name="fromChangeUtc">From datetime in UTC format</param>
        /// <param name="toChangeUtc">To datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public IEnumerable<CustomFlag> GetByChangeUtc(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            try
            {
                return GetByChangeUtcAsync(fromChangeUtc, toChangeUtc, fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Asynchronous version of the method that gets custom flag data for flags that have changed 
        /// within a given UTC range.  If toChangeUtc is null, will return date for all flags 
        /// modified since fromChangeUtc.  If fromChangeUtc is null, will return data for all
        /// flags modified before toChangeUtc. 
        /// </summary>
        /// <param name="fromChangeUtc">From datetime in UTC format</param>
        /// <param name="toChangeUtc">To datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public async Task<IEnumerable<CustomFlag>> GetByChangeUtcAsync(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromChangeUtc", fromChangeUtc.ToString()},
                {"toChangeUtc", toChangeUtc.ToString()},
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
            var customFlagJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return customFlagJson.DeserializeJson<IEnumerable<CustomFlag>>();
        }


        /// <summary>
        /// Synchronous version of the method that gets custom flag data for flags within Stage Number 
        /// range.  If toStageNumber is null, will return date for all flags greater than or equal to 
        /// fromStageNumber.  If fromStageNumber is null, will return data for all flags less than or 
        /// equal to toStageNumber. 
        /// </summary>
        /// <param name="fromStageNumber">From stage number in decimal format</param>
        /// <param name="toStageNumber">To stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public IEnumerable<CustomFlag> GetByStageNumber(decimal? fromStageNumber, decimal? toStageNumber)
        {
            try
            {
                return GetByStageNumberAsync(fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Asynchronous version of the method that gets custom flag data for flags within Stage Number 
        /// range.  If toStageNumber is null, will return date for all flags greater than or equal to 
        /// fromStageNumber.  If fromStageNumber is null, will return data for all flags less than or 
        /// equal to toStageNumber. 
        /// </summary>
        /// <param name="fromStageNumber">From stage number in decimal format</param>
        /// <param name="toStageNumber">To stage number in decimal format</param>
        /// <returns>Collection of CustomFlag objects.</returns>
        public async Task<IEnumerable<CustomFlag>> GetByStageNumberAsync(decimal? fromStageNumber, decimal? toStageNumber)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
            var customFlagJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return customFlagJson.DeserializeJson<IEnumerable<CustomFlag>>();
        }

    }
}
