using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
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
        
        private CuiElementContainer GetRandomOxideContainer()
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
                        AnchorMin = $"{(float)_random.NextDouble()} {(float)_random.NextDouble()}",
                        AnchorMax = $"{(float)_random.NextDouble()} {(float)_random.NextDouble()}"
                    }
                });
            }

            return container;
        }
        
        [Benchmark]
        public CuiElementContainer OxideBenchmark_WithoutJson()
        {
            return GetOxideContainer();
        }
        
        [Benchmark]
        public UiBuilder FrameworkBenchmark_WithoutJson()
        {
            UiBuilder builder = GetFrameworkBuilder();
            builder.Dispose();
            return builder;
        }
        
        [Benchmark(Baseline = true)]
        public string OxideBenchmark_WithJson()
        {
            return GetOxideContainer().ToJson();
        }
        
        [Benchmark]
        public string FrameworkBenchmark_WithJson()
        {
            UiBuilder builder = GetFrameworkBuilder();
            string json = builder.ToJson();
            builder.Dispose();
            return json;
        }
        
        //[Benchmark]
        public CuiElementContainer OxideBenchmark_RandomPos_WithoutJson()
        {
            return GetRandomOxideContainer();
        }
        
        //[Benchmark]
        public UiBuilder FrameworkBenchmark_RandomPos_WithoutJson()
        {
            UiBuilder builder = GetRandomPositionBuilder();
            builder.Dispose();
            return builder;
        }
        
        //[Benchmark]
        public string OxideBenchmark_RandomPos_WithJson()
        {
            return GetRandomOxideContainer().ToJson();
        }

        //[Benchmark]
        public string FrameworkBenchmark_RandomPos_WithJson()
        {
            UiBuilder builder = GetRandomPositionBuilder();
            string json = builder.ToJson();
            builder.Dispose();
            return json;
        }

        private UiBuilder GetFrameworkBuilder()
        {
            UiBuilder builder = new UiBuilder(UiColors.StandardColors.Clear, UiPosition.Full, "123");
            builder.EnsureCapacity(Iterations);
            for (int i = 0; i < Iterations - 1; i++)
            {
                builder.Panel(builder.Root, UiColors.StandardColors.Black, _frameworkPos[i]);
            }

            return builder;
        }

        private UiBuilder GetRandomPositionBuilder()
        {
            MovablePosition move = new MovablePosition(0, 0, 0, 0);

            UiBuilder builder = new UiBuilder(UiColors.StandardColors.Clear, UiPosition.Full, "123");
            for (int i = 0; i < Iterations - 1; i++)
            {
                move.Set((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());
                builder.Panel(builder.Root, UiColors.StandardColors.Black, move);
            }

            return builder;
        }
    }
}