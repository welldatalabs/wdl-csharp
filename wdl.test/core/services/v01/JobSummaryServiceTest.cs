using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using wdl.common.models.v01;
using wdl.core.services.v01;


namespace wdl.test.core.services.v01
{
    [TestFixture]
    public class JobSummaryServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************
       
		private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static JobSummaryService _jobSummaryService;
        private static IEnumerable<JobHeader> _jobHeaders;


		//**********************************************************************************
		// SETUP/CLEANUP
		//**********************************************************************************
		
		[SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _jobSummaryService = new JobSummaryService(TestApiKey);
            _jobHeaders = _jobHeaderService.GetAll();
        }


		//**********************************************************************************
		// TESTS
		//**********************************************************************************

        /// <summary>
        /// Test get the most recent 10 job summaries.
        /// </summary>
        [Test]
        public void GetAll_Success()
        {
            var jobSummaries = _jobSummaryService.GetAll();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count() > 0);
        }


        /// <summary>
        /// Async Test get the most recent 10 job summaries.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Success()
        {
            var jobSummaries = await _jobSummaryService.GetAllAsync();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count() > 0);
        }


        /// <summary>
        /// Test get job summaries using a well api number.
        /// Validate every summary data row has the same well api number and demonstrate
        /// using column metadata to find the row data correct index.
        /// </summary>
		[Test]
        public void Get_Success()
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
        /// Async test get job summaries using a well api number.
        /// </summary>
		[Test]
        public async Task GetAsync_Success()
        {
            var jobId = _jobHeaders.First().JobId;
            var jobSummaries = await _jobSummaryService.GetAsync(jobId.ToString());
            Assert.IsNotNull(jobSummaries);
            Assert.AreEqual(1, jobSummaries.Count());
            Assert.AreEqual(jobId, jobSummaries.First().JobId);
        }


        /// <summary>
        /// Test get job summaries using a timeframe.
        /// </summary>
		[Test]
        public void GetByDates_Success()
        {
            var realModifiedUtc = GetRealModifiedUtc();
            var fromUtc = realModifiedUtc.AddSeconds(-1);
            var toUtc = realModifiedUtc.AddSeconds(1);
            var jobSummaries = _jobSummaryService.GetByDates(fromUtc, toUtc);
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count() > 0);
        }


        /// <summary>
        /// Async test get job summaries using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByDatesAsync_Success()
        {
            var realModifiedUtc = GetRealModifiedUtc();
            var fromUtc = realModifiedUtc.AddSeconds(-1);
            var toUtc = realModifiedUtc.AddSeconds(1);
            var jobSummaries = await _jobSummaryService.GetByDatesAsync(fromUtc, toUtc);
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count() > 0);
        }


		//**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************

        private DateTime GetRealModifiedUtc()
        {
            var modifiedUtc = DateTime.UtcNow;
            var jobHeaders = _jobHeaderService.GetAll();
            var count = jobHeaders.Count();
            if (count > 0)
            {
                var jobHeader = jobHeaders.First();
                modifiedUtc = DateTime.Parse(jobHeader.ModifiedUtc);
            }

            return modifiedUtc;
        }

    }
}
