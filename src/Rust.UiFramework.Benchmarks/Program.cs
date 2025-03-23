using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Positions;
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

        UiColor _bodyColor = "#888888C8";
        UiColor _startColor = _bodyColor.WithAlpha(0f);
        byte a = 200;
        var b = a * 0.01f;
        for (int i = 0; i <= 100; i++)
        {
            UiColor lerp = UiColor.Lerp(_startColor, _bodyColor, i / 100f);
        }

    }
}