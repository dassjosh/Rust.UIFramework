using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

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
            
            while (true)
            {
                benchmarks.FrameworkBenchmark_WithoutJson();
            }
#else
            BenchmarkRunner.Run<Benchmarks>(config);
            //BenchmarkRunner.Run<PositionBenchmarks>(config);
#endif
        }
    }
}