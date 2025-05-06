﻿using System.Text;
using BenchmarkDotNet.Attributes;
using Facepunch;
using Network;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Game.Rust.Cui;

namespace Rust.UiFramework.Benchmarks;

#if BENCHMARKS

using Oxide.Ext.UiFramework.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    private const int Iterations = 100;
    private readonly List<string> _oxideMins = new();
    private readonly List<string> _oxideMaxs = new();
    private readonly List<UiPosition> _frameworkPos = new();
    private readonly Random _random = new();
    public readonly byte[] Buffer = new byte[1024 * 1024];
    private CuiElementContainer _oxideContainer;
    private string _oxideJson;
    private UiBuilder _builder;
    private UiBuilder _randomBuilder;
    private JsonFrameworkWriter _writer;
    private JsonFrameworkWriter _randomWriter;
    private readonly Connection _connection = new();

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

    // [Benchmark]
    // public CuiElementContainer Oxide_CreateContainer()
    // {
    //     return GetOxideContainer();
    // }

    // [Benchmark(Baseline = true)]
    // public UiBuilder UiFramework_CreateContainer()
    // {
    //     UiBuilder builder = GetFrameworkBuilder();
    //     builder.Dispose();
    //     return builder;
    // }

    //
    // [Benchmark]
    // public string Oxide_CreateJson()
    // {
    //     return _oxideContainer.ToJson();
    // }
    //
    // [Benchmark]
    // public JsonFrameworkWriter UiFramework_CreateJson()
    // {
    //     JsonFrameworkWriter writer = _builder.CreateWriter();
    //     writer.Dispose();
    //     return writer;
    // }
    //
    // [Benchmark]
    // public byte[] Oxide_EncodeJson()
    // {
    //     return Encoding.UTF8.GetBytes(_oxideJson);
    // }
    //
    // [Benchmark]
    // public int UiFramework_EncodeJson()
    // {
    //     int count = _writer.WriteTo(Buffer);
    //     return count;
    // }

    [Benchmark(Baseline = true)]
    public void UiFramework_Async()
    {
        UiBuilder builder = GetFrameworkBuilder();
        builder.AddUi(default(SendInfo));
        builder.Dispose();
    }
    
    [Benchmark]
    public void Oxide_Async()
    {
        CuiElementContainer builder = GetOxideContainer();
        builder.AddUiAsync(_connection);
    }
    
    //[Benchmark]
    public void UiFramework_Full()
    {
        UiBuilder builder = GetFrameworkBuilder();
        JsonFrameworkWriter writer = builder.CreateWriter();
        BenchmarkNetWrite write = Pool.Get<BenchmarkNetWrite>();
        writer.WriteToNetwork(write);
        writer.Dispose();
        Pool.Free(ref write);
        builder.Dispose();
    }
    
    [Benchmark]
    public byte[] Oxide_Full()
    {
        CuiElementContainer builder = GetOxideContainer();
        string json = builder.ToJson();
        return Encoding.UTF8.GetBytes(json);
    }
    
    private CuiElementContainer GetOxideContainer()
    {
        CuiElementContainer container = new();
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

    private UiBuilder GetFrameworkBuilder()
    {
        UiBuilder builder = UiBuilder.Create(UiPosition.Full, UiColor.Clear, "123");
        builder.EnsureCapacity(Iterations);
        for (int i = 0; i < Iterations - 1; i++)
        {
            builder.Panel(builder.Root, _frameworkPos[i], UiColor.Black);
        }

        return builder;
    }
}
#endif