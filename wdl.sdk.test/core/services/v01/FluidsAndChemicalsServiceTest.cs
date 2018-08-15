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
    public class FluidsAndChemicalsServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************

        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static FluidsAndChemicalsService _fluidsAndChemicalsService;
        private static IEnumerable<FluidsAndChemicals> _fluidsAndChemicals;


		//**********************************************************************************
		// SETUP/CLEANUP
		//**********************************************************************************
		
		[SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _fluidsAndChemicalsService = new FluidsAndChemicalsService(TestApiKey);
            _fluidsAndChemicals = _fluidsAndChemicalsService.GetAll();
        }


		//**********************************************************************************
		// TESTS
		//**********************************************************************************

        /// <summary>
        /// Test get the fluids and chemicals for the most recent 10 jobs.
        /// </summary>
        [Test]
        public void GetAll_Demo()
        {
            var fluidsAndChemicals = _fluidsAndChemicalsService.GetAll().ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


        /// <summary>
        /// Async Test get the fluids and chemicals for the most recent 10 jobs.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Demo()
        {
			var task = _fluidsAndChemicalsService.GetAllAsync();
            var fluidsAndChemicals = (await task).ToList() ;
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


        /// <summary>
        /// Test get fluids and chemicals using a job id.
        /// Validate every result row has the same job id.
        /// </summary>
		[Test]
        public void Get_Demo()
        {
            var expectedJobId = _fluidsAndChemicals.First().JobId.ToString();
            var fluidsAndChemicals = _fluidsAndChemicalsService.Get(expectedJobId).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);

            //since job id is part of the fluids and chemicals, all results should have he same job id as the expectedJobId
            foreach (var row in fluidsAndChemicals.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Async test get fluids and chemicals using a job id.
        /// </summary>
		[Test]
        public async Task GetAsync_Demo()
        {
            var expectedJobId = _fluidsAndChemicals.First().JobId.ToString();
            var task = _fluidsAndChemicalsService.GetAsync(expectedJobId);
            var fluidsAndChemicals = (await task).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
            
            //since job id is part of the custom flag, all results should have he same job id as the expectedJobId
            foreach (var row in fluidsAndChemicals.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Test get fluids and chemicals using a timeframe.
        /// </summary>
		[Test]
        public void GetByChangeUtc_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var fluidsAndChemicals = _fluidsAndChemicalsService.GetByChangeUtc(fromChangeUtc, toChangeUtc).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


        /// <summary>
        /// Async test get fluids and chemicals using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByChangeUtcAsync_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var task = _fluidsAndChemicalsService.GetByChangeUtcAsync(fromChangeUtc, toChangeUtc);
            var fluidsAndChemicals = (await task).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


        /// <summary>
        /// Test get fluids and chemicals using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var fluidsAndChemicals = _fluidsAndChemicalsService.GetByStageNumber(fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


        /// <summary>
        /// Async test get fluids and chemicals using a stage number range.
        /// </summary>
        [Test]
        public async Task GetByStageNumberAsync_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var task = _fluidsAndChemicalsService.GetByStageNumberAsync(fromStageNumber, toStageNumber);
            var fluidsAndChemicals = (await task).ToList();
            Assert.IsNotNull(fluidsAndChemicals);
            Assert.IsTrue(fluidsAndChemicals.Count > 0);
        }


		//**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************

        private DateTime GetActualModifiedUtc()
        {
            var jobId = _fluidsAndChemicals.First().JobId.ToString();
	        var header = _jobHeaderService
                .Get(jobId)
		        .FirstOrDefault(m => m.ModifiedUtc.HasValue);
	        return header == null ? DateTime.UtcNow : header.ModifiedUtc.Value;
        }

    }
}
