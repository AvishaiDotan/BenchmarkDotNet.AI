using System;
using System.Threading.Tasks;

namespace LLM;


public interface ILlmEngine
{
    Task<string> GetCompletionAsync(string req);
}