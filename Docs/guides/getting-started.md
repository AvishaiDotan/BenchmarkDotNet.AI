# Getting Started with BenchmarkDotNet.AI

This guide will walk you through setting up BenchmarkDotNet.AI and running your first AI benchmark.

## Prerequisites

- .NET 8.0 or later
- BenchmarkDotNet 0.14.0 or later
- An API key for the LLM engine you plan to use (e.g., OpenAI)

## Installation

Install the BenchmarkDotNet.AI NuGet package using the Package Manager Console:

```powershell
Install-Package BenchmarkDotNetWrapper.AI
```

Or using the .NET CLI:

```bash
dotnet add package BenchmarkDotNetWrapper.AI
```

## Basic Usage

Here's a simple example to get you started:

```csharp
using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.AI;
using BenchmarkDotNet.AI.Types;
using BenchmarkDotNet.AI.Types.BenchmarkDotNet.AI.Types;
using LLM;

namespace MyBenchmarkProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create LLM engine options
            var llmOptions = new LlmEngineOptions
            {
                EngineType = typeof(OpenAiEngine),
                ApiKey = "your-openai-api-key" // Replace with your actual API key
            };

            // Run benchmarks with AI analysis
            var summary = await BenchmarkRunner<MyBenchmarks>.Run(llmOptions);
            
            // The summary contains traditional benchmark results plus AI-powered insights
            Console.WriteLine(summary.ToString());
        }
    }

    [MemoryDiagnoser]
    public class MyBenchmarks
    {
        [Benchmark]
        public void StandardBenchmark()
        {
            // Your code to benchmark
            System.Threading.Thread.Sleep(10);
        }
        
        [Benchmark]
        public void AIRelatedBenchmark()
        {
            // AI-related code to benchmark
            System.Threading.Thread.Sleep(50);
        }
    }
}
```

## Understanding Results

When you run the benchmark, you'll get:

1. Standard BenchmarkDotNet performance metrics
2. Additional AI-specific metrics
3. AI-powered code suggestions based on analysis of your benchmark code

## Next Steps

- Explore the [API Documentation](../api/README.md) to learn about available options
- Check out the [Examples](../examples/README.md) for more complex scenarios
- Read the [Configuring OpenAI Engine](configuring-openai.md) guide to customize your OpenAI settings 