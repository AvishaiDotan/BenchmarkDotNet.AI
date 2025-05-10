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

## Simple Usage

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNetWrapper.AI;
using BenchmarkDotNetWrapper.AI.Types;
using BenchmarkDotNetWrapper.AI.LlmEngines.OpenAI;

// Define your benchmark
[MemoryDiagnoser]
public class MyBenchmark
{
    [Benchmark]
    public void MyOperation()
    {
        // Your code to benchmark
    }
}

// Run with AI analysis
var llmOptions = new LlmEngineOptions
{
    EngineType = typeof(OpenAiEngine),
    ApiKey = "your-openai-api-key"
};

var summary = await BenchmarkRunner.Run<MyBenchmark>().WithAI<MyBenchmark>(llmOptions);
```

## Requirements

- .NET 8.0 or later
- BenchmarkDotNetWrapper
- OpenAI API key

## Documentation

For full documentation, see the [Documentation](Docs/README.md) section.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

If you encounter any issues or have questions, please open an issue on the GitHub repository. 