# BenchmarkData

The `BenchmarkData` class represents the data structure for benchmark information in BenchmarkDotNetWrapper.AI.

## Properties

- `Name` - The name of the benchmark
- `benchmarkStatistics` - Statistical data from the benchmark run

## Usage

The `BenchmarkData` class is used to store and transport benchmark results between different components of the system.

```csharp
// Example of creating benchmark data
var data = new BenchmarkData
{
    Name = "StringProcessingBenchmark",
    benchmarkStatistics = new BenchmarkStatistics
    {
        Mean = 150.5,
        StandardDeviation = 2.3
    }
};
``` 