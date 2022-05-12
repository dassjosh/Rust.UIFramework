using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
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

            while (true)
            {
                //var buffer = benchmarks.Buffer;
                 benchmarks.Setup();
               //var text = Encoding.UTF8.GetString(buffer, 0, count);
               //var length = text.Length;
               //var text1 = Encoding.UTF8.GetBytes(text);
            }
#else
            BenchmarkRunner.Run<Benchmarks>(config);
            //BenchmarkRunner.Run<PositionBenchmarks>(config);
            //BenchmarkRunner.Run<ColorBenchmarks>(config);
#endif
        }
    }
}