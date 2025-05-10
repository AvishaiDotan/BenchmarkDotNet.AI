# BenchmarkRunner Extensions

The `BenchmarkRunner` extensions provide methods for adding AI analysis to benchmark results in BenchmarkDotNetWrapper.AI.

## Extension Methods

- `WithAI<T>(LlmEngineOptions options)` - Applies AI analysis to the benchmark summary using the specified LLM engine options

## Usage

Use the extension method to apply AI analysis to benchmark results.

```csharp
// Configure the LLM engine options
var llmOptions = new LlmEngineOptions
{
    EngineType = typeof(OpenAiEngine),
    ApiKey = "your-openai-api-key",
    OveridingPrompt = "Analyze this benchmark data"
};

// Run the benchmark and apply AI analysis
var summary = await BenchmarkRunner.Run<MyBenchmark>().WithAI<MyBenchmark>(llmOptions);
``` 