using BenchmarkDotNet.Running;

namespace Rust.UiFramework.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}