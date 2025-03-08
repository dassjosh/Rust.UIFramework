using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
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
            .WithIterationCount(10))
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        BenchmarkRunner.Run<Benchmarks>(config, args);
#else



#endif
        var a = UiHelpers.CalculateMaxPage(1730, 24);

    }
}