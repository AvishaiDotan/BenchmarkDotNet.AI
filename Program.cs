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

namespace BenchmarkDotNet.AI;

public class Run<T> where T : class
{
    public static async Task<string> Main()
    {
        var summary = BenchmarkRunner.Run<T>();

        var benchmarkContext = new BenchmarkContext()
        {
            BenchmarkData = summary.Reports.Select(r => new BenchmarkData()
            {
                benchmarkStatistics = new BenchmarkStatistics()
                {
                    Max = r.ResultStatistics?.Max ?? 0,
                    Min = r.ResultStatistics?.Min ?? 0,
                    Mean = r.ResultStatistics?.Mean ?? 0,
                    Error = r.ResultStatistics?.StandardError ?? 0,
                    StdDev = r.ResultStatistics?.StandardDeviation ?? 0,
                    Median = r.ResultStatistics?.Median ?? 0,
                },
                Name = r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo,
            }).ToList(),
            Code = GetCodeAsText<T>.Get()
        };

        return await new OpenAIEngine().GetCompletionAsync(JsonSerializer.Serialize(benchmarkContext));
    }
}

public class BenchmarkStatistics
{
    public double Mean { get; set; }
    public double Error { get; set; }
    public double StdDev { get; set; }
    public double Median { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
}

public class BenchmarkData 
{
    public string Name { get; set; }
    public BenchmarkStatistics benchmarkStatistics { get; set; }
}

public class BenchmarkContext
{
    public string Code = "";
    public List<BenchmarkData> BenchmarkData { get; set; }
    
}

public static class GetCodeAsText<T> where T : class
{
    public static string Get()
    {
        Type type = typeof(T);
        string sourceCode = string.Empty;
        
        try
        {
            // Get the assembly where the type is defined
            Assembly assembly = type.Assembly;
            
            // Get the file path from the assembly location
            string assemblyLocation = assembly.Location;
            string? assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            
            // Try to find the source file based on type name (assuming file name matches class name)
            string potentialFileName = type.Name + ".cs";
            string[] possibleSourceFiles = Directory.GetFiles(
                Directory.GetCurrentDirectory(), 
                potentialFileName, 
                SearchOption.AllDirectories);
            
            if (possibleSourceFiles.Length > 0)
            {
                // Read the source code from the file
                sourceCode = File.ReadAllText(possibleSourceFiles[0]);
            }
            else
            {
                // If we can't find the file, use decompilation to get a better representation
                try
                {
                    // Use ICSharpCode.Decompiler to get the decompiled code
                    var decompiler = new CSharpDecompiler(assemblyLocation, new DecompilerSettings());
                    string typeFullName = type.FullName;
                    
                    // Decompile the entire type
                    var typeDefinition = decompiler.TypeSystem.FindType(new FullTypeName(typeFullName));
                    if (typeDefinition != null)
                    {
                        // Get the decompiled code for the specific type
                        sourceCode = decompiler.DecompileTypeAsString(new FullTypeName(typeDefinition.FullName));
                    }
                    else
                    {
                        // Fallback to reflection-based approach if decompilation fails
                        sourceCode = GenerateCodeFromReflection(type);
                    }
                }
                catch (Exception ex)
                {
                    // If decompilation fails, fall back to reflection-based approach
                    sourceCode = GenerateCodeFromReflection(type);
                    sourceCode = $"// Decompilation failed: {ex.Message}\n" + sourceCode;
                }
            }
        }
        catch (Exception ex)
        {
            sourceCode = $"// Error retrieving source code: {ex.Message}";
        }
        
        return sourceCode;
    }

    private static string GenerateCodeFromReflection(Type type)
    {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine($"// Source code for {type.FullName}");
        sb.AppendLine($"public class {type.Name}");
        sb.AppendLine("{");
        
        // Get all properties
        foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            sb.AppendLine($"    public {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        
        // Get all fields
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            sb.AppendLine($"    public {field.FieldType.Name} {field.Name};");
        }
        
        // Get all methods
        foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            // Skip property accessors
            if (method.IsSpecialName)
                continue;
                
            StringBuilder methodSignature = new StringBuilder();
            methodSignature.Append($"    public {method.ReturnType.Name} {method.Name}(");
            
            // Add parameters
            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                    methodSignature.Append(", ");
                    
                ParameterInfo param = parameters[i];
                methodSignature.Append($"{param.ParameterType.Name} {param.Name}");
            }
            
            methodSignature.Append(")");
            sb.AppendLine(methodSignature.ToString());
            sb.AppendLine("    {");
            sb.AppendLine("        // Method body not available through pure reflection");
            sb.AppendLine("        // Use decompilation to see actual implementation");
            sb.AppendLine("    }");
        }
        
        // Get nested types recursively
        foreach (Type nestedType in type.GetNestedTypes())
        {
            sb.AppendLine($"    // Nested type: {nestedType.Name}");
            sb.AppendLine($"    public class {nestedType.Name}");
            sb.AppendLine("    {");
            sb.AppendLine("        // Nested type members not shown");
            sb.AppendLine("    }");
        }
        
        sb.AppendLine("}");
        return sb.ToString();
    }
}
