using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json;

public static class JsonDefaults
{
    public static class Common
    {
        public static readonly Utf8String ComponentTypeName = "type"u8;
        public static readonly Utf8String ComponentsName = "components"u8;
        public static readonly Utf8String ComponentName = "name"u8;
        public static readonly Utf8String ParentName = "parent"u8;
        public static readonly Utf8String FadeInName = "fadeIn"u8;
        public static readonly Utf8String FadeOutName = "fadeOut"u8;
        public const float FadeOut = 0;
        public const float FadeIn = 0;
        public static readonly Utf8String RectTransformName = "RectTransform"u8;
        public const string NullValue = null;
        public static readonly Utf8String NeedsCursorValue = "NeedsCursor"u8;
        public static readonly Utf8String NeedsKeyboardValue = "NeedsKeyboard"u8;
        public static readonly Utf8String AutoDestroy = "destroyUi"u8;
        public static readonly Utf8String CommandName = "command"u8;
        public static readonly Utf8String EnabledName = "enabled"u8;
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 Max = new(1, 1);
        public static readonly UiOffset TextPadding = new(2, 0, -2, 0);
    }
        
    public static class Position
    {
        public static readonly Utf8String AnchorMinName = "anchormin"u8;
        public static readonly Utf8String AnchorMaxName = "anchormax"u8;
    }

    public static class Offset
    {
        public static readonly Utf8String OffsetMinName = "offsetmin"u8;
        public static readonly Utf8String OffsetMaxName = "offsetmax"u8;
    }

    public static class Color
    {
        public static readonly Utf8String ColorName = "color"u8;
        public static readonly UiColor ColorValue = "#FFFFFFFF";
    }

    public static class BaseImage
    {
        public static readonly Utf8String SpriteName = "sprite"u8;
        public static readonly Utf8String MaterialName = "material"u8;
        public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string Material = "Assets/Icons/IconMaterial.mat";
    }

    public static class RawImage
    {
        public static readonly Utf8String Type = "UnityEngine.UI.RawImage"u8;
        public const string TextureValue = "Assets/Icons/rust.png";
    }

    public static class BaseText
    {
        public const int FontSize = 14;
        public static readonly Utf8String Type = "UnityEngine.UI.Text"u8;
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public static readonly Utf8String FontName = "font"u8;
        public static readonly Utf8String TextName = "text"u8;
        public static readonly Utf8String FontSizeName = "fontSize"u8;
        public static readonly Utf8String AlignName = "align"u8;
        public static readonly Utf8String VerticalOverflowName = "verticalOverflow"u8;
    }

