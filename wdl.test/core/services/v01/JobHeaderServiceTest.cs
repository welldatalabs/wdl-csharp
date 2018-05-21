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
        public void GetAll_Demo()
        {
            var jobHeaders = _jobHeaderService.GetAll().ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
        }


        /// <summary>
        /// Async Test get all job headers.
        /// </summary>
		[Test]
        public async Task GetAllAsync_Demo()
        {
            var task = _jobHeaderService.GetAllAsync();
			var jobHeaders = (await task).ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
        }


        /// <summary>
        /// Test get job header using a well api number.
        /// </summary>
		[Test]
        public void Get_Demo()
        {
            string wellApiNumber = GetWellApiNumber();
            var jobHeaders = _jobHeaderService.Get(wellApiNumber).ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
        }


        /// <summary>
        /// Async test get job header using a well api number.
        /// </summary>
		[Test]
        public async Task GetAsync_Demo()
        {
            string wellApiNumber = GetWellApiNumber();
			var task = _jobHeaderService.GetAsync(wellApiNumber);
            var jobHeaders = (await task).ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
        }


        /// <summary>
        /// Test get job headers using a timeframe.
        /// </summary>
		[Test]
        public void GetByChangeUtc_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddMinutes(-1);
            var toChangeUtc = realModifiedUtc.AddMinutes(1);
            var jobHeaders = _jobHeaderService.GetByChangeUtc(fromChangeUtc, toChangeUtc).ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
        }


        /// <summary>
        /// Async test get job header using a timeframe.
        /// </summary>
		[Test]
        public async Task GetByChangeUtcAsync_Demo()
        {
            var realModifiedUtc = GetActualModifiedUtc();
            var fromChangeUtc = realModifiedUtc.AddMinutes(-1);
            var toChangeUtc = realModifiedUtc.AddMinutes(1);
            var task = _jobHeaderService.GetByChangeUtcAsync(fromChangeUtc, toChangeUtc);
            var jobHeaders = (await task).ToList();
            Assert.IsTrue(jobHeaders.Count > 0);
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

        private string GetWellApiNumber()
        {
            var header = _jobHeaderService.GetAll().FirstOrDefault();
            return header == null ? "10-203-04050-60-" : header.API;
        }

        private DateTime GetActualModifiedUtc()
        {
            var header = _jobHeaderService
				.GetAll()
				.FirstOrDefault(m => m.ModifiedUtc.HasValue);
			return header == null ? DateTime.UtcNow : header.ModifiedUtc.Value;
        }
    }
}
