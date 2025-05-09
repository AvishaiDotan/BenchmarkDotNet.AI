using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.AI.Types
{
    public class BenchmarkData
    {
        public string Name { get; set; }
        public BenchmarkStatistics benchmarkStatistics { get; set; }
    }
}
