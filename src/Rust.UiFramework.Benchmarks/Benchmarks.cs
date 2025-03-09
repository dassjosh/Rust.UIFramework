
using Oxide.Ext.UiFramework.Enums;

namespace Rust.UiFramework.Benchmarks;

#if BENCHMARKS
using BenchmarkDotNet.Attributes;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Game.Rust.Cui;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Builder.UI;

[MemoryDiagnoser]
public class Benchmarks
{
    private const int Iterations = 100;
    private readonly List<string> _oxideMins = new();
    private readonly List<string> _oxideMaxs = new();
    private readonly List<UiPosition> _frameworkPos = new();
    private Random _random;
    public readonly byte[] Buffer = new byte[1024 * 1024];
    private CuiElementContainer _oxideContainer;
    private string _oxideJson;
    private UiBuilder _builder;
    private CachedUiBuilder _cached;
    private UiBuilder _randomBuilder;
    private JsonFrameworkWriter _writer;
    private JsonFrameworkWriter _randomWriter;
    private readonly Connection _connection = new();

    //[Params(2048, 4096, 8192)] public int ArraySize;

    [GlobalSetup]
    public void Setup()
    {
        _random = new(1234);
        //JsonBinaryWriter.SegmentSize = ArraySize;
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
        _cached = GetFrameworkBuilder().ToCachedBuilder();
        //_randomBuilder = GetRandomPositionBuilder();
        _writer = _builder.CreateWriter();
        //_randomWriter = _randomBuilder.CreateWriter();
        _random = new(1234);
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

    // [Benchmark]
    // public void UiFramework_Writer()
    // {
    //     UiBuilder builder = _builder;
    //     JsonFrameworkWriter writer = builder.CreateWriter();
    //     writer.Dispose();
    // }
    
    // public string UiFramework_Writer1()
    // {
    //     UiBuilder builder = _builder;
    //     JsonFrameworkWriter writer = builder.CreateWriter();
    //     return Encoding.UTF8.GetString(writer.ToArray());
    //     //writer.Dispose();
    // }
    
    // [Benchmark]
    // public void UiFramework_Network()
    // {
    //     BenchmarkNetWrite write = Pool.Get<BenchmarkNetWrite>();
    //     _writer.WriteToNetwork(write);
    //     Pool.Free(ref write);
    // }
    
    // [Benchmark(Baseline = false)]
    // public void UiFramework_Cached()
    // {
    //     CachedUiBuilder builder = _cached;
    //     builder.AddUi(default(SendInfo));
    // }
    
    // [Benchmark(Baseline = false)]
    // public void UiFramework_Async()
    // {
    //     UiBuilder builder = GetFrameworkBuilder();
    //     builder.AddUi(default(SendInfo));
    //     builder.Dispose();
    // }
    
    // [Benchmark]
    // public void Oxide_Async()
    // {
    //     CuiElementContainer builder = GetOxideContainer();
    //     builder.AddUiAsync(_connection);
    // }
    
    // [Benchmark(Baseline = false)]
    // public void UiFramework_Full()
    // {
    //     UiBuilder builder = GetFrameworkBuilder();
    //     JsonFrameworkWriter writer = builder.CreateWriter();
    //     BenchmarkNetWrite write = Pool.Get<BenchmarkNetWrite>();
    //     writer.WriteToNetwork(write);
    //     writer.Dispose();
    //     Pool.Free(ref write);
    //     builder.Dispose();
    // }
    //
    // [Benchmark(Baseline = false)]
    // public byte[] Oxide_Full()
    // {
    //     CuiElementContainer builder = GetOxideContainer();
    //     string json = builder.ToJson();
    //     return Encoding.UTF8.GetBytes(json);
    // }
    
    public CuiElementContainer GetOxideContainer()
    {
        CuiElementContainer container = new();
        for (int i = 0; i < Iterations; i++)
        {
            int mode = _random.Next(7);
            switch (mode)
            {
                case 0:
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
                    break;
                case 1:
                    container.Add(new CuiLabel
                    {
                        Text =
                        {
                            Color = "1.0 1.0 1.0 1.0",
                            Text = "Text"
                        },
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
                case 2:
                    container.Add(new CuiButton
                    {
                        Text =
                        {
                            Color = "1.0 1.0 1.0 1.0",
                            Text = "Text"
                        },
                        Button =
                        {
                            Command = "command"
                        },
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
                case 3:
                    container.Add(new CuiPanel
                    {
                        RawImage = new CuiRawImageComponent()
                        {
                            Png = "png",
                            Color = "1.0 1.0 1.0 1.0"
                        },
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
                case 4:
                    container.Add(new CuiPanel()
                    {
                        RawImage = new CuiRawImageComponent()
                        {
                            Url = "url",
                            Color = "1.0 1.0 1.0 1.0"
                        },
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
                case 5:
                    container.Add(new CuiPanel
                    {
                        Image = 
                        {
                            ItemId = 0,
                            Color = "1.0 1.0 1.0 1.0"
                        },
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
                case 6:
                    container.Add(new CuiPanel
                    {
                        RectTransform =
                        {
                            AnchorMin = _oxideMins[i],
                            AnchorMax = _oxideMaxs[i]
                        }
                    });
                    break;
            }
        }

        return container;
    }

    private UiBuilder GetFrameworkBuilder()
    {
        UiBuilder builder = UiBuilder.Create(UiPosition.Full, UiColor.Clear, "123");
        builder.EnsureCapacity(Iterations);
        for (int i = 0; i < Iterations - 1; i++)
        {
            int mode = _random.Next(7);
            switch (mode)
            {
                case 0:
                    builder.Panel(builder.Root, _frameworkPos[i], default, UiColor.Clear);
                    break;
                case 1:
                    builder.Label(builder.Root, _frameworkPos[i], default, "Text", 14, UiColor.White);
                    break;
                case 2:
                    builder.CommandButton(builder.Root, _frameworkPos[i], default, UiColor.Green, "command");
                    break;
                case 3:
                    builder.ImageFileStorage(builder.Root, _frameworkPos[i], default, "0", UiColor.Blue);
                    break;
                case 4:
                    builder.WebImage(builder.Root, _frameworkPos[i], default, "http://google.com", UiColor.Yellow);
                    break;
                case 5:
                    builder.ItemIcon(builder.Root, _frameworkPos[i], default, 0, color: UiColor.Cyan);
                    break;
                case 6:
                    builder.Section(builder.Root, _frameworkPos[i]);
                    break;
            }
        }

        return builder;
    }
}
#endif