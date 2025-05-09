using BenchmarkDotNet.AI.Types;
using System;
using System.Threading.Tasks;

namespace LLM;


public interface ILlmEngine
{
    Task<string> GetCompletionAsync(BenchmarkContext benchmarkContext);
}