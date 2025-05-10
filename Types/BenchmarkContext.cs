using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNetWrapper.AI.Types
{
    public class BenchmarkContext
    {
        public string Code { get; set; } = "";
        public List<BenchmarkData> BenchmarkData { get; set; }

    }
}