    public static class Outline
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Outline"u8;
        public static readonly Utf8String DistanceName = "distance"u8;
        public static readonly Utf8String UseGraphicAlphaName = "useGraphicAlpha"u8;
        public static readonly Vector2 FpDistance = new(1.0f, -1.0f);
        public static readonly Vector2 Distance = new(0.5f, -0.5f);
    }

    public static class Button
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Button"u8;
        public static readonly Utf8String CloseName = "close"u8;
    }

    public static class Image
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Image"u8;
        public static readonly Utf8String PngName = "png"u8;
        public static readonly Utf8String UrlName = "url"u8;
        public static readonly Utf8String ImageType = "imagetype"u8;
    }

    public static class ItemIcon
    {
        public static readonly Utf8String ItemIdName = "itemid"u8;
        public static readonly Utf8String SkinIdName = "skinid"u8;
    }
    
    public static class PlayerAvatar
    {
        public static readonly Utf8String SteamIdName = "steamid"u8;
    }

    public static class Input
    {
        public static readonly Utf8String Type = "UnityEngine.UI.InputField"u8;
        public static readonly Utf8String CharacterLimitName = "characterLimit"u8;
        public const int CharacterLimitValue = 0;
        public static readonly Utf8String PasswordName = "password"u8;
        public static readonly Utf8String ReadOnlyName = "readOnly"u8;
        public static readonly Utf8String LineTypeName = "lineType"u8;
        public static readonly Utf8String NeedsKeyboardName = "needsKeyboard"u8;
        public static readonly Utf8String NeedsHudKeyboardName = "hudMenuInput"u8;
        public static readonly Utf8String AutoFocusName = "autofocus"u8;
    }
        
    public static class Countdown
    {
        public static readonly Utf8String Type = "Countdown"u8;
        
        public static readonly Utf8String StartTimeName = "startTime"u8;
        public const float StartTimeValue = 0;
        
        public static readonly Utf8String EndTimeName = "endTime"u8;
        public const float EndTimeValue = 0;
        
        public static readonly Utf8String StepName = "step"u8;
        public const float StepValue = 1;
        
        public static readonly Utf8String IntervalName = "interval"u8;
        public const float IntervalValue = 1;
        
        public static readonly Utf8String TimerFormatName = "timerFormat"u8;
        public const TimerFormat TimeFormatValue = TimerFormat.None;
        
        public static readonly Utf8String NumberFormatName = "numberFormat"u8;
        public const string NumberFormatValue = "0.####";
        
        public static readonly Utf8String DestroyIfDoneName = "destroyIfDone"u8;
        public const bool DestroyIfDone = true;
        public static readonly Utf8String CountdownCommandName = "command"u8;
    }

    public static class ScrollView
    {
        public static readonly Utf8String Type = "UnityEngine.UI.ScrollView"u8;
        
        public static readonly Utf8String MovementTypeName = "movementType"u8;
        public const ScrollRect.MovementType MovementType = ScrollRect.MovementType.Clamped;
        
        public static readonly Utf8String ElasticityName = "elasticity"u8;
        public const float Elasticity = 0.1f;
        
        public static readonly Utf8String InertiaName = "inertia"u8;
        public const bool Inertia = false;
        
        public static readonly Utf8String DecelerationRateName = "decelerationRate"u8;
        public const float DecelerationRate = 0.135f;
        
        public static readonly Utf8String ScrollSensitivityName = "scrollSensitivity"u8;
        public const float ScrollSensitivity = 1f;
        
        public static readonly Utf8String Horizontal = "horizontal"u8;
        public static readonly Utf8String Vertical = "vertical"u8;
        public static readonly Utf8String HorizontalScrollbar = "horizontalScrollbar"u8;
        public static readonly Utf8String VerticalScrollbar = "verticalScrollbar"u8;
        public static readonly Utf8String ContentTransform = "contentTransform"u8;
        
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 AnchorMax = new(1, 1);
        public static readonly Vector2 OffsetMax = new(0, 0);
    }

    public static class ScrollBar
    {
        public static readonly Utf8String InvertName = "invert"u8;
        public const bool Invert = false;
        public static readonly Utf8String AutoHideName = "autoHide"u8;
        public const bool AutoHide = false;
        public static readonly Utf8String HandleSprite = "handleSprite"u8;
        public static readonly Utf8String TrackSprite = "trackSprite"u8;
        public static readonly Utf8String SizeName = "size"u8;
        public const float Size = 20f;
        public static readonly Utf8String HandleColorName = "handleColor"u8;
        public static readonly Utf8String HighlightColorName = "highlightColor"u8;
        public static readonly Utf8String PressedColorName = "pressedColor"u8;
        public static readonly Utf8String TrackColorName = "trackColor"u8;
        public static readonly UiColor HandleColor = UiColor.ParseHexColor("#262626");
        public static readonly UiColor HighlightColor = UiColor.ParseHexColor("#2B2B2B");
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#333333");
        public static readonly UiColor TrackColor = UiColor.ParseHexColor("#171717");
    }

    public static class ColorBlock
    {
        public const string HighlightedColorName = "highlightedColor";
        public static readonly UiColor HighlightedColor = UiColor.ParseHexColor("#F5F5F5FF");
        
        public const string PressedColorName = "pressedColor";
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#C8C8C8FF");
        
        public const string SelectedColorName = "selectedColor";
        public static readonly UiColor SelectedColor = UiColor.ParseHexColor("#F5F5F5FF");
        
        public const string ColorMultiplierName = "colorMultiplier";
        public const float ColorMultiplier = 1f;
        
        public const string FadeDurationName = "fadeDuration";
        public const float FadeDuration = 0.1f;
    }

    public static class Draggable
    {
        public const string LimitToParentName = "limitToParent";
        public const bool LimitToParent = false;
        
        public const string MaxDistanceName = "maxDistance";
        public const float MaxDistance = -1f;
        
        public const string AllowSwappingName = "allowSwapping";
        public const bool AllowSwapping = false;
        
        public const string DropAnywhereName = "dropAnywhere";
        public const bool DropAnywhere = false;
        
        public const string DragAlphaName = "dragAlpha";
        public const float DragAlpha = 1f;
        
        public const string ParentLimitIndexName = "parentLimitIndex";
        public const int ParentLimitIndex = 1;
        
        public const string FilterName = "filter";
        
        public const string ParentPaddingName = "parentPadding";
        public static readonly Vector2 ParentPadding = new(0, 0);
        
        public const string AnchorOffsetName = "anchorOffset";
        public static readonly Vector2 AnchorOffset = new(0, 0);
        
        public const string KeepOnTopName = "keepOnTop";
        public const bool KeepOnTop = false;
        
        public const string PositionRpcName = "positionRPC";
        public static readonly DraggablePositionSendType? PositionRpc = null;
        
        public const string MoveToAnchorName = "moveToAnchor";
        public const bool MoveToAnchor = false;
        
        public const string RebuildAnchorName = "moveToAnchor";
        public const bool RebuildAnchor = false;
    }

    public static class Slot
    {
        public const string FilterName = "filter";
    }
}