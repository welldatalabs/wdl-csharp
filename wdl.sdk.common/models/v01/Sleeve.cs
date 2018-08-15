using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class Sleeve
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
        public decimal? PortSize { get; set; }
        public string PortSizeUnit { get; set; }
        public decimal? BallSize { get; set; }
        public string BallSizeUnit { get; set; }
        public decimal? SeatID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
