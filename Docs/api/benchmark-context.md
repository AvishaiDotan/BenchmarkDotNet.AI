# BenchmarkContext

The `BenchmarkContext` class provides context information for benchmark runs in BenchmarkDotNetWrapper.AI.

## Properties

- `Code` - The code being benchmarked (string)
- `BenchmarkData` - A list of benchmark data (List<BenchmarkData>)

## Usage

The `BenchmarkContext` is automatically created and populated during benchmark execution. It provides important context information that can be used by LLM engines for analysis and reporting.

```csharp
// Example of accessing benchmark context in a custom LLM engine
public class CustomEngine : LlmEngineBase
{
    protected override async Task<string> AnalyzeBenchmarkAsync(BenchmarkContext context)
    {
        var benchmarkCount = context.BenchmarkData.Count;
        return $"Analyzed {benchmarkCount} benchmark results for code: {context.Code.Substring(0, 20)}...";
    }
}
``` 