using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class Plug
    {
        public Guid JobId { get; set; }
        public string WellName { get; set; }
        public string API { get; set; }
        public decimal StageNumber { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public decimal? TopMeasuredDepth { get; set; }
        public decimal? BottomMeasuredDepth { get; set; }
        public string DepthUnit { get; set; }
        public decimal? Diameter { get; set; }
        public string DiameterUnit { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
