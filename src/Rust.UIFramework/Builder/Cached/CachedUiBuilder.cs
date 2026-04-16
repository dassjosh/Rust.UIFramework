using System;
using Network;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Builder.Cached;

public class CachedUiBuilder : BaseBuilder
{
    private readonly byte[] _cachedJson;

    private CachedUiBuilder(UiBuilder builder)
    {
        _cachedJson = builder.GetBytes();
        RootName = builder.GetRootName();
        Plugin = builder.Plugin;
    }

    internal static CachedUiBuilder CreateCachedBuilder(UiBuilder builder) => new(builder);

    public override byte[] GetBytes() => _cachedJson;
    public override void Combine(SendInfo send, JsonFrameworkWriter writer)
    {
        writer.WriteRaw(_cachedJson.AsSpan()[1..^1]);
    }

    internal override void SendUi(SendInfo send, in UiDebugOptions? options)
    {
        AddUi(send, GetBytes(), options);
    }

    internal override void SendAnimations(SendInfo send) { }
}