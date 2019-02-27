using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.core.services.v01
{
    /// <summary>
    /// The PerSecData API is a premium paid feature, please contact sales@welldatalabs.com for more information.
    /// This service produces much more data than others in the WDL system. We suggest downloading to a file or stream.
    /// </summary>
    public class PerSecDataService
    {
        private const string _endpoint = "persecdata";
        private const string _version = "1";
        private string _apiKey;
        private Uri _baseUri;


        public PerSecDataService(string apiKey, string baseUri = null)
        {
            _apiKey = apiKey;
            _baseUri = string.IsNullOrWhiteSpace(baseUri) ? HttpHelper.BaseUri : new Uri(baseUri);
        }
        
        /// <summary>
        /// This downloads the per-sec data for the series of stages of a job
        /// </summary>
        /// <param name="id">Job ID or Job API Number</param>
        /// <param name="startingStageNumber">Stage number to start with</param>
        /// <param name="endingStageNumber">Stage number to end with <example>20.1m</example></param>
        /// <returns>FileInfo for the temp file</returns>
        public async Task<FileInfo> DownloadToFile(string id, decimal? startingStageNumber = null, decimal? endingStageNumber = null)
        {
            var fileName = Path.GetTempFileName();

            using (var file = File.OpenWrite(fileName))
            {
                await DownloadToStream(file, id, startingStageNumber, endingStageNumber);
            }

            return new FileInfo(fileName);
        }

        internal async Task DownloadToStream(Stream outStream, string id, decimal? startingStageNumber = null, decimal? endingStageNumber = null)
        {
            var requestUri = CreateRequestUri(id, startingStageNumber, endingStageNumber);

            using (var stream = await HttpHelper.GetStreamAsync(requestUri, _apiKey, _version))
            {
                await stream.CopyToAsync(outStream);
            }
        }

        private string CreateRequestUri(string id, decimal? startingStageNumber, decimal? endingStageNumber)
        {
            var parameters = new Dictionary<string, string>(3);
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Job ID must be a GUID or an API number", nameof(id));
            }

            parameters.Add("id", id);

            if (startingStageNumber.GetValueOrDefault(0) > 0)
            {
                var fromStage = startingStageNumber.ToString();
                if (startingStageNumber % 1 == 0)
                    fromStage = ((int) startingStageNumber).ToString();

                parameters.Add("fromStageNumber", fromStage);
            }

            if (endingStageNumber.GetValueOrDefault(0) > 0)
            {
                var toStage = endingStageNumber.ToString();
                if (endingStageNumber % 1 == 0)
                    toStage = ((int) endingStageNumber).ToString();

                parameters.Add("toStageNumber", toStage);
            }

            return HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
        }
    }
}
