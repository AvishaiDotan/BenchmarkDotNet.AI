
using BenchmarkDotNetWrapper.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNetWrapper.AI.Types
{
    public record LlmEngineOptions
    {
        private Type _engineType = typeof(object);

        public Type EngineType
        {
            get => _engineType;
            set
            {
                if (!typeof(ILlmEngine).IsAssignableFrom(value))
                    throw new ArgumentException($"EngineType must implement {nameof(ILlmEngine)}.", nameof(value));
                _engineType = value;
            }
        }

        public string ApiKey { get; set; } = string.Empty;
        public string? OveridingPrompt { get; set; } = null;
    }
}
