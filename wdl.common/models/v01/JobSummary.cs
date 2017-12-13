using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.common.models.v01
{
    public class JobSummary
    {
        /// <summary>
        /// Unique id of the job.
        /// </summary>
        public Guid JobId { get; set; }
        /// <summary>
        /// Collection of all the columns and their metadata for the job.
        /// </summary>
        public IEnumerable<JobSummaryColumn> ColumnMetadata { get; set; }
        /// <summary>
        /// Job total and stage row data for the job.  Each row is a collection of values whose index will lines up with the column index in the ColumnMetadata collection.
        /// </summary>
        public IEnumerable<IEnumerable<string>> RowData { get; set; }
    }
}
