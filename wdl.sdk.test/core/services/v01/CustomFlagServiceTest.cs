using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using wdl.sdk.common.models.v01;
using wdl.sdk.core.services.v01;


namespace wdl.test.core.services.v01
{
    [TestFixture]
    public class CustomFlagServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************

        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static CustomFlagService _customFlagService;
        private static IEnumerable<CustomFlag> _customFlags;


		//**********************************************************************************
		// SETUP/CLEANUP
		//**********************************************************************************
		
		[SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _customFlagService = new CustomFlagService(TestApiKey);
            _customFlags = _customFlagService.GetAll();
        }


		//**********************************************************************************
		// TESTS
		//**********************************************************************************

        /// <summary>
        /// Test get the most recent 1000 custom flags.
        /// </summary>
        [Test]
        public void GetAll_Demo()
        {
            var customFlags = _customFlagService.GetAll().ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


        /// <summary>
        /// Async Test get the most recent 1000 custom flags.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Demo()
        {
            var task = _customFlagService.GetAllAsync();
            var customFlags = (await task).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


        /// <summary>
        /// Test get custom flags using a job id.
        /// Validate every result row has the same job id.
        /// </summary>
		[Test]
        public void Get_Demo()
        {
            var expectedJobId = _customFlags.First().JobId.ToString();
            var customFlags = _customFlagService.Get(expectedJobId).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);

            //since job id is part of the custom flag, all results should have he same job id as the expectedJobId
            foreach (var row in customFlags.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Async test get custom flags using a job id.
        /// </summary>
		[Test]
        public async Task GetAsync_Demo()
        {
            var expectedJobId = _customFlags.First().JobId.ToString();
            var task = _customFlagService.GetAsync(expectedJobId);
            var customFlags = (await task).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);

            //since job id is part of the custom flag, all results should have he same job id as the expectedJobId
            foreach (var row in customFlags.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Test get custom flags using a timeframe.
        /// </summary>
		[Test]
        public void GetByChangeUtc_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var customFlags = _customFlagService.GetByChangeUtc(fromChangeUtc, toChangeUtc).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


        /// <summary>
        /// Async test get custom flags using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByChangeUtcAsync_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var task = _customFlagService.GetByChangeUtcAsync(fromChangeUtc, toChangeUtc);
            var customFlags = (await task).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


        /// <summary>
        /// Test get custom flags using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var customFlags = _customFlagService.GetByStageNumber(fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


        /// <summary>
        /// Async test get custom flags using a stage number range.
        /// </summary>
        [Test]
        public async Task GetByStageNumberAsync_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var task = _customFlagService.GetByStageNumberAsync(fromStageNumber, toStageNumber);
            var customFlags = (await task).ToList();
            Assert.IsNotNull(customFlags);
            Assert.IsTrue(customFlags.Count > 0);
        }


		//**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************

        private DateTime GetActualModifiedUtc()
        {
            var jobId = _customFlags.First().JobId.ToString();
	        var header = _jobHeaderService
	            .Get(jobId)
		        .FirstOrDefault(m => m.ModifiedUtc.HasValue);
	        return header == null ? DateTime.UtcNow : header.ModifiedUtc.Value;
        }

    }
}
