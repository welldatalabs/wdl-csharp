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

        [TestMethod]
        public void GetAll_Success()
        {
            var jobHeaders = _jobHeaderService.GetAll();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }

        [TestMethod]
        public async Task GetAllAsync_Success()
        {
            var jobHeaders = await _jobHeaderService.GetAllAsync();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }

        [TestMethod]
        public void Get_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = _jobHeaderService.Get(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.AreEqual(1, jobHeaders.Count());
        }

        [TestMethod]
        public void GetAsync_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = _jobHeaderService.Get(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.AreEqual(1, jobHeaders.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void Get_NoResults()
        {
            string jobId = "asdf1234";
            var jobHeaders = _jobHeaderService.Get(jobId);
        }
    }
}
