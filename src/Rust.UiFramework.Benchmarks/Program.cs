
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
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
        Benchmarks benchmarks = new();
        benchmarks.Setup();
        var pool = Singleton<UiPool>.Instance;
        benchmarks.UiFramework_Async();
        benchmarks.UiFramework_Async();
        benchmarks.UiFramework_Async();
        benchmarks.GlobalCleanup();
        // while (true)
        // {
        //     var a = Task.Run(() =>
        //     {
        //         for (int i = 0; i < 65536; i++)
        //         {
        //             benchmarks.UiFramework_Async();
        //         }
        //     });
        //     
        //     var b = Task.Run(() =>
        //     {
        //         for (int i = 0; i < 65536; i++)
        //         {
        //             benchmarks.UiFramework_CreateContainer();
        //         }
        //     });
        //
        //     Task.WaitAll(a, b);
        // }

#if BENCHMARKS
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(10))
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        BenchmarkRunner.Run<Benchmarks>(config, args);
#endif
    }
}