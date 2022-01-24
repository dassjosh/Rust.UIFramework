using BenchmarkDotNet.Running;

namespace Rust.UiFramework.Benchmarks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Benchmarks benchmarks = new Benchmarks();
            benchmarks.Setup();
            benchmarks.FrameworkBenchmark_WithoutJson();
#else
            BenchmarkRunner.Run<Benchmarks>();
#endif
        }
    }
}