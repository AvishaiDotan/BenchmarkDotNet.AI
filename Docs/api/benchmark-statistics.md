# BenchmarkStatistics

The `BenchmarkStatistics` class provides statistical data from benchmark runs in BenchmarkDotNetWrapper.AI.

## Properties

- `Mean` - The mean execution time
- `StandardDeviation` - The standard deviation of execution times
- `Min` - The minimum execution time
- `Max` - The maximum execution time
- `Median` - The median execution time

## Usage

The `BenchmarkStatistics` class is used to store and analyze statistical data from benchmark runs.

```csharp
// Example of accessing benchmark statistics
var stats = new BenchmarkStatistics
{
    Mean = 150.5,
    StandardDeviation = 2.3,
    Min = 145.0,
    Max = 155.0,
    Median = 150.0
};
``` 