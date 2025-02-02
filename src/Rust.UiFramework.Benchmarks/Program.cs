using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Rust.UiFramework.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
#if BENCHMARKS
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(30))
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        BenchmarkRunner.Run<Benchmarks>(config, args);
#else
         Benchmarks benchmarks = new Benchmarks();
         benchmarks.Setup();
         benchmarks.GetOxideContainer();
        //var a = benchmarks.UiFramework_Writer1();
#endif
    }
}