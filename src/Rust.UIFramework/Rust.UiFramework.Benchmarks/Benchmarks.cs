using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Game.Rust.Cui;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private const int Iterations = 100;
        private readonly List<string> _oxideMins = new List<string>();
        private readonly List<string> _oxideMaxs = new List<string>();
        private readonly List<UiPosition> _frameworkPos = new List<UiPosition>();
        private readonly Random _random = new Random();
        private readonly MovablePosition _move = new MovablePosition(0, 0, 0, 0);
        public readonly byte[] Buffer = new byte[1024 * 1024];
        private CuiElementContainer _oxideContainer;
        private string _oxideJson;
        private UiBuilder _builder;
        //private UiBuilder _randomBuilder;
        private JsonFrameworkWriter _writer;
        //private JsonFrameworkWriter _randomWriter;

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < Iterations; i++)
            {
                float xMin = (float)_random.NextDouble();
                float xMax = (float)_random.NextDouble();
                float yMin = (float)_random.NextDouble();
                float yMax = (float)_random.NextDouble();
                _oxideMins.Add($"{xMin} {yMin}");
                _oxideMaxs.Add($"{xMax} {yMax}");
                _frameworkPos.Add(new UiPosition(xMin, yMin, xMax, yMax));
            }
            
            _oxideContainer = GetOxideContainer();
            _oxideJson = _oxideContainer.ToJson();
            _builder = GetFrameworkBuilder();
            //_randomBuilder = GetRandomPositionBuilder();
            _writer = _builder.CreateWriter();
            //_randomWriter = _randomBuilder.CreateWriter();
        }

        [Benchmark]
        public CuiElementContainer Oxide_CreateContainer()
        {
            return GetOxideContainer();
        }
        
        [Benchmark]
        public UiBuilder Framework_CreateContainer()
        {
            UiBuilder builder = GetFrameworkBuilder();
            builder.Dispose();
            return builder;
        }
        
        [Benchmark]
        public string Oxide_CreateJson()
        {
            return _oxideContainer.ToJson();
        }
        
        [Benchmark]
        public JsonFrameworkWriter Framework_CreateJson()
        {
            JsonFrameworkWriter writer = _builder.CreateWriter();
            writer.Dispose();
            return writer;
        }
        
        [Benchmark]
        public byte[] Oxide_EncodeJson()
        {
            return Encoding.UTF8.GetBytes(_oxideJson);
        }
        
        [Benchmark]
        public int Framework_EncodeJson()
        {
            int count = _writer.WriteTo(Buffer);
            return count;
        }
        
        [Benchmark(Baseline = true)]
        public byte[] Oxide_Full()
        {
            var builder = GetOxideContainer();
            string json = builder.ToJson();
            return Encoding.UTF8.GetBytes(json);
        }
        
        [Benchmark]
        public int Framework_Full()
        {
            UiBuilder builder = GetFrameworkBuilder();
            int count = builder.WriteBuffer(Buffer);
            builder.Dispose();
            return count;
        }

        private CuiElementContainer GetOxideContainer()
        {
            CuiElementContainer container = new CuiElementContainer();
            for (int i = 0; i < Iterations; i++)
            {
                container.Add(new CuiPanel
                {
                    Image =
                    {
                        Color = "1.0 1.0 1.0 1.0"
                    },
                    RectTransform =
                    {
                        AnchorMin = _oxideMins[i % 10],
                        AnchorMax = _oxideMaxs[i % 10]
                    }
                });
            }

            return container;
        }

        private UiBuilder GetFrameworkBuilder()
        {
            UiBuilder builder = UiBuilder.Create(UiColors.StandardColors.Clear, UiPosition.Full, "123");
            builder.EnsureCapacity(Iterations);
            for (int i = 0; i < Iterations - 1; i++)
            {
                builder.Panel(builder.Root, UiColors.StandardColors.Black, _frameworkPos[i]);
            }

            return builder;
        }
    }
}