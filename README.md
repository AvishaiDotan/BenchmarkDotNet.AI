# BenchmarkDotNet.AI

A BenchmarkDotNet extension for AI benchmarking that helps you measure and analyze the performance of AI-related operations in your .NET applications.

## Features

- Easy integration with BenchmarkDotNet
- Support for AI-specific benchmarking scenarios
- Detailed performance metrics and analysis
- Compatible with .NET 8.0 and later

## Installation

```shell
dotnet add package BenchmarkDotNet.AI
```

## Usage

```csharp
using BenchmarkDotNet.AI;

// Create a benchmark class
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

## Requirements

- .NET 8.0 or later
- BenchmarkDotNet 0.14.0 or later

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

If you encounter any issues or have questions, please open an issue on the GitHub repository. 