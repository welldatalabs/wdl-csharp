using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using wdl.core.services.v01;

namespace wdl.test.core.services.v01
{
    [TestClass]
    public class JobHeaderServiceTest
    {
        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        JobHeaderService _jobHeaderService = new JobHeaderService(TestApiKey);

        /// <summary>
        /// Test get all job headers.
        /// </summary>
        [TestMethod]
        public void GetAll_Success()
        {
            var jobHeaders = _jobHeaderService.GetAll();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }

        /// <summary>
        /// Async Test get all job headers.
        /// </summary>
        [TestMethod]
        public async Task GetAllAsync_Success()
        {
            var jobHeaders = await _jobHeaderService.GetAllAsync();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }

        /// <summary>
        /// Test get job header using a well api number.
        /// </summary>
        [TestMethod]
        public void Get_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = _jobHeaderService.Get(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.AreEqual(1, jobHeaders.Count());
        }

        /// <summary>
        /// Async test get job header using a well api number.
        /// </summary>
        [TestMethod]
        public void GetAsync_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = _jobHeaderService.Get(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.AreEqual(1, jobHeaders.Count());
        }

        /// <summary>
        /// Test get a job header for an invlid job id.  There api
        /// will return a 404 not found which will become a HttpRequestException
        /// from the .Net HttpClient class.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void Get_NoResults()
        {
            string jobId = "asdf1234";
            var jobHeaders = _jobHeaderService.Get(jobId);
        }
    }
}
