using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Json;

public static class JsonDefaults
{
    public static class Common
    {
        public static readonly byte[] ComponentTypeName = "type"u8.ToArray();
        public static readonly byte[] ComponentsName = "components"u8.ToArray();
        public static readonly byte[] ComponentName = "name"u8.ToArray();
        public static readonly byte[] ParentName = "parent"u8.ToArray();
        public static readonly byte[] FadeInName = "fadeIn"u8.ToArray();
        public static readonly byte[] FadeOutName = "fadeOut"u8.ToArray();
        public const float FadeOut = 0;
        public const float FadeIn = 0;
        public static readonly byte[] RectTransformName = "RectTransform"u8.ToArray();
        public const string NullValue = null;
        public static readonly byte[] NeedsCursorValue = "NeedsCursor"u8.ToArray();
        public static readonly byte[] NeedsKeyboardValue = "NeedsKeyboard"u8.ToArray();
        public static readonly byte[] AutoDestroy = "destroyUi"u8.ToArray();
        public static readonly byte[] CommandName = "command"u8.ToArray();
        public static readonly byte[] EnabledName = "enabled"u8.ToArray();
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 Max = new(1, 1);
    }
        
    public static class Position
    {
        public static readonly byte[] AnchorMinName = "anchormin"u8.ToArray();
        public static readonly byte[] AnchorMaxName = "anchormax"u8.ToArray();
    }

    public static class Offset
    {
        public static readonly byte[] OffsetMinName = "offsetmin"u8.ToArray();
        public static readonly byte[] OffsetMaxName = "offsetmax"u8.ToArray();
    }

    public static class Color
    {
        public static readonly byte[] ColorName = "color"u8.ToArray();
        public static readonly UiColor ColorValue = "#FFFFFFFF";
    }

    public static class BaseImage
    {
        public static readonly byte[] SpriteName = "sprite"u8.ToArray();
        public static readonly byte[] MaterialName = "material"u8.ToArray();
        public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string Material = "Assets/Icons/IconMaterial.mat";
    }

    public static class RawImage
    {
        public static readonly byte[] Type = "UnityEngine.UI.RawImage"u8.ToArray();
        public const string TextureValue = "Assets/Icons/rust.png";
    }

    public static class BaseText
    {
        public const int FontSize = 14;
        public static readonly byte[] Type = "UnityEngine.UI.Text"u8.ToArray();
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public static readonly byte[] FontName = "font"u8.ToArray();
        public static readonly byte[] TextName = "text"u8.ToArray();
        public static readonly byte[] FontSizeName = "fontSize"u8.ToArray();
        public static readonly byte[] AlignName = "align"u8.ToArray();
        public static readonly byte[] VerticalOverflowName = "verticalOverflow"u8.ToArray();
    }

    public static class Outline
    {
        public static readonly byte[] Type = "UnityEngine.UI.Outline"u8.ToArray();
        public static readonly byte[] DistanceName = "distance"u8.ToArray();
        public static readonly byte[] UseGraphicAlphaName = "useGraphicAlpha"u8.ToArray();
        public static readonly Vector2 FpDistance = new(1.0f, -1.0f);
        public static readonly Vector2 Distance = new(0.5f, -0.5f);
    }

    public static class Button
    {
        public static readonly byte[] Type = "UnityEngine.UI.Button"u8.ToArray();
        public static readonly byte[] CloseName = "close"u8.ToArray();
    }

    public static class Image
    {
        public static readonly byte[] Type = "UnityEngine.UI.Image"u8.ToArray();
        public static readonly byte[] PngName = "png"u8.ToArray();
        public static readonly byte[] UrlName = "url"u8.ToArray();
        public static readonly byte[] ImageType = "imagetype"u8.ToArray();
    }

    public static class ItemIcon
    {
        public static readonly byte[] ItemIdName = "itemid"u8.ToArray();
        public static readonly byte[] SkinIdName = "skinid"u8.ToArray();
    }
    
    public static class PlayerAvatar
    {
        public static readonly byte[] SteamIdName = "steamid"u8.ToArray();
    }

