using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.AI.Types
{
    public record CompletionRequestPayload
    {
        public string Prompt { get; set; } = string.Empty;
        public BenchmarkContext? benchmarkContext { get; set; } = null;
    }
}
