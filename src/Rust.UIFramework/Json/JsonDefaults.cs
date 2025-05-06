﻿using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Json;

public static class JsonDefaults
{
    public static class Common
    {
        public const string ComponentTypeName = "type";
        public const string ComponentName = "name";
        public const string ParentName = "parent";
        public const string FadeInName = "fadeIn";
        public const string FadeOutName = "fadeOut";
        public const float FadeOut = 0;
        public const float FadeIn = 0;
        public const string RectTransformName = "RectTransform";
        public const string NullValue = null;
        public const string NeedsCursorValue = "NeedsCursor";
        public const string NeedsKeyboardValue = "NeedsKeyboard";
        public const string AutoDestroy = "destroyUi";
        public const string CommandName = "command";
        public const string EnabledName = "enabled";
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 Max = new(1, 1);
    }
        
    public static class Position
    {
        public const string AnchorMinName = "anchormin";
        public const string AnchorMaxName = "anchormax";
    }

    public static class Offset
    {
        public const string OffsetMinName = "offsetmin";
        public const string OffsetMaxName = "offsetmax";
        public const string DefaultOffsetMax = "0 0";
    }

    public static class Color
    {
        public const string ColorName = "color";
        public static readonly UiColor ColorValue = "#FFFFFFFF";
    }

    public static class BaseImage
    {
        public const string SpriteName = "sprite";
        public const string MaterialName = "material";
        public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string Material = "Assets/Icons/IconMaterial.mat";
    }

    public static class RawImage
    {
        public const string TextureValue = "Assets/Icons/rust.png";
    }

    public static class BaseText
    {
        public const int FontSize = 14;
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public const string FontName = "font";
        public const string TextName = "text";
        public const string FontSizeName = "fontSize";
        public const string AlignName = "align";
        public const string VerticalOverflowName = "verticalOverflow";
    }

    public static class Outline
    {
        public const string DistanceName = "distance";
        public const string UseGraphicAlphaName = "useGraphicAlpha";
        public static readonly Vector2 FpDistance = new(1.0f, -1.0f);
        public static readonly Vector2 Distance = new(0.5f, -0.5f);
    }

    public static class Button
    {
        public const string CloseName = "close";
    }

    public static class Image
    {
        public const string PngName = "png";
        public const string UrlName = "url";
        public const string ImageType = "imagetype";
    }

    public static class ItemIcon
    {
        public const string ItemIdName = "itemid";
        public const string SkinIdName = "skinid";
    }
    
    public static class PlayerAvatar
    {
        public const string SteamIdName = "steamid";
    }

    public static class Input
    {
        public const string CharacterLimitName = "characterLimit";
        public const int CharacterLimitValue = 0;
        public const string PasswordName = "password";
        public const string ReadOnlyName = "readOnly";
        public const string LineTypeName = "lineType";
        public const string NeedsKeyboardName = "needsKeyboard";
        public const string NeedsHudKeyboardName = "hudMenuInput";
        public const string AutoFocusName = "autofocus";
    }
        
    public static class Countdown
    {
        public const string StartTimeName = "startTime";
        public const float StartTimeValue = 0;
        public const string EndTimeName = "endTime";
        public const float EndTimeValue = 0;
        public const string StepName = "step";
        public const float StepValue = 1;
        public const string IntervalName = "interval";
        public const float IntervalValue = 1;
        public const string TimerFormatName = "timerFormat";
        public const string NumberFormatName = "numberFormat";
        public const string NumberFormatValue = "0.####";
        public const string DestroyIfDoneName = "destroyIfDone";
        public const string CountdownCommandName = "command";
    }

    public static class ScrollView
    {
        public const string Horizontal = "horizontal";
        public const string Vertical = "vertical";
        public const string MovementType = "movementType";
        public const string ElasticityName = "elasticity";
        public const string Inertia = "inertia";
        public const string DecelerationRateName = "decelerationRate";
        public const string ScrollSensitivityName = "scrollSensitivity";
        public const string HorizontalScrollbar = "horizontalScrollbar";
        public const string VerticalScrollbar = "verticalScrollbar";
        public const string ContentTransform = "contentTransform";
            
        public static readonly float Elasticity = 0.1f;
        public static readonly float DecelerationRate = 0.135f;
        public static readonly float ScrollSensitivity = 1f;
            
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 AnchorMax = new(1, 1);
        public static readonly Vector2 OffsetMax = new(0, 0);
    }

    public static class ScrollBar
    {
        public const string Invert = "invert";
        public const string AutoHide = "autoHide";
        public const string HandleSprite = "handleSprite";
        public const string TrackSprite = "trackSprite";
        public const string SizeName = "size";
        public const float Size = 20f;
        public const string HandleColorName = "handleColor";
        public const string HighlightColorName = "highlightColor";
        public const string PressedColorName = "pressedColor";
        public const string TrackColorName = "trackColor";
        public static readonly UiColor HandleColor = UiColor.ParseHexColor("#262626");
        public static readonly UiColor HighlightColor = UiColor.ParseHexColor("#2B2B2B");
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#333333");
        public static readonly UiColor TrackColor = UiColor.ParseHexColor("#171717");
    }
}