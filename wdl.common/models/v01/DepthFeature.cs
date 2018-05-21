﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.common.models.v01
{
    public class DepthFeature
    {
        public Guid JobId { get; set; }
        public string WellName { get; set; }
        public string API { get; set; }
        public decimal StageNumber { get; set; }
        public string Feature { get; set; }
        public string Name { get; set; }
        public decimal? TopMeasuredDepth { get; set; }
        public decimal? BottomMeasuredDepth { get; set; }
        public string DepthUnit { get; set; }
        public decimal? Clusters { get; set; }
        public decimal? ShotDensity { get; set; }
        public string ShotDensityUnit { get; set; }
        public decimal? ShotCount { get; set; }
        public decimal? Phasing { get; set; }
        public decimal? PortSize { get; set; }
        public string PortSizeUnit { get; set; }
        public decimal? BallSize { get; set; }
        public string BallSizeUnit { get; set; }
        public decimal? SeatID { get; set; }
        public decimal? Diameter { get; set; }
        public string DiameterUnit { get; set; }
    }
}
