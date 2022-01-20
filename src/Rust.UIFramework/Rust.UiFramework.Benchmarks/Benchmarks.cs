using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Oxide.Game.Rust.Cui;
using UI.Framework.Rust.Builder;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Positions;
using UI.Framework.Rust.UiElements;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(1, 15, 30, Invocations)]
    public class Benchmarks
    {
        private const int Invocations = 192;
        private const int Iterations = 1000;
        private readonly List<string> _oxideMins = new List<string>();
        private readonly List<string> _oxideMaxs = new List<string>();
        private readonly List<UiPosition> _frameworkPos = new List<UiPosition>();
        private UiBuilder _builder;
        
        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();
            for (int i = 0; i < Iterations; i++)
            {
                float xMin = (float)random.NextDouble();
                float xMax = (float)random.NextDouble();
                float yMin = (float)random.NextDouble();
                float yMax = (float)random.NextDouble();
                _oxideMins.Add($"{xMin} {yMin}");
                _oxideMaxs.Add($"{xMax} {yMax}");
                _frameworkPos.Add(new StaticUiPosition(xMin, yMin, xMax, yMax));
            }

            const int bufferSize = Iterations * (Invocations + 1);
            Facepunch.Pool.ResizeBuffer<List<BaseUiComponent>>(bufferSize);
            Facepunch.Pool.ResizeBuffer<UiPanel>(bufferSize);
            Facepunch.Pool.ResizeBuffer<ImageComponent>(bufferSize);
            Facepunch.Pool.FillBuffer<UiPanel>(bufferSize);
            Facepunch.Pool.FillBuffer<ImageComponent>(bufferSize);
            Facepunch.Pool.FillBuffer<List<BaseUiComponent>>(bufferSize);
            UiColor _ = UiColors.Black;
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _builder?.Dispose();
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
                        AnchorMin = _oxideMins[i],
                        AnchorMax = _oxideMaxs[i]
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
        
        [Benchmark(Baseline = true)]
        public string OxideBenchmark_WithJson()
        {
            return GetOxideContainer().ToJson();
        }
        
        [Benchmark]
        public UiBuilder FrameworkBenchmark_WithoutJson()
        {
            return GetFrameworkBuilder();
        }

        [Benchmark]
        public string FrameworkBenchmark_WithJson()
        {
            return GetFrameworkBuilder().ToJson();
        }

        private UiBuilder GetFrameworkBuilder()
        {
            UiBuilder builder = new UiBuilder(UiColors.Clear, UiPosition.FullPosition, false, "123");
            for (int i = 0; i < Iterations; i++)
            {
                builder.Panel(builder.Root, UiColors.Black, _frameworkPos[i]);
            }

            _builder = builder;

            return builder;
        }
    }
}