# ILlmEngine

The `ILlmEngine` interface defines the contract for all LLM (Large Language Model) engine implementations in BenchmarkDotNetWrapper.AI.

## Methods

- `GetCompletionAsync(BenchmarkContext benchmarkContext)` - Gets completion from the LLM based on benchmark context

## Usage

Implement this interface to create custom LLM engines for different AI providers.

```csharp
public class CustomEngine : ILlmEngine
{
    public async Task<string> GetCompletionAsync(BenchmarkContext benchmarkContext)
    {
        // Implement custom analysis logic
        return "Analysis results";
    }
}
``` 