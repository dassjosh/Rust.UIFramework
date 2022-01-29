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

            //while (true)
            //{
                benchmarks.FrameworkBenchmark_WithoutJson();
                benchmarks.IterationCleanup();
            //}
#else
            BenchmarkRunner.Run<Benchmarks>();
#endif
        }
    }
}