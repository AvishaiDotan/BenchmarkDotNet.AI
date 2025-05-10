using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Text;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using BenchmarkDotNetWrapper.AI.LlmEngines.OpenAI;
using BenchmarkDotNetWrapper.AI.Helpers;
using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;
using BenchmarkDotNetWrapper.AI.LlmEngines.Common;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNetWrapper.AI.Types;
using BenchmarkDotNetWrapper.LLM;

namespace BenchmarkWrapper.AI
{
    public static class AIBenchmarkExtensions
    {
        public static async Task<Summary> WithAI<T>(this Summary summary, LlmEngineOptions options)
            where T : class
        {
            ILlmEngine engine = LlmEngineSwitcher.GetEngine(options.EngineType, options.ApiKey);
            string code = GetCodeAsText<T>.Get();

            var benchmarkContext = new BenchmarkContext()
            {
                Code = code,
                BenchmarkData = summary.Reports.Select(r => new BenchmarkData()
                {
                    Name = r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo,
                    benchmarkStatistics = new BenchmarkStatistics()
                    {
                        Max = r.ResultStatistics?.Max,
                        Min = r.ResultStatistics?.Min,
                        Mean = r.ResultStatistics?.Mean,
                        Error = r.ResultStatistics?.StandardError,
                        StdDev = r.ResultStatistics?.StandardDeviation,
                        Median = r.ResultStatistics?.Median,
                        Q3 = r.ResultStatistics?.Q3,
                        Kurtosis = r.ResultStatistics?.Kurtosis,
                        InterquartileRange = r.ResultStatistics?.InterquartileRange,
                        LowerFence = r.ResultStatistics?.LowerFence,
                        UpperFence = r.ResultStatistics?.UpperFence,
                        Variance = r.ResultStatistics?.Variance,
                        Skewness = r.ResultStatistics?.Skewness,
                        Q1 = r.ResultStatistics?.Q1,
                        Count = r.ResultStatistics?.N,
                    }
                }).ToList()
            };

            string reason = await engine.GetCompletionAsync(benchmarkContext);
            Console.WriteLine(reason);

            return summary;
        }
    }
}