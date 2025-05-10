# LlmEngineOptions

The `LlmEngineOptions` class provides configuration settings for LLM engines in BenchmarkDotNetWrapper.AI.

## Namespace

`BenchmarkDotNetWrapper.AI.Types`

## Type

`record` - This is an immutable record class in C#.

## Properties

| Property | Type | Description |
|----------|------|-------------|
| `EngineType` | `Type` | Gets or sets the type of the LLM engine to use. Must implement `ILlmEngine`. Will throw an `ArgumentException` if an invalid type is provided. |
| `ApiKey` | `string` | Gets or sets the API key for the LLM service. Default is an empty string. This is used during execution but not stored permanently. |
| `OveridingPrompt` | `string?` | Gets or sets an optional custom prompt that overrides the default prompt. Default is `null`. |

## Usage Example

```csharp
// Configure the LLM engine options
var llmOptions = new LlmEngineOptions
{
    EngineType = typeof(OpenAiEngine),
    ApiKey = "your-openai-api-key", // Replace with your actual API key
    OveridingPrompt = "Analyze this benchmark data and suggest optimizations"
};

// Run the benchmark with AI analysis
var summary = await BenchmarkRunner.Run<MyBenchmarks>().WithAI<MyBenchmarks>(llmOptions);
```

## Remarks

- The API key is used only during execution and is not stored permanently for security reasons.
- The `EngineType` property validates that the provided type implements the `ILlmEngine` interface.
- Custom prompts can be used to tailor the AI analysis to specific scenarios. 