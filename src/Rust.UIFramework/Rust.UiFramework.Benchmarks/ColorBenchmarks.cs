using System.Text;
using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    public class ColorBenchmarks
    {
        private readonly UiColor _color = new UiColor(0.25f, .75f, 0.25f, .75f);
        private const string Format = "0.####";
        
        [GlobalSetup]
        public void Setup()
        {
            
        }
        
        [Benchmark]
        public string Position_StringConcat()
        {
            Color color = _color.Color;
            return string.Concat(color.r, " ",color.g, " ", color.b, " ", color.a);
        }
        
        // [Benchmark(Baseline = true)]
        // public string Position_VectorExt()
        // {
        //     return VectorExt.ToString(_color);
        // }
        
        [Benchmark]
        public string Position_StringToString()
        {
            Color color = _color.Color;
            return string.Concat(color.r.ToString(Format), " ",color.g.ToString(Format), " ", color.b.ToString(Format), " ", color.a.ToString(Format));
        }
        
        [Benchmark]
        public string Position_StringFormatBox()
        {
            const string format = "{0:0.####} {1:0.####} {2:0.####} {3:0.####}";
            Color color = _color.Color;
            return string.Format(format, color.r, color.g, color.b, color.a);
        }
        
        [Benchmark]
        public string Position_StringInterpolation()
        {
            Color color = _color.Color;
            return $"{color.r:0.####} {color.g:0.####} {color.b:0.####} {color.a:0.####}";
        }
        
        [Benchmark]
        public string Position_SingleStringBuilder()
        {
            const string format = "{0:0.####} {1:0.####} {2:0.####} {3:0.####}";
            Color color = _color.Color;
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.AppendFormat(format, color.r, color.g, color.b, color.a);
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
        
        [Benchmark]
        public string Position_SingleStringBuilderFormat()
        {
            const string format = "{0} {1} {2} {3}";
            Color color = _color.Color;
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.AppendFormat(format, color.r.ToString(Format), color.g.ToString(Format), color.b.ToString(Format), color.a.ToString(Format));
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
        
        [Benchmark]
        public string Position_MultipleStringBuilder()
        {
            const string rgbFormat = "{0:0.####} ";
            const string aFormat = "{0:0.####}";
            
            Color color = _color.Color;
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.AppendFormat(rgbFormat, color.r);
            builder.AppendFormat(rgbFormat, color.g);
            builder.AppendFormat(rgbFormat, color.b);
            builder.AppendFormat(aFormat, color.a);
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
        
        [Benchmark]
        public string Position_MultipleStringBuilderFormat()
        {
            const string rgbFormat = "{0} ";
            const string aFormat = "{0}";
            
            Color color = _color.Color;
            StringBuilder builder = UiFrameworkPool.GetStringBuilder();
            builder.AppendFormat(rgbFormat, color.r.ToString(Format));
            builder.AppendFormat(rgbFormat, color.g.ToString(Format));
            builder.AppendFormat(rgbFormat, color.b.ToString(Format));
            builder.AppendFormat(aFormat, color.a.ToString(Format));
            return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
        }
    }
}