using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    public class PositionBenchmarks
    {
        private readonly Vector2 _pos = new Vector2(0.25f, .75f);

        [GlobalSetup]
        public void Setup()
        {
            
        }
        
        [Benchmark]
        public string Position_StringConcat()
        {
            return string.Concat(_pos.x, " ", _pos.y);
        }
        
        [Benchmark(Baseline = true)]
        public string Position_VectorExt()
        {
            return VectorExt.ToString(_pos);
        }
        
        [Benchmark]
        public string Position_StringToString()
        {
            return string.Concat(_pos.x.ToString("0.####"), " ", _pos.y.ToString("0.####"));
        }
        
        [Benchmark]
        public string Position_StringFormatBox()
        {
            return string.Format("{0:0.####} {1:0.####}", _pos.x, _pos.y);
        }
        
        [Benchmark]
        public string Position_StringInterpolation()
        {
            return $"{_pos.x:0.####} {_pos.y:0.####}";
        }
    }
}