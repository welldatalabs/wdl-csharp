using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using wdl.sdk.common.models.v01;
using wdl.sdk.core.services.v01;


namespace wdl.test.core.services.v01
{
    /// <summary>
    /// NOTE: The per-sec data API is heavily throttled.
    /// Please run these tests with your usage restrictions in mind (suggested 1 request/min).
    /// </summary>
    
    [TestFixture]
    public class PerSecDataServiceTest
    {
		//**********************************************************************************
		// PROPERTIES/FIELDS
		//**********************************************************************************
        
        private const string TestApiKey = "b+S15uKWEK0lFU+NomEmvekn8yk/ALTTBAYOJalVKrI=";
        private const string JobId = "8eeefc91-8b9e-403b-ad27-1d53f339f8a3";
        private readonly PerSecDataService _perSecDataService = new PerSecDataService(TestApiKey);
        
        
		//**********************************************************************************
		// TESTS
		//**********************************************************************************
        [Test]
        public async Task Download_File_Exists_With_Data()
        {
            var download = await _perSecDataService.DownloadToFile(JobId, 1, 2);

            FileAssert.Exists(download);
            Assert.NotZero(download.Length);



            using (var reader = File.OpenText(download.FullName))
            {
                var header = reader.ReadLine() ?? String.Empty; // Main header row for column names
                var units = reader.ReadLine();                  // Sub-header row for column units

                var columnCount = header.Count(ch => ch == ',') + 1;

                long recordCount = 0;
                while (reader.ReadLine() != null)
                {
                    recordCount++;
                }

                Console.WriteLine("Per-sec file stats\n" +
                                        $"Bytes: {download.Length}\t" +
                                        $"Columns: {columnCount}\t" +
                                        $"Records: {recordCount}");
            }
            download.Delete();
        }
        
        [Test]
        public async Task Download_File_With_AllChannels_Includes_Extra_Channels()
        {
            var download = await _perSecDataService.DownloadToFile(JobId, 1, 2, true);

            FileAssert.Exists(download);
            Assert.NotZero(download.Length);

            var globalHeaders = new[]
            {
                "TREATING PRESSURE",
                "BOTTOMHOLE PRESSURE",
                "ANNULUS PRESSURE",
                "SURFACE PRESSURE",
                "SLURRY RATE",
                "CLEAN VOLUME",
                "SLURRY VOLUME",
                "PROPPANT TOTAL",
                "PROPPANT CONC",
                "BOTTOMHOLE PROPPANT CONC"
            };
            
            using (var reader = File.OpenText(download.FullName))
            {
                var header = reader.ReadLine() ?? String.Empty; // Main header row for column names
                var units = reader.ReadLine();                  // Sub-header row for column units

                var columnCount = header.Count(ch => ch == ',') + 1;

                
                var headerCollection = new SortedSet<string>(header.Split(','));

                Assert.True(headerCollection.IsProperSupersetOf(globalHeaders));


                long recordCount = 0;
                while (reader.ReadLine() != null)
                {
                    recordCount++;
                }

                Console.WriteLine("Per-sec file stats\n" +
                                  $"Bytes: {download.Length}\t" +
                                  $"Columns: {columnCount}\t" +
                                  $"Records: {recordCount}");
                Console.WriteLine($"Downloaded headers: {string.Join(", ", headerCollection)}");
            }
            download.Delete();
        }


        //**********************************************************************************
		// PRIVATE HELPERS
		//**********************************************************************************
        
    }
}
