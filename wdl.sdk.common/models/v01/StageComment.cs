using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class StageComment
    {
        public Guid JobId { get; set; }
        public decimal StageNumber { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
    }
}
