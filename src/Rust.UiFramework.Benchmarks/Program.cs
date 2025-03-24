using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Positions;
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
#if BENCHMARKS
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(10))
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        BenchmarkRunner.Run<Benchmarks>(config, args);
#else

#endif

        // UiPosition start = UiPosition.BottomMiddle;
        // UiPosition end = UiPosition.MiddleMiddle;
        // var bezier = new BezierPoints(1,0.5f,0, 0.5f);
        // //cubic-bezier(.06,-0.81,.61,1.92)
        //
        // for(float f = 0; f <= 1.1; f += 0.01f)
        // {
        //     var pos = Singleton<BezierCurve>.Instance.GetPosition(bezier, start, end, f);
        //     Console.WriteLine($"{f:0.##}: {pos}");
        // }
    }
}