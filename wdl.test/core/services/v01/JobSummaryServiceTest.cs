using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using wdl.common.models.v01;
using wdl.core.services.v01;

namespace wdl.test.core.services.v01
{
    [TestClass]
    public class JobSummaryServiceTest
    {
        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static JobSummaryService _jobSummaryService;
        private static IEnumerable<JobHeader> _jobHeaders;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _jobSummaryService = new JobSummaryService(TestApiKey);
            _jobHeaders = _jobHeaderService.GetAll();
        }

        /// <summary>
        /// Test retireving job summaries using a well api number.
        /// Validate every summary data row has the same well api number and demonstrate
        /// using column metadata to find the row data correct index.
        /// </summary>
        [TestMethod]
        public void Get_Test()
        {
            var expectedWellApiNumber = _jobHeaders.First().API;

            var jobSummaries = _jobSummaryService.Get(expectedWellApiNumber);
            Assert.IsNotNull(jobSummaries);
            Assert.AreEqual(1, jobSummaries.Count());

            var jobSummary = jobSummaries.First();

            //get well api number column
            var wellApiColumn = jobSummary.ColumnMetadata.SingleOrDefault(c => c.WdlFieldName == "well_api");

            //since well api number is part of the job header, all data rows should have he same value for the column
            foreach (var row in jobSummary.RowData.ToList())
            {
                //get the api number from the data rows using the columnIndex from the metdata
                var rowWellApiNumber = row.Skip(wellApiColumn.ColumnIndex).First();
                Assert.AreEqual(expectedWellApiNumber, rowWellApiNumber);
            }

        }

        /// <summary>
        /// Test retireving job summaries using a well api number.
        /// Validate every summary data row has the same well api number.
        /// </summary>
        [TestMethod]
        public async Task GetAsync_Test()
        {
            var jobId = _jobHeaders.First().JobId;
            var jobSummaries = await _jobSummaryService.GetAsync(jobId.ToString());
            Assert.IsNotNull(jobSummaries);
            Assert.AreEqual(1, jobSummaries.Count());
            Assert.AreEqual(jobId, jobSummaries.First().JobId);
        }
    }
}
