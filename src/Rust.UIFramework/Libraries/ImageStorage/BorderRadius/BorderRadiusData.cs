using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal sealed class BorderRadiusData : BasePoolable, IEquatable<BorderRadiusData>
{
    public UiDimensions2D Size { get; private set; }
    public UiBorderRadius Radius { get; private set; }
    public bool AntiAlias { get; private set; }
    public float EdgeWidth { get; private set; }

    public BorderRadiusData() { }

    public BorderRadiusData(UiDimensions2D size, UiBorderRadius radius, bool antiAlias, float edgeWidth)
    {
        Size = size;
        Radius = radius;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
    }

    public static BorderRadiusData Get(IUiFrameworkPlugin plugin, UiDimensions2D size, in UiBorderRadius radius, bool antiAlias, float edgeWidth)
    {
        return plugin.PluginPool.Get<BorderRadiusData>().Init(size, radius, antiAlias, edgeWidth);
    }

    private BorderRadiusData Init(UiDimensions2D size, in UiBorderRadius radius, bool antiAlias, float edgeWidth)
    {
        Size = size;
        Radius = radius;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        return this;
    }

    public bool Equals(BorderRadiusData other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Size.Equals(other.Size)
               && Radius.Equals(other.Radius)
               && AntiAlias == other.AntiAlias
               && EdgeWidth.Equals(other.EdgeWidth);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return Equals((BorderRadiusData)obj);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(Size);
        hashCode.Add(Radius);
        hashCode.Add(AntiAlias);
        hashCode.Add(EdgeWidth);
        return hashCode.ToHashCode();
    }

    public string ToName() => $"{Size.ToString()}-{Radius.ToString()}-{(AntiAlias ? '1' : '0')}-{EdgeWidth}";

    public BorderRadiusData New() => new(Size, Radius, AntiAlias, EdgeWidth);

    protected override void EnterPool()
    {
        Size = default;
        Radius = default;
        AntiAlias = default;
        EdgeWidth = default;
    }
}