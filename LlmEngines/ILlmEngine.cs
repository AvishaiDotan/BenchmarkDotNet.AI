using BenchmarkDotNetWrapper.AI.Types;
using System;
using System.Threading.Tasks;

namespace BenchmarkDotNetWrapper.LLM;


public interface ILlmEngine
{
    Task<string> GetCompletionAsync(BenchmarkContext benchmarkContext);
}