using System;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Types.Results;

namespace Oxide.Ext.UiFramework.Libraries;

public class BorderRadiusBuilder
{
    public UiSize2D Size { get; private set; }
    public UiBorderRadius Radius { get; private set; } = UiBorderRadius.None;

    public bool AntiAlias { get; private set; } = true;
    public float EdgeWidth { get; private set; } = 1;

    public UiColor Fill { get; private set; } = UiColors.White;
    public UiColor Transparent { get; private set; } = UiColors.Transparent;

    public bool EnableBorder { get; private set; } = false;
    public float BorderWidth { get; private set; } = 1f;
    public UiColor BorderColor { get; private set; } = UiColors.Black;

    public bool EnableDashedBorder { get; private set; } = false;
    public float DashLength { get; private set; } = 10f;
    public float GapLength { get; private set; } = 5f;

    public bool UseInputImage { get; private set; }
    public string InputImage { get; private set; }

    public BorderRadiusBuilder() {}

    public BorderRadiusBuilder(string image)
    {
        UseInputImage = true;
        InputImage = image;
    }

    public BorderRadiusBuilder SetSize(UiSize2D size)
    {
        if (UseInputImage) throw new InvalidOperationException("Cannot set size when using input image");
        Size = size;
        return this;
    }

    public BorderRadiusBuilder SetRadius(in UiBorderRadius radius)
    {
        Radius = radius;
        return this;
    }

    public BorderRadiusBuilder SetAntiAlias(bool antiAlias, float edgeWidth = 1f)
    {
        Guard.IsGreaterThanOrEqualToZero(edgeWidth);
        AntiAlias = antiAlias;
        EdgeWidth = edgeWidth;
        return this;
    }

    public BorderRadiusBuilder SetAntiAliasEdgeWidth(float edgeWidth)
    {
        Guard.IsGreaterThanOrEqualToZero(edgeWidth);
        EdgeWidth = edgeWidth;
        return this;
    }

    public BorderRadiusBuilder SetFill(UiColor fill)
    {
        Fill = fill;
        return this;
    }

    public BorderRadiusBuilder SetTransparent(UiColor transparent)
    {
        Transparent = transparent;
        return this;
    }

    public BorderRadiusBuilder SetBorder(bool enable, float borderWidth = 1f, UiColor? borderColor = null)
    {
        Guard.IsGreaterThanOrEqualToZero(borderWidth);
        EnableBorder = enable;
        BorderWidth = borderWidth;
        BorderColor = borderColor ?? UiColors.Black;
        return this;
    }

    public BorderRadiusBuilder SetEnableBorder(bool enable)
    {
        EnableBorder = enable;
        return this;
    }

    public BorderRadiusBuilder SetBorderWidth(float borderWidth)
    {
        Guard.IsGreaterThanOrEqualToZero(borderWidth);
        BorderWidth = borderWidth;
        return this;
    }

    public BorderRadiusBuilder SetBorderColor(UiColor borderColor)
    {
        BorderColor = borderColor;
        return this;
    }

    public BorderRadiusBuilder SetDashedBorder(bool enable, float dashLength = 10f, float gapLength = 5f)
    {
        Guard.IsGreaterThanOrEqualToZero(dashLength);
        Guard.IsGreaterThanOrEqualToZero(gapLength);
        EnableDashedBorder = enable;
        DashLength = dashLength;
        GapLength = gapLength;
        return this;
    }

    public BorderRadiusBuilder SetEnableDashedBorder(bool enable)
    {
        EnableDashedBorder = enable;
        return this;
    }

    public BorderRadiusBuilder SetDashLength(float dashLength)
    {
        Guard.IsGreaterThanOrEqualToZero(dashLength);
        DashLength = dashLength;
        return this;
    }

    public BorderRadiusBuilder SetGapLength(float gapLength)
    {
        Guard.IsGreaterThanOrEqualToZero(gapLength);
        GapLength = gapLength;
        return this;
    }

    public BorderRadiusBuilder SetImage(string image)
    {
        if (string.IsNullOrEmpty(image))
        {
            UseInputImage = false;
            InputImage = null;
        }
        else
        {
            UseInputImage = true;
            InputImage = image;
        }

        return this;
    }

    public BorderRadiusBuilder Clone(string image)
    {
        return new BorderRadiusBuilder(image)
        {
            Radius = Radius,
            Transparent = Transparent,
            AntiAlias = AntiAlias,
            EdgeWidth = EdgeWidth,
            EnableBorder = EnableBorder,
            BorderWidth = BorderWidth,
            BorderColor = BorderColor,
            EnableDashedBorder = EnableDashedBorder,
            DashLength = DashLength,
            GapLength = GapLength
        };
    }

    public BorderRadiusBuilder Clone()
    {
        return new BorderRadiusBuilder
        {
            Size = Size,
            Fill = Fill,
            Transparent = Transparent,
            Radius = Radius,
            AntiAlias = AntiAlias,
            EdgeWidth = EdgeWidth,
            EnableBorder = EnableBorder,
            BorderWidth = BorderWidth,
            BorderColor = BorderColor,
            EnableDashedBorder = EnableDashedBorder,
            DashLength = DashLength,
            GapLength = GapLength
        };
    }

    public IRegisterImageRequest Generate(IUiFrameworkPlugin plugin)
    {
        if (UseInputImage)
        {
            return Singleton<UiImageStorage>.Instance.RegisterBorderRadius(plugin, InputImage, Radius, Transparent, AntiAlias, EdgeWidth, EnableBorder, BorderWidth, BorderColor, EnableDashedBorder, DashLength, GapLength);
        }

        return Singleton<UiImageStorage>.Instance.RegisterBorderRadius(plugin, Size, Radius, Fill, Transparent, AntiAlias, EdgeWidth, EnableBorder, BorderWidth, BorderColor, EnableDashedBorder, DashLength, GapLength);
    }

    public async UniTask<Result<ImageId>> GenerateAsync(IUiFrameworkPlugin plugin)
    {
        return await Generate(plugin).AsUniTask();
    }
}