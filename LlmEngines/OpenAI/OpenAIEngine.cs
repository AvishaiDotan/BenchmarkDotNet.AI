using LLM;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BenchmarkDotNet.AI.LlmEngines.OpenAI
{
    public class OpenAIEngine : ILlmEngine
    {
        private readonly ChatClient client;
        public OpenAIEngine()
        {
            client = new(model: "gpt-4o", apiKey: "");

        }

        public async Task<string> GetCompletionAsync(string req)
        {
            try
            {
                string prompt = PrePrompt();
                prompt += req;
                ChatCompletion completion = await client.CompleteChatAsync(prompt);
                return completion.Content[0].Text ?? "";
            }
            catch
            {
                Console.WriteLine("Error");
                throw;
            }

        }

        private string PrePrompt() => "You are a performance benchmarking expert. You have a BenchmarkContext object with benchmark results, structured as:\r\nBenchmarkContext:\r\nCode: String with benchmark context (may be empty).\r\n\r\nBenchmarkData: List of BenchmarkData objects.\r\n\r\nBenchmarkData:\r\nName: String identifying the benchmark.\r\n\r\nbenchmarkStatistics: Metrics for the benchmark.\r\n\r\nBenchmarkStatistics:\r\nMean: Average execution time.\r\n\r\nError: Margin of error for mean.\r\n\r\nStdDev: Standard deviation of execution times.\r\n\r\nMedian: Median execution time.\r\n\r\nMin: Minimum execution time.\r\n\r\nMax: Maximum execution time.\r\n\r\nTask:\r\nFor each BenchmarkData entry:\r\nSummarize key metrics (Mean, Median, StdDev, Min, Max, Error) in a concise table or list.\r\n\r\nInterpret results briefly, focusing on performance, consistency, and variability.\r\n\r\nFlag notable issues (e.g., high StdDev, large Error, Mean vs. Median mismatch).\r\n\r\nIf Code is non-empty, note its relevance to the benchmarks.\r\n\r\nProvide a short overall summary with key trends and recommendations.\r\n\r\nOutput Format:\r\nUse concise, technical language.\r\n\r\nStructure with headings (Benchmark: <Name>), followed by metrics and interpretation.\r\n\r\nEnd with an \"Overall Summary\" section.\r\n\r\nState assumptions (e.g., time units).\r\n\r\n";
    }
}





