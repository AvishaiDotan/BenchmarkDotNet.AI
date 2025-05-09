using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.AI.Types
{
    public class BenchmarkStatistics
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
        public double? Mean { get; set; }
        public double? Error { get; set; }
        public double? StdDev { get; set; }
        public double? Median { get; set; }
        public double? Q1 { get; set; }
        public double? Q3 { get; set; }
        public double? Kurtosis { get; set; }
        public double? InterquartileRange { get; set; }
        public double? LowerFence { get; set; }
        public double? UpperFence { get; set; }
        public double? Variance { get; set; }
        public double? Skewness { get; set; }
        public int? Count { get; set; }
    }
}
