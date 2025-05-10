using BenchmarkDotNetWrapper.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNetWrapper.AI.LlmEngines.Common
{
    public interface ILlmEngineSwitcher
    {
        static abstract ILlmEngine GetEngine(Type engineType, string apiKey, string? overridingPrompt = null);
    }
}
