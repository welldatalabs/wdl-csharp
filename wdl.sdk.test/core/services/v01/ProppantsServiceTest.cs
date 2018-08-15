using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using wdl.sdk.common.models.v01;
using wdl.sdk.core.services.v01;


namespace wdl.test.core.services.v01
{
    [TestFixture]
    public class ProppantsServiceTest
    {
        //**********************************************************************************
        // PROPERTIES/FIELDS
        //**********************************************************************************

        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static ProppantsService _proppantsService;


        //**********************************************************************************
        // SETUP/CLEANUP
        //**********************************************************************************

        [SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _proppantsService = new ProppantsService(TestApiKey);
        }


        //**********************************************************************************
        // TESTS
        //**********************************************************************************

        /// <summary>
        /// Test get proppants using a job id.
        /// </summary>
        [Test]
        public void Get_Demo()
        {
            string jobId = GetJobId();
            var proppants = _proppantsService.Get(jobId).ToList();
            Assert.IsTrue(proppants.Count >= 0);
        }


        /// <summary>
        /// Async test get proppants using a job id.
        /// </summary>
        [Test]
        public async Task GetAsync_Demo()
        {
            string jobId = GetJobId();
            var task = _proppantsService.GetAsync(jobId);
            var proppants = (await task).ToList();
            Assert.IsTrue(proppants.Count >= 0);
        }


        /// <summary>
        /// Test get proppants using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            string jobId = GetJobId();
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var proppants = _proppantsService.Get(jobId, fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(proppants);
            Assert.IsTrue(proppants.Count >= 0);
        }


        /// <summary>
        /// Async test get depth features using a stage number range.
        /// </summary>
        [Test]
        public async Task GetByStageNumberAsync_Demo()
        {
            string jobId = GetJobId();
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var task = _proppantsService.GetAsync(jobId, fromStageNumber, toStageNumber);
            var proppants = (await task).ToList();
            Assert.IsNotNull(proppants);
            Assert.IsTrue(proppants.Count >= 0);
        }


        //**********************************************************************************
        // PRIVATE HELPERS
        //**********************************************************************************

        private string GetJobId()
        {
            var header = _jobHeaderService.GetAll().FirstOrDefault();
            return header == null ? "be9b1ff9-34b2-43bb-bf9a-aec7f77770ee" : header.JobId.ToString();
        }
    }
}
