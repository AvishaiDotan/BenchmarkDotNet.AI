using BenchmarkDotNetWrapper.AI.LlmEngines.OpenAI;
using BenchmarkDotNetWrapper.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNetWrapper.AI.LlmEngines.Common
{
    public class LlmEngineSwitcher : ILlmEngineSwitcher
    {
        public static ILlmEngine GetEngine(Type engineType, string apiKey, string? overridingPrompt = null)
        {
            return engineType switch
            {
                var t when t == typeof(OpenAIEngine) => new OpenAIEngine(apiKey, overridingPrompt),
                _ => throw new NotSupportedException($"Unsupported type: {engineType.Name}")
            };
        }
    }
}
