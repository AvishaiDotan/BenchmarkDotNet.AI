using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BenchmarkDotNetWrapper.AI.LlmEngines
{
    public abstract class LlmEngineBase
    {
        protected virtual string PrePrompt() => @"
                        You are a performance benchmarking expert. You are analyzing a BenchmarkContext object, which contains benchmark results and optionally source code. The structure is as follows:

                        BenchmarkContext:
                        - Code: A string containing relevant benchmarked code (can be empty).
                        - BenchmarkData: A list of BenchmarkData entries.

                        Each BenchmarkData contains:
                        - Name: The identifier for the benchmark.
                        - BenchmarkStatistics:
                            • Mean: Average execution time
                            • Error: Margin of error for the mean
                            • StdDev: Standard deviation
                            • Median: Median execution time
                            • Min: Minimum execution time
                            • Max: Maximum execution time
                            • Q3: Third quartile
                            • Kurtosis: Measure of the peakness of the distribution
                            • InterquartileRange: Difference between Q3 and Q1
                            • LowerFence: Lower bound for outliers
                            • UpperFence: Upper bound for outliers
                            • Variance: Variability of the data
                            • Skewness: Measure of asymmetry in the distribution
                            • Q1: First quartile
                            • Count: Number of data points

                        Task:
                        1. For each BenchmarkData entry:
                           - Present the benchmark metrics (Mean, Median, StdDev, Min, Max, Error) in a clean table or list.
                           - Explain what the statistics reveal about performance, consistency, and variability.
                           - Identify anomalies or concerns (e.g., high StdDev or Error, large gaps between Mean and Median).

                        2. If 'Code' is non-empty:
                           - Analyze which parts of the code might be contributing to the performance characteristics.
                           - Suggest concrete code-level improvements based on the observed benchmark data.

                        3. Provide a final section titled ""Overall Summary"" with:
                           - Key trends across all benchmarks
                           - Any general issues detected
                           - High-level performance recommendations

                        Output Format:
                        - Use structured headings (e.g., Benchmark: <Name>)
                        - Use concise, technical language
                        - Clearly state any assumptions (e.g., time units)
                        ".Trim();
                            }
}