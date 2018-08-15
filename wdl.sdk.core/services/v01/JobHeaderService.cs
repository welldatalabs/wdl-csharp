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
    public class JobHeaderService
    {
        private const string _endpoint = "jobheaders";
        private const string _version = "1";
        private string _apiKey;
        private Uri _baseUri;
        

        /// <summary>
        /// Initialize JobHeaderService with api authorization token.
        /// </summary>
        /// <param name="apiKey">API Key provided by Well Data Labls.</param>
        /// <param name="baseUri">Optional baseUri</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public JobHeaderService(string apiKey, string baseUri = null)
        {
            _apiKey = apiKey;
            _baseUri = string.IsNullOrWhiteSpace(baseUri) ? HttpHelper.BaseUri : new Uri(baseUri);
        }


        /// <summary>
        /// Get all job headers.
        /// </summary>
        /// <returns>Collection of all JobHeader objects available for the api key.</returns>
        public IEnumerable<JobHeader> GetAll()
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
        /// Get all job headers.
        /// </summary>
        /// <returns>Returns all job headers available for the api key.</returns>
        public async Task<IEnumerable<JobHeader>> GetAllAsync()
        {
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint);
            var jobHeaderJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return jobHeaderJson.DeserializeJson<IEnumerable<JobHeader>>();
        }


        /// <summary>
        /// Get job header data for a specific job id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of JobHeader objects.</returns>
        public IEnumerable<JobHeader> Get(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
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
        /// Get job header data for a specific job id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of JobHeader objects.</returns>
        public async Task<IEnumerable<JobHeader>> GetAsync(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, string.Format("{0}/{1}", _endpoint, id), parameters);
            var jobHeaderJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return jobHeaderJson.DeserializeJson<IEnumerable<JobHeader>>();
        }





        /// <summary>
        /// Synchronous version of the method that gets job headers for jobs that have changed 
		/// within a given UTC range.  If toChangeUtc is null, will return headers for all jobs 
		/// modified since fromChangeUtc.  If fromChangeUtc is null, will return headers for all
		/// jobs modified before toChangeUtc.  
        /// </summary>
        /// <param name="fromChangeUtc">Optional from datetime in UTC format</param>
        /// <param name="toChangeUtc">Optional to datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of JobHeader objects.</returns>
        public IEnumerable<JobHeader> GetByChangeUtc(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
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
        /// Asynchronous version of the method that gets job headers for jobs that have changed 
        /// within a given UTC range.  If toChangeUtc is null, will return headers for all jobs 
        /// modified since fromChangeUtc.  If fromChangeUtc is null, will return headers for all
        /// jobs modified before toChangeUtc. 
        /// </summary>
        /// <param name="fromChangeUtc">Optional from datetime in UTC format</param>
        /// <param name="toChangeUtc">Optional to datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of JobHeader objects.</returns>
        public async Task<IEnumerable<JobHeader>> GetByChangeUtcAsync(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromChangeUtc", fromChangeUtc.ToString()},
                {"toChangeUtc", toChangeUtc.ToString()},
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
            var jobHeaderJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return jobHeaderJson.DeserializeJson<IEnumerable<JobHeader>>();
        }
    
	}
}
