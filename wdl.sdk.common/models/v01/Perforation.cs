using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class Perforation
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
        public decimal? Clusters { get; set; }
        public decimal? ShotDensity { get; set; }
        public string ShotDensityUnit { get; set; }
        public decimal? ShotCount { get; set; }
        public decimal? Phasing { get; set; }
        public string PerforationCompany { get; set; }
        public decimal? GunSize { get; set; }
        public string GunSizeUnit { get; set; }
        public string ConveyanceMethod { get; set; }
        public string ChargeType { get; set; }
        public decimal? ChargeSize { get; set; }
        public string ChargeSizeUnit { get; set; }
        public decimal? Penetration { get; set; }
        public string PenetrationUnit { get; set; }
        public decimal? EstimatedHoleDiameter { get; set; }
        public string EstimatedHoleDiameterUnit { get; set; }
    }
}
