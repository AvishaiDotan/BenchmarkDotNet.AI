# OpenAi Engine

OpenAi Engine is an implementation of `ILlmEngine` for interacting with OpenAI's API.

## Properties

- `ApiKey` - The OpenAI API key (never stored)

## Usage

To use the OpenAI Engine in your benchmarks, configure it in your LlmEngineOptions:

```csharp
var llmOptions = new LlmEngineOptions
{
    EngineType = typeof(OpenAiEngine),
    ApiKey = "your-openai-api-key" // Replace with your actual API key
};

var summary = await BenchmarkRunner.Run<MyBenchmark>().WithAI<MyBenchmark>(llmOptions);
``` 