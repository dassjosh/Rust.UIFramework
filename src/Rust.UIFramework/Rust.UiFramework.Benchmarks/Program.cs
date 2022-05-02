using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Rust.UiFramework.Benchmarks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ManualConfig config = ManualConfig
                                  .Create(DefaultConfig.Instance)
                                  .WithOptions(ConfigOptions.DisableOptimizationsValidator);
#if DEBUG
            Benchmarks benchmarks = new Benchmarks();
            benchmarks.Setup();

            var position = Marshal.SizeOf(typeof(Position));
            var offset = Marshal.SizeOf(typeof(Offset));
            var color = Marshal.SizeOf(typeof(UiColor));
            
            
            
            while (true)
            {
               var a = benchmarks.FrameworkBenchmark_WithJson();
            }
#else
            BenchmarkRunner.Run<Benchmarks>(config);
            //BenchmarkRunner.Run<PositionBenchmarks>(config);
            //BenchmarkRunner.Run<ColorBenchmarks>(config);
#endif
        }
    }
}