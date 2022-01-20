using BenchmarkDotNet.Running;

namespace Rust.UiFramework.Benchmarks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
            //Benchmarks benchmarks = new Benchmarks();
            //benchmarks.Setup();
            //benchmarks.FrameworkBenchmark_WithJson();
        }
    }
}