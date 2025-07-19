using System.Diagnostics;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace EventBusNet.Benchmark;

internal class Program
{
    static void Main(string[] args)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        if (Regex.IsMatch(baseDir, @"\\bin\\Release\\net[^\\]+\\"))
        {
            var originBaseDir = baseDir;
            for (var i = 0; i < 4; i++)
                baseDir = Path.GetDirectoryName(baseDir);
            baseDir ??= originBaseDir;
        }
        var config = DefaultConfig.Instance.WithArtifactsPath(Path.Combine(baseDir, "BenchmarkArtifacts"));
        var stopwatch = Stopwatch.StartNew();
        BenchmarkRunner.Run<Benchmark_EventBusNet>(config);
        stopwatch.Stop();
        Console.WriteLine($"Benchmark costs: {stopwatch.Elapsed}");
        Console.Read();
    }
}
