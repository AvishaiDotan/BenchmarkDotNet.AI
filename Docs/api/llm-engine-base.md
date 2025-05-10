# LlmEngineBase

The `LlmEngineBase` class provides a base implementation for LLM engines in BenchmarkDotNetWrapper.AI.

## Namespace

`BenchmarkDotNetWrapper.AI.LlmEngines`

## Methods

- `PrePrompt()` - Protected virtual method that returns the default prompt for the LLM engine. This prompt defines the context and instructions for analyzing benchmark results.

## Usage

Extend this class to create custom LLM engines with common functionality already implemented. When creating a custom engine, you'll need to:

1. Inherit from `LlmEngineBase`
2. Implement any required interface for your LLM engine
3. Optionally override the `PrePrompt()` method to customize the prompt

```csharp
public class CustomEngine : LlmEngineBase
{
    public async Task<string> AnalyzeBenchmarksAsync(BenchmarkContext benchmarkContext)
    {
        // Get the default prompt
        string prompt = PrePrompt();
        
        // Implement custom logic to process the prompt and context
        
        return "Analysis results";
    }
}
``` 

## Default Prompt

The default prompt provided by `PrePrompt()` instructs the LLM to:

1. Analyze each benchmark's metrics (Mean, Median, StdDev, Min, Max, Error)
2. Explain performance characteristics and identify anomalies
3. Analyze code (if provided) and suggest improvements
4. Provide an overall summary with trends and recommendations
