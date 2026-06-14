using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal sealed class BorderRadiusImageData : BasePoolable, IEquatable<BorderRadiusImageData>
{
    public string Png { get; private set; }
    public UiBorderRadius Radius { get; private set; }
    public bool AntiAlias { get; private set; }
    public float EdgeWidth { get; private set; }
    public UiColor ReplacementColor { get; private set; }

    public BorderRadiusImageData() { }

    public BorderRadiusImageData(UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor replacementColor)
    {
        Radius = radius;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        ReplacementColor = replacementColor;
    }

    public BorderRadiusImageData(string png, UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor replacementColor) : this(radius, antiAlias, edgeWidth, replacementColor)
    {
        Png = png;
        ReplacementColor = replacementColor;
    }

    public static BorderRadiusImageData Get(IUiFrameworkPlugin plugin, string png, in UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor replacementColor)
    {
        return plugin.PluginPool.Get<BorderRadiusImageData>().Init(png, radius, antiAlias, edgeWidth, replacementColor);
    }

    private BorderRadiusImageData Init(string png, in UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor replacementColor)
    {
        Png = png;
        Radius = radius;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        ReplacementColor = replacementColor;
        return this;
    }

    public bool Equals(BorderRadiusImageData other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Png.Equals(other.Png)
               && Radius.Equals(other.Radius)
               && AntiAlias == other.AntiAlias
               && EdgeWidth.Equals(other.EdgeWidth)
               && ReplacementColor.Equals(other.ReplacementColor);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return Equals((BorderRadiusImageData)obj);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(Png);
        hashCode.Add(Radius);
        hashCode.Add(AntiAlias);
        hashCode.Add(EdgeWidth);
        hashCode.Add(ReplacementColor);
        return hashCode.ToHashCode();
    }

    public string ToName() => $"{Png}-{Radius.ToString()}-{(AntiAlias ? '1' : '0')}-{EdgeWidth}-{ReplacementColor.ToHexRGBA()}";

    public BorderRadiusImageData New() => new(Png, Radius, AntiAlias, EdgeWidth, ReplacementColor);

    protected override void EnterPool()
    {
        Png = default;
        Radius = default;
        AntiAlias = default;
        EdgeWidth = default;
        ReplacementColor = default;
    }
}