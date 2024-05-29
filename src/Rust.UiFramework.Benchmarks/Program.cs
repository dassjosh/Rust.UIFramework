using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Rust.UiFramework.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(30));
        BenchmarkRunner.Run<Benchmarks>(config, args);
        //
        // Benchmarks b = new Benchmarks();
        // b.Setup();
        // while (true)
        // {
        //     b.UiFramework_Full();
        // }
    }
}