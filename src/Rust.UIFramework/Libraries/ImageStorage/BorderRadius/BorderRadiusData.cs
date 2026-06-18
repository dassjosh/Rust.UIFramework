using System;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal sealed class BorderRadiusData : BasePoolable, IEquatable<BorderRadiusData>
{
    public UiSize2D Size { get; private set; }
    public UiBorderRadius Radius { get; private set; } = UiBorderRadius.None;

    public UiColor Fill { get; private set; } = UiColors.White;
    public UiColor Transparent { get; private set; } = UiColors.Transparent;

    public bool AntiAlias { get; private set; } = true;
    public float EdgeWidth { get; private set; } = 1;

    public bool EnableBorder { get; private set; }
    public float BorderWidth { get; private set; }
    public UiColor BorderColor { get; private set; }

    public bool EnableDashedBorder { get; private set; }
    public float DashLength { get; private set; }
    public float GapLength { get; private set; }

    public bool UseInputImage;
    public string InputImage;

    public BorderRadiusData() { }

    public BorderRadiusData(UiSize2D size, UiBorderRadius radius,
        UiColor fill, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength) : this(radius, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength)
    {
        Size = size;
        Fill = fill;
    }

    public BorderRadiusData(string image, UiBorderRadius radius, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength) : this(radius, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength)
    {
        UseInputImage = true;
        InputImage = image;
    }

    public BorderRadiusData(UiBorderRadius radius, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        Radius = radius;
        Transparent = transparent;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        EnableBorder = enableBorder;
        BorderWidth = borderWidth;
        BorderColor = borderColor;
        EnableDashedBorder = enableDashedBorder;
        DashLength = dashLength;
        GapLength = gapLength;
    }

    public static BorderRadiusData Get(IUiFrameworkPlugin plugin, UiSize2D size, in UiBorderRadius radius,
        UiColor fill, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        return plugin.PluginPool.Get<BorderRadiusData>().Init(size, radius, fill, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength);
    }

    public static BorderRadiusData Get(IUiFrameworkPlugin plugin, string image, in UiBorderRadius radius, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        return plugin.PluginPool.Get<BorderRadiusData>().Init(image, radius, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength);
    }

    private BorderRadiusData Init(UiSize2D size, in UiBorderRadius radius,
        UiColor fill, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        Fill = fill;
        Size = size;
        return Init(radius, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength);
    }

    private BorderRadiusData Init(string image,
        in UiBorderRadius radius, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        UseInputImage = true;
        InputImage = image;
        return Init(radius, transparent, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor, enableDashedBorder, dashLength, gapLength);
    }

    private BorderRadiusData Init(in UiBorderRadius radius, UiColor transparent,
        bool antiAlias, float edgeWidth,
        bool enableBorder, float borderWidth, UiColor borderColor,
        bool enableDashedBorder, float dashLength, float gapLength)
    {
        Radius = radius;
        Transparent = transparent;
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        EnableBorder = enableBorder;
        BorderWidth = borderWidth;
        BorderColor = borderColor;
        EnableDashedBorder = enableDashedBorder;
        DashLength = dashLength;
        GapLength = gapLength;
        return this;
    }

    public string ToName()
    {
        const string format = "{0:0.##}";

        StringBuilder sb = UiPool.Internal.GetStringBuilder();

        sb.Append("1:");

        if (UseInputImage)
        {
            sb.Append(InputImage);
        }
        else
        {
            sb.Append(Size);
            sb.Append('-');
            sb.Append(Fill.ToHexRGBA());
        }

        sb.Append('-');
        sb.Append(Radius);
        sb.Append('-');
        sb.Append(Transparent.ToHexRGBA());
        sb.Append('-');

        if (AntiAlias)
        {
            sb.Append("2:");
            sb.AppendFormat(format, EdgeWidth);
        }

        if (EnableBorder)
        {
            sb.Append("3:");
            sb.AppendFormat(format, BorderWidth);
            sb.Append('-');
            sb.Append(BorderColor.ToHexRGBA());
        }

        if (EnableDashedBorder)
        {
            sb.Append("4:");
            sb.AppendFormat(format, DashLength);
            sb.Append('-');
            sb.AppendFormat(format, GapLength);
        }

        return UiPool.Internal.ToStringAndFree(sb);
    }

    public BorderRadiusData New() => new(Size, Radius, Fill, Transparent, AntiAlias, EdgeWidth, EnableBorder, BorderWidth, BorderColor, EnableDashedBorder, DashLength, GapLength);

    public bool Equals(BorderRadiusData other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return UseInputImage == other.UseInputImage
               && InputImage == other.InputImage
               && Size.Equals(other.Size)
               && Radius.Equals(other.Radius)
               && Fill.Equals(other.Fill)
               && Transparent.Equals(other.Transparent)
               && AntiAlias == other.AntiAlias
               && EdgeWidth.Equals(other.EdgeWidth)
               && EnableBorder == other.EnableBorder
               && BorderWidth.Equals(other.BorderWidth)
               && BorderColor.Equals(other.BorderColor)
               && EnableDashedBorder == other.EnableDashedBorder
               && DashLength.Equals(other.DashLength)
               && GapLength.Equals(other.GapLength);
    }

    public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is BorderRadiusData other && Equals(other);

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        hashCode.Add(UseInputImage);
        hashCode.Add(InputImage);
        hashCode.Add(Size);
        hashCode.Add(Radius);
        hashCode.Add(Fill);
        hashCode.Add(Transparent);
        hashCode.Add(AntiAlias);
        hashCode.Add(EdgeWidth);
        hashCode.Add(EnableBorder);
        hashCode.Add(BorderWidth);
        hashCode.Add(BorderColor);
        hashCode.Add(EnableDashedBorder);
        hashCode.Add(DashLength);
        hashCode.Add(GapLength);
        return hashCode.ToHashCode();
    }

    protected override void EnterPool()
    {
        Size = default;
        Radius = default;
        Fill = default;
        Transparent = default;
        AntiAlias = default;
        EdgeWidth = default;
        EnableBorder = default;
        BorderWidth = default;
        BorderColor = default;
        EnableDashedBorder = default;
        DashLength = default;
        GapLength = default;
        UseInputImage = default;
        InputImage = default;
    }
}