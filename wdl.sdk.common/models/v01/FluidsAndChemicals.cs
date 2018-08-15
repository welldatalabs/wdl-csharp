using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.sdk.common.models.v01
{
    public class FluidsAndChemicals
    {
        public Guid JobId { get; set; }
        public string WellName { get; set; }
        public string API { get; set; }
        public decimal StageNumber { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Name { get; set; }
        public decimal? Volume { get; set; }
        public string VolumeUnit { get; set; }
        public decimal? VolumeConcentration { get; set; }
        public string VolumeConcentrationUnit { get; set; }
        public decimal? DryTotal { get; set; }
        public string DryTotalUnit { get; set; }
        public decimal? DryConcentration { get; set; }
        public string DryConcentrationUnit { get; set; }
    }
}
