namespace Rust.UiFramework.Benchmarks;

class Program
{
    static void Main(string[] args)
    {
#if BENCHMARKS
        ManualConfig config = DefaultConfig.Instance.AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithIterationCount(30));
        BenchmarkRunner.Run<Benchmarks>(config, args);
#else
        
#endif
        
        byte[] a = BitConverter.GetBytes(int.MaxValue);
        string b = Convert.ToBase64String(a);
    }
}