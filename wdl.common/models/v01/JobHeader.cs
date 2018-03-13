using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.common.models.v01
{
    public class JobHeader
    {
        public Guid JobId { get; set; }
        public Guid WellId { get; set; }
        public string WellName { get; set; }
        public string API { get; set; }
        public string JobStartDate { get; set; }
        public string ServiceCompany { get; set; }
        public string Operator { get; set; }
        public string AssetGroup { get; set; }
        public string Formation { get; set; }
        public string JobType { get; set; }
        public string FracSystem { get; set; }
        public string FluidSystem { get; set; }
        public Decimal? BottomholeLatitude { get; set; }
        public Decimal? BottomholeLongitude { get; set; }
        public int? MeasuredDepth { get; set; }
        public string MeasuredDepthUnitText { get; set; }
        public int StageCount { get; set; }
        public string PadName { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string SurfaceLatitude { get; set; }
        public string SurfaceLongitude { get; set; }
        public string LegalDescription { get; set; }
        public string ModifiedUtc { get; set; }
    }
}
