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

        KeyFramePositionAnimator         _animator = new KeyFramePositionAnimator(UiPosition.MiddleLeft, UiPosition.MiddleMiddle);
        _animator.AddKeyFrame(10f, new UiPosition(0.25f, 0.75f, 0.25f, 0.75f));
        _animator.AddKeyFrame(20f, UiPosition.TopMiddle);
        _animator.AddKeyFrame(30f, new UiPosition(0.75f, 0.75f, 0.75f, 0.75f));
        _animator.AddKeyFrame(40f, UiPosition.MiddleRight);
        _animator.AddKeyFrame(50f, new UiPosition(0.75f, 0.25f, 0.75f, 0.25f));
        _animator.AddKeyFrame(60f, UiPosition.BottomMiddle);
        _animator.AddKeyFrame(70f, new UiPosition(0.25f, 0.25f, 0.25f, 0.25f));
        _animator.AddKeyFrame(80f, UiPosition.MiddleLeft);
        _animator.AddKeyFrame(90f, UiPosition.MiddleRight);
        
        for(float i = 0f; i < 1f; i += 0.01f)
        {
            Console.WriteLine($"{i:0.00}: {_animator.Get(i)}");
        }
    }
}