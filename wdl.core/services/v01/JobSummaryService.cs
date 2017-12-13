using System;
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
    /// https://api.welldatalabs.com/docs/v1/jobsummaries
    /// </summary>
    public class JobSummaryService
    {
        private HttpClient _client;
        private const string Endpiont = "jobsummaries";

        /// <summary>
        /// Initialize JobSummaryService with api authorization token.
        /// </summary>
        /// <param name="authorizationToken">Authorization token provided by Well Data Labls.</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public JobSummaryService(string authorizationToken)
        {
            _client = HttpHelper.GetHttClient(authorizationToken);
        }

        /// <summary>
        /// Get job summary data for a specific job id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id or well API Number</param>
        /// <returns>Collection of JobSummary objects.</returns>
        public IEnumerable<JobSummary> Get(string id)
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
        /// Get job summary data for a specific job id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id or well API Number</param>
        /// <returns>Collection of JobSummary objects.</returns>
        public async Task<IEnumerable<JobSummary>> GetAsync(string id)
        {
            string requestUri = string.Format("{0}/{1}", Endpiont, id);
            var jobSummaryJson = await _client.GetStringAsync(requestUri);
            return jobSummaryJson.DeserializeJson<IEnumerable<JobSummary>>();
        }
    }
}
