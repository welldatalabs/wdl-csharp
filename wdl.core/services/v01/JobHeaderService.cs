﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using wdl.common.models.v01;
using wdl.common.serialization;

namespace wdl.core.services.v01
{
    /// <summary>
    /// https://api.welldatalabs.com/docs/v1/jobheaders
    /// </summary>
    public class JobHeaderService
    {
        private HttpClient _client;
        private const string Endpiont = "jobheaders";

        /// <summary>
        /// Initialize JobHeaderService with api authorization token.
        /// </summary>
        /// <param name="authorizationToken">Authorization token provided by Well Data Labls.</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public JobHeaderService(string authorizationToken)
        {
            _client = HttpHelper.GetHttClient(authorizationToken);
        }

        /// <summary>
        /// Get job header data for a specific job id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id or well API Number</param>
        /// <returns>Collection of JobHeader objects.</returns>
        public IEnumerable<JobHeader> Get(string id)
        {
            try
            {
                return GetAsync(id).Result;
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
        /// <returns>Collection of JobHeader objects.</returns>
        public async Task<IEnumerable<JobHeader>> GetAsync(string id)
        {
            string requestUri = string.Format("{0}/{1}", Endpiont, id);
            var jobHeaderJson = await _client.GetStringAsync(requestUri);
            return jobHeaderJson.DeserializeJson<IEnumerable<JobHeader>>();
        }

        /// <summary>
        /// Get all job headers.
        /// </summary>
        /// <returns>Returns all job headers available for the api key.</returns>
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
            var jobHeaderJson = await _client.GetStringAsync(Endpiont);
            return jobHeaderJson.DeserializeJson<IEnumerable<JobHeader>>();
        }
    }
}