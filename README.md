# BenchmarkDotNetWrapper.AI

A BenchmarkDotNet extension for AI model benchmarking that helps you measure and analyze the performance of AI-related operations in your .NET applications.

[![NuGet](https://img.shields.io/nuget/v/BenchmarkDotNetWrapper.AI.svg)](https://www.nuget.org/packages/BenchmarkDotNetWrapper.AI)

## Features

- Easy integration with BenchmarkDotNet
- Support for AI-specific benchmarking scenarios including OpenAI models
- Detailed performance metrics and analysis
- API keys are never stored - only used temporarily for valid providers
- Code optimization suggestions powered by AI
- Compatible with .NET 8.0 and later

## Installation

```shell
dotnet add package BenchmarkDotNetWrapper.AI
```

## Usage

### Basic Usage

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.AI;
using BenchmarkDotNet.AI.Types.BenchmarkDotNet.AI.Types;
using LLM;

[MemoryDiagnoser]
public class MyBenchmark
{
    [Benchmark]
    public void MyOperation()
    {
        // Your code to benchmark
    }
}

// Run the benchmark
var summary = BenchmarkRunner.Run<MyBenchmark>();
```

### AI-Powered Code Analysis

```csharp
using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.AI;
using BenchmarkDotNet.AI.Types.BenchmarkDotNet.AI.Types;
using BenchmarkDotNet.AI.LlmEngines.OpenAI;
using LLM;

[MemoryDiagnoser]
public class MyBenchmark
{
    [Benchmark]
    public void SlowMethod()
    {
        // Inefficient code to analyze
        var list = new List<int>();
        for (int i = 0; i < 1000000; i++)
        {
            list.Add(i);
            // Inefficient implementation
        }
    }
}

// Run the benchmark with AI analysis
var llmOptions = new LlmEngineOptions
{
    EngineType = typeof(OpenAiEngine),
    ApiKey = "your-openai-api-key", // Never stored, only used during execution
    OveridingPrompt = null // Use the default prompt
};

var summary = await BenchmarkRunner<MyBenchmark>.Run(llmOptions);
```

### Complete Implementation Example

Here's a complete implementation example showing how to set up and use the AI benchmarking features:

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.AI;
using BenchmarkDotNet.AI.Types.BenchmarkDotNet.AI.Types;
using BenchmarkDotNet.AI.LlmEngines.OpenAI;
using LLM;

namespace MyBenchmarkProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Configure the LLM engine options
            var llmOptions = new LlmEngineOptions
            {
                EngineType = typeof(OpenAiEngine),
                ApiKey = "your-openai-api-key", // Never stored permanently
                OveridingPrompt = "Analyze this .NET code for performance issues and suggest specific optimizations"
            };

            var summary = await BenchmarkRunner<StringProcessingBenchmark>.Run(llmOptions);
        }
    }

    [MemoryDiagnoser]
    public class StringProcessingBenchmark
    {
        private readonly List<string> _sampleData = new();
        private const int SampleSize = 10000;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            for (int i = 0; i < SampleSize; i++)
            {
                var length = random.Next(10, 100);
                var chars = new char[length];
                for (int j = 0; j < length; j++)
                {
                    chars[j] = (char)random.Next(97, 123); // a-z
                }
                _sampleData.Add(new string(chars));
            }
        }

        [Benchmark(Baseline = true)]
        public int ProcessStringsWithConcat()
        {
            var result = string.Empty;
            foreach (var str in _sampleData)
            {
                result += str.ToUpper();
            }
            return result.Length;
        }

        [Benchmark]
        public int ProcessStringsWithStringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var str in _sampleData)
            {
                sb.Append(str.ToUpper());
            }
            return sb.Length;
        }

        [Benchmark]
        public int ProcessStringsWithLinq()
        {
            var result = string.Join("", _sampleData.Select(s => s.ToUpper()));
            return result.Length;
        }
    }
}
```

## Configuration Options

### LlmEngineOptions

The `LlmEngineOptions` class provides the configuration for the LLM engine:

| Option | Description | Default |
|--------|-------------|---------|
| `EngineType` | Type that implements `ILlmEngine` | Required |
| `ApiKey` | Your AI provider API key (never stored) | Empty string |
| `OveridingPrompt` | Custom prompt to override the default | null |

## Default Prompt

BenchmarkDotNet.AI includes a default prompt system that instructs the LLM to analyze the benchmark results. You can view the [full documentation on the default prompt](Docs/api/default-prompt.md) for more details.

## Requirements

- .NET 8.0 or later
- BenchmarkDotNet 0.14.0 or later
- OpenAI API key (for OpenAI provider)

## Documentation

For full documentation, see the [Documentation](Docs/README.md) section.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

If you encounter any issues or have questions, please open an issue on the GitHub repository. 