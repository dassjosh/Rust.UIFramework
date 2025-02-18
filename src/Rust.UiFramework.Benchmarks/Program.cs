
using Oxide.Ext.UiFramework.Extensions;

#if BENCHMARKS
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
#endif

namespace Rust.UiFramework.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
#if BENCHMARKS
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(10));
        BenchmarkRunner.Run<Benchmarks>(config, args);
#else
        
#endif
        Random random = new Random();
        while (true)
        {
            int a = random.Next(int.MinValue, int.MaxValue);
            string b = a.ToBase64Span().ToString();
            string c = a.ToString();
            string d = $"{b.Length} - {c.Length}";
        }
    }
}