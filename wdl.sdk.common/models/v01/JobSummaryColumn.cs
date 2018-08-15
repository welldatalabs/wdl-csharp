using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class JobSummaryColumn
    {
        public string Name { get; set; }
        public string WdlFieldName { get; set; }
        public string UnitText { get; set; }
        public int ColumnIndex { get; set; }
        public string DataType { get; set; }
    }
}
