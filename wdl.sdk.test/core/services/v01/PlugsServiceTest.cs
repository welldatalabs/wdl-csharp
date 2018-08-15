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
    public class PlugsServiceTest
    {
        //**********************************************************************************
        // PROPERTIES/FIELDS
        //**********************************************************************************

        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private static JobHeaderService _jobHeaderService;
        private static PlugsService _plugsService;


        //**********************************************************************************
        // SETUP/CLEANUP
        //**********************************************************************************

        [SetUp]
        public static void Setup()
        {
            _jobHeaderService = new JobHeaderService(TestApiKey);
            _plugsService = new PlugsService(TestApiKey);
        }


        //**********************************************************************************
        // TESTS
        //**********************************************************************************

        /// <summary>
        /// Test get plugs using a job id.
        /// </summary>
        [Test]
        public void Get_Demo()
        {
            string jobId = GetJobId();
            var plugs = _plugsService.Get(jobId).ToList();
            Assert.IsTrue(plugs.Count >= 0);
        }


        /// <summary>
        /// Async test get plugs using a job id.
        /// </summary>
        [Test]
        public async Task GetAsync_Demo()
        {
            string jobId = GetJobId();
            var task = _plugsService.GetAsync(jobId);
            var plugs = (await task).ToList();
            Assert.IsTrue(plugs.Count >= 0);
        }


        /// <summary>
        /// Test get plugs using a stage number range.
        /// </summary>
        [Test]
        public void GetByStageNumber_Demo()
        {
            string jobId = GetJobId();
            var fromStageNumber = 1;
            var toStageNumber = 100;
            var plugs = _plugsService.Get(jobId, fromStageNumber, toStageNumber).ToList();
            Assert.IsNotNull(plugs);
            Assert.IsTrue(plugs.Count >= 0);
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
            var task = _plugsService.GetAsync(jobId, fromStageNumber, toStageNumber);
            var plugs = (await task).ToList();
            Assert.IsNotNull(plugs);
            Assert.IsTrue(plugs.Count >= 0);
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