    public static class Input
    {
        public static readonly byte[] Type = "UnityEngine.UI.InputField"u8.ToArray();
        public static readonly byte[] CharacterLimitName = "characterLimit"u8.ToArray();
        public const int CharacterLimitValue = 0;
        public static readonly byte[] PasswordName = "password"u8.ToArray();
        public static readonly byte[] ReadOnlyName = "readOnly"u8.ToArray();
        public static readonly byte[] LineTypeName = "lineType"u8.ToArray();
        public static readonly byte[] NeedsKeyboardName = "needsKeyboard"u8.ToArray();
        public static readonly byte[] NeedsHudKeyboardName = "hudMenuInput"u8.ToArray();
        public static readonly byte[] AutoFocusName = "autofocus"u8.ToArray();
    }
        
    public static class Countdown
    {
        public static readonly byte[] Type = "Countdown"u8.ToArray();
        public static readonly byte[] StartTimeName = "startTime"u8.ToArray();
        public const float StartTimeValue = 0;
        public static readonly byte[] EndTimeName = "endTime"u8.ToArray();
        public const float EndTimeValue = 0;
        public static readonly byte[] StepName = "step"u8.ToArray();
        public const float StepValue = 1;
        public static readonly byte[] IntervalName = "interval"u8.ToArray();
        public const float IntervalValue = 1;
        public static readonly byte[] TimerFormatName = "timerFormat"u8.ToArray();
        public static readonly byte[] NumberFormatName = "numberFormat"u8.ToArray();
        public const string NumberFormatValue = "0.####";
        public static readonly byte[] DestroyIfDoneName = "destroyIfDone"u8.ToArray();
        public static readonly byte[] CountdownCommandName = "command"u8.ToArray();
    }

    public static class ScrollView
    {
        public static readonly byte[] Type = "UnityEngine.UI.ScrollView"u8.ToArray();
        public static readonly byte[] Horizontal = "horizontal"u8.ToArray();
        public static readonly byte[] Vertical = "vertical"u8.ToArray();
        public static readonly byte[] MovementType = "movementType"u8.ToArray();
        public static readonly byte[] ElasticityName = "elasticity"u8.ToArray();
        public static readonly byte[] Inertia = "inertia"u8.ToArray();
        public static readonly byte[] DecelerationRateName = "decelerationRate"u8.ToArray();
        public static readonly byte[] ScrollSensitivityName = "scrollSensitivity"u8.ToArray();
        public static readonly byte[] HorizontalScrollbar = "horizontalScrollbar"u8.ToArray();
        public static readonly byte[] VerticalScrollbar = "verticalScrollbar"u8.ToArray();
        public static readonly byte[] ContentTransform = "contentTransform"u8.ToArray();
            
        public const float Elasticity = 0.1f;
        public const float DecelerationRate = 0.135f;
        public const float ScrollSensitivity = 1f;
            
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 AnchorMax = new(1, 1);
        public static readonly Vector2 OffsetMax = new(0, 0);
    }

    public static class ScrollBar
    {
        public static readonly byte[] Invert = "invert"u8.ToArray();
        public static readonly byte[] AutoHide = "autoHide"u8.ToArray();
        public static readonly byte[] HandleSprite = "handleSprite"u8.ToArray();
        public static readonly byte[] TrackSprite = "trackSprite"u8.ToArray();
        public static readonly byte[] SizeName = "size"u8.ToArray();
        public const float Size = 20f;
        public static readonly byte[] HandleColorName = "handleColor"u8.ToArray();
        public static readonly byte[] HighlightColorName = "highlightColor"u8.ToArray();
        public static readonly byte[] PressedColorName = "pressedColor"u8.ToArray();
        public static readonly byte[] TrackColorName = "trackColor"u8.ToArray();
        public static readonly UiColor HandleColor = UiColor.ParseHexColor("#262626");
        public static readonly UiColor HighlightColor = UiColor.ParseHexColor("#2B2B2B");
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#333333");
        public static readonly UiColor TrackColor = UiColor.ParseHexColor("#171717");
    }
}