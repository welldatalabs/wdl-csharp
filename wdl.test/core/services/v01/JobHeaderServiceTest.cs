using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using wdl.core.services.v01;

namespace wdl.test.core.services.v01
{
    [TestFixture]
    public class JobHeaderServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************
        		
		private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        JobHeaderService _jobHeaderService = new JobHeaderService(TestApiKey);


		//**********************************************************************************
		// TESTS
		//**********************************************************************************

        /// <summary>
        /// Test get all job headers.
        /// </summary>
        [Test]
        public void GetAll_Success()
        {
            var jobHeaders = _jobHeaderService.GetAll();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Async Test get all job headers.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Success()
        {
            var jobHeaders = await _jobHeaderService.GetAllAsync();
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Test get job header using a well api number.
        /// </summary>
		[Test]
        public void Get_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = _jobHeaderService.Get(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Async test get job header using a well api number.
        /// </summary>
		[Test]
        public async Task GetAsync_Success()
        {
            string wellApiNumber = "10-203-04050-60-";
            var jobHeaders = await _jobHeaderService.GetAsync(wellApiNumber);
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Test get job headers using a timeframe.
        /// </summary>
		[Test]
        public void GetByDates_Success()
        {
            var realModifiedUtc = GetRealModifiedUtc();
            var fromUtc = realModifiedUtc.AddSeconds(-1);
            var toUtc = realModifiedUtc.AddSeconds(1);
            var jobHeaders = _jobHeaderService.GetByDates(fromUtc, toUtc);
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Async test get job header using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByDatesAsync_Success()
        {
            var realModifiedUtc = GetRealModifiedUtc();
            var fromUtc = realModifiedUtc.AddSeconds(-1);
            var toUtc = realModifiedUtc.AddSeconds(1);
            var jobHeaders = await _jobHeaderService.GetByDatesAsync(fromUtc, toUtc);
            Assert.IsNotNull(jobHeaders);
            Assert.IsTrue(jobHeaders.Count() > 0);
        }


        /// <summary>
        /// Test get a job header for an invlid job id.  There api
        /// will return a 404 not found which will become a HttpRequestException
        /// from the .Net HttpClient class.
        /// </summary>
		[Test]
        public void Get_NoResults()
        {
            string jobId = "asdf1234";
			Assert.Throws<HttpRequestException>(() => _jobHeaderService.Get(jobId));
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
