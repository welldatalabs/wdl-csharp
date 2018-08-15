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
    public class DepthFeatureServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************

        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static DepthFeatureService _depthFeatureService;
        private static IEnumerable<DepthFeature> _depthFeatures;


		//**********************************************************************************
		// SETUP/CLEANUP
		//**********************************************************************************
		
		[SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _depthFeatureService = new DepthFeatureService(TestApiKey);
            _depthFeatures = _depthFeatureService.GetAll();
        }


		//**********************************************************************************
		// TESTS
		//**********************************************************************************

        /// <summary>
        /// Test get the depth features for the most recent 10 jobs.
        /// </summary>
        [Test]
        public void GetAll_Demo()
        {
            var depthFeatures = _depthFeatureService.GetAll().ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


        /// <summary>
        /// Async Test get the depth features for the most recent 10 jobs.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Demo()
        {
			var task = _depthFeatureService.GetAllAsync();
            var depthFeatures = (await task).ToList() ;
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


        /// <summary>
        /// Test get depth features using a job id.
        /// Validate every result row has the same job id.
        /// </summary>
		[Test]
        public void Get_Demo()
        {
            var expectedJobId = _depthFeatures.First().JobId.ToString();
            var depthFeatures = _depthFeatureService.Get(expectedJobId).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);

            //since job id is part of the depth features, all results should have he same job id as the expectedJobId
            foreach (var row in depthFeatures.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Async test get depth features using a job id.
        /// </summary>
		[Test]
        public async Task GetAsync_Demo()
        {
            var expectedJobId = _depthFeatures.First().JobId.ToString();
            var task = _depthFeatureService.GetAsync(expectedJobId);
            var depthFeatures = (await task).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
            
            //since job id is part of the custom flag, all results should have he same job id as the expectedJobId
            foreach (var row in depthFeatures.ToList())
            {
                //get the job id from the result rows
                var rowJobId = row.JobId.ToString();
                Assert.AreEqual(expectedJobId, rowJobId);
            }
        }


        /// <summary>
        /// Test get depth features using a timeframe.
        /// </summary>
		[Test]
        public void GetByChangeUtc_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var depthFeatures = _depthFeatureService.GetByChangeUtc(fromChangeUtc, toChangeUtc).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


        /// <summary>
        /// Async test get depth features using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByChangeUtcAsync_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddYears(-1);
            var toChangeUtc = realModifiedUtc.AddYears(1);
            var task = _depthFeatureService.GetByChangeUtcAsync(fromChangeUtc, toChangeUtc);
            var depthFeatures = (await task).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


        /// <summary>
        /// Test get depth features using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var depthFeatures = _depthFeatureService.GetByStageNumber(fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


        /// <summary>
        /// Async test get depth features using a stage number range.
        /// </summary>
        [Test]
        public async Task GetByStageNumberAsync_Demo()
        {
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var task = _depthFeatureService.GetByStageNumberAsync(fromStageNumber, toStageNumber);
            var depthFeatures = (await task).ToList();
            Assert.IsNotNull(depthFeatures);
            Assert.IsTrue(depthFeatures.Count > 0);
        }


		//**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************

        private DateTime GetActualModifiedUtc()
        {
            var jobId = _depthFeatures.First().JobId.ToString();
	        var header = _jobHeaderService
                .Get(jobId)
		        .FirstOrDefault(m => m.ModifiedUtc.HasValue);
	        return header == null ? DateTime.UtcNow : header.ModifiedUtc.Value;
        }

    }
}
