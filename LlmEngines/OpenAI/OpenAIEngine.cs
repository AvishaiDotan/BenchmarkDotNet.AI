using BenchmarkDotNet.AI.Types;
using LLM;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BenchmarkDotNet.AI.LlmEngines.OpenAI
{
    public class OpenAIEngine : LlmEngineBase, ILlmEngine
    {
        private readonly ChatClient _client;
        private readonly string? _overridingPrompt = null;
        public OpenAIEngine(string apiKey, string? overridingPrompt = null)
        {
            _client = new(model: "gpt-4o", apiKey);
            _overridingPrompt = overridingPrompt;

        }

        public async Task<string> GetCompletionAsync(BenchmarkContext benchmarkContext)
        {
            try
            {
                string prompt = _overridingPrompt ?? PrePrompt(); 
                CompletionRequestPayload req = new()
                {
                    Prompt = prompt,
                    benchmarkContext = benchmarkContext
                };

                string jsonStr = JsonSerializer.Serialize(req, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                });

                ChatCompletion completion = await _client.CompleteChatAsync(jsonStr);
                return completion.Content[0].Text ?? "";
            }
            catch
            {
                Console.WriteLine("Error");
                throw;
            }

        }



    }
}





