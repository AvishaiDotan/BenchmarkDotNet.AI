using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Text;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using BenchmarkDotNet.AI.LlmEngines.OpenAI;
using BenchmarkDotNet.AI.Helpers;
using BenchmarkDotNet.AI.Types;
using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;
using LLM;
using BenchmarkDotNet.AI.LlmEngines.Common;
using BenchmarkDotNet.Configs;
using Perfolizer.Mathematics.Common;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.AI.Types.BenchmarkDotNet.AI.Types;

namespace BenchmarkDotNet.AI;

public class BenchmarkRunner<T> where T : class
{
    public static async Task<Summary> Run(LlmEngineOptions options, IConfig? config = null, string[]? args = null)
    {
        ILlmEngine engine = LlmEngineSwitcher.GetEngine(options.EngineType, options.ApiKey);
        string code = GetCodeAsText<T>.Get();
        var summary = BenchmarkRunner.Run<T>(config, args);

        var benchmarkContext = new BenchmarkContext()
        {
            BenchmarkData = summary.Reports.Select(r => new BenchmarkData()
            {
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
                },
                Name = r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo,
            }).ToList(),
            Code = code
        };
        string reason = await engine.GetCompletionAsync(benchmarkContext);
        Console.WriteLine(reason);
        return summary;
    }
}