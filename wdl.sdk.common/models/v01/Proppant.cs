using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class Proppant
    {
        public Guid JobId { get; set; }
        public decimal StageNumber { get; set; }
        public string Name { get; set; }
        public decimal? Mass { get; set; }
        public string MassUnit { get; set; }
        public string Material { get; set; }
        public string MeshSize { get; set; }
    }
}
