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
        public void GetAll_Demo()
        {
            var jobSummaries = _jobSummaryService.GetAll().ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


        /// <summary>
        /// Async Test get the most recent 10 job summaries.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Demo()
        {
			var task = _jobSummaryService.GetAllAsync();
            var jobSummaries = (await task).ToList() ;
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


        /// <summary>
        /// Test get job summaries using a well api number.
        /// Validate every summary data row has the same well api number and demonstrate
        /// using column metadata to find the row data correct index.
        /// </summary>
		[Test]
        public void Get_Demo()
        {
            var expectedWellApiNumber = _jobHeaders.First().API;

            var jobSummaries = _jobSummaryService.Get(expectedWellApiNumber).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.AreEqual(1, jobSummaries.Count);

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
        public async Task GetAsync_Demo()
        {
            var jobId = _jobHeaders.First().JobId;
			var task = _jobSummaryService.GetAsync(jobId.ToString());
            var jobSummaries = (await task).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.AreEqual(1, jobSummaries.Count());
            Assert.AreEqual(jobId, jobSummaries.First().JobId);
        }


        /// <summary>
        /// Test get job summaries using a timeframe.
        /// </summary>
		[Test]
        public void GetByChangeUtc_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddSeconds(-1);
            var toChangeUtc = realModifiedUtc.AddSeconds(1);
            var jobSummaries = _jobSummaryService.GetByChangeUtc(fromChangeUtc, toChangeUtc).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


        /// <summary>
        /// Async test get job summaries using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByChangeUtcAsync_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddSeconds(-1);
            var toChangeUtc = realModifiedUtc.AddSeconds(1);
            var task = _jobSummaryService.GetByChangeUtcAsync(fromChangeUtc, toChangeUtc);
            var jobSummaries = (await task).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


        /// <summary>
        /// Test get job summaries using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 1;
            var jobSummaries = _jobSummaryService.GetByStageNumber(fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


        /// <summary>
        /// Async test get job summaries using a stage number range.
        /// </summary>
        [Test]
        public async Task GetByStageNumberAsync_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 1;
            var task = _jobSummaryService.GetByStageNumberAsync(fromStageNumber, toStageNumber);
            var jobSummaries = (await task).ToList();
            Assert.IsNotNull(jobSummaries);
            Assert.IsTrue(jobSummaries.Count > 0);
        }


		//**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************

        private DateTime GetActualModifiedUtc()
        {
	        var header = _jobHeaderService
		        .GetAll()
		        .FirstOrDefault(m => m.ModifiedUtc.HasValue);
	        return header == null ? DateTime.UtcNow : header.ModifiedUtc.Value;
        }

    }
}
