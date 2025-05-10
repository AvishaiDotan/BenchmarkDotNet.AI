# OpenAI Completion Benchmarking Example

This example demonstrates how to benchmark OpenAI completion requests using BenchmarkDotNetWrapper.AI.

## Prerequisites

- .NET 8.0 or later
- BenchmarkDotNetWrapper.AI package installed
- OpenAI API key

## Code Example

```csharp
using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNetWrapper.AI;
using BenchmarkDotNetWrapper.AI.Types;
using BenchmarkDotNetWrapper.AI.LlmEngines.OpenAI;

namespace OpenAiCompletionBenchmark
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Configure the LLM engine options
            var llmOptions = new LlmEngineOptions
            {
                EngineType = typeof(OpenAiEngine),
                ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY"),
                OveridingPrompt = "Analyze this OpenAI completion benchmark"
            };

            // Run the benchmark with AI analysis
            var summary = await BenchmarkRunner.Run<OpenAiCompletionBenchmarks>()
                .WithAI<OpenAiCompletionBenchmarks>(llmOptions);

            Console.WriteLine(summary.ToString());
        }
    }

    [MemoryDiagnoser]
    public class OpenAiCompletionBenchmarks
    {
        // Benchmark methods for OpenAI completions
        [Benchmark(Baseline = true)]
        public void ShortPromptCompletion()
        {
            // Code that makes a short prompt completion request
            // This would typically use HttpClient or a specific OpenAI SDK
            // to send a completion request with a short prompt
            System.Threading.Thread.Sleep(100); // Simulating API call time
        }

        [Benchmark]
        public void LongPromptCompletion()
        {
            // Code that makes a longer prompt completion request
            System.Threading.Thread.Sleep(300); // Simulating API call time
        }
        
        [Benchmark]
        public void ShortPromptWithHighTemperature()
        {
            // Code that makes a completion request with higher temperature setting
            System.Threading.Thread.Sleep(120); // Simulating API call time
        }
    }
}
```

## Understanding the Results

This benchmark compares:

1. A short prompt (baseline)
2. A longer prompt that requires more processing
3. A short prompt with increased temperature (more randomness)

The results will show execution time and memory usage metrics, along with AI-powered analysis of the benchmark results.

## Best Practices

- Always set token limits to control response size
- Consider benchmarking with different parameter combinations
- For production systems, cache results when appropriate
- Use environment variables for API keys instead of hardcoding them 