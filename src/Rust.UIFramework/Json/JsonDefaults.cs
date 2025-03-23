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
        public static readonly Utf8String ComponentTypeName = "type";
        public static readonly Utf8String ComponentsName = "components";
        public static readonly Utf8String ComponentName = "name";
        public static readonly Utf8String ParentName = "parent";
        public static readonly Utf8String Update = "update";
        
        public static readonly Utf8String FadeInName = "fadeIn";
        public const float FadeIn = 0;
        public static readonly Utf8String FadeOutName = "fadeOut";
        public const float FadeOut = 0;

        public static readonly Utf8String RectTransformName = "RectTransform";
        public const string NullValue = null;
        public static readonly Utf8String NeedsCursorValue = "NeedsCursor";
        public static readonly Utf8String NeedsKeyboardValue = "NeedsKeyboard";
        public static readonly Utf8String AutoDestroy = "destroyUi";
        public static readonly Utf8String CommandName = "command";
        public static readonly Utf8String EnabledName = "enabled";
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 Max = new(1, 1);
        public static readonly UiOffset TextPadding = new(2, 0, -2, 0);
        public static readonly UiOffset Padding = new(2, 2, -2, -2);
    }
        
    public static class Position
    {
        public static readonly Utf8String AnchorMinName = "anchormin";
        public static readonly Utf8String AnchorMaxName = "anchormax";
    }

    public static class Offset
    {
        public static readonly Utf8String OffsetMinName = "offsetmin";
        public static readonly Utf8String OffsetMaxName = "offsetmax";
    }

    public static class Color
    {
        public static readonly Utf8String ColorName = "color";
        public static readonly UiColor ColorValue = "#FFFFFFFF";
    }

    public static class BaseImage
    {
        public static readonly Utf8String SpriteName = "sprite";
        public static readonly Utf8String MaterialName = "material";
        public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string Material = "Assets/Icons/IconMaterial.mat";
    }

    public static class RawImage
    {
        public static readonly Utf8String Type = "UnityEngine.UI.RawImage";
        public const string TextureValue = "Assets/Icons/rust.png";
    }

    public static class BaseText
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Text";
       
        public static readonly Utf8String FontName = "font";
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        
        public static readonly Utf8String TextName = "text";
        
        public static readonly Utf8String FontSizeName = "fontSize";
        public const int FontSize = 14;
        
        public static readonly Utf8String AlignName = "align";
        
        public static readonly Utf8String VerticalOverflowName = "verticalOverflow";
    }

    public static class Outline
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Outline";
        public static readonly Utf8String DistanceName = "distance";
        public static readonly Utf8String UseGraphicAlphaName = "useGraphicAlpha";
        public static readonly Vector2 FpDistance = new(1.0f, -1.0f);
        public static readonly Vector2 Distance = new(0.5f, -0.5f);
    }

    public static class Button
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Button";
        public static readonly Utf8String CloseName = "close";
    }

    public static class Image
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Image";
        public static readonly Utf8String PngName = "png";
        public static readonly Utf8String UrlName = "url";
        public static readonly Utf8String ImageType = "imagetype";
    }

    public static class ItemIcon
    {
        public static readonly Utf8String ItemIdName = "itemid";
        public static readonly Utf8String SkinIdName = "skinid";
    }
    
    public static class PlayerAvatar
    {
        public static readonly Utf8String SteamIdName = "steamid";
    }

    public static class Input
    {
        public static readonly Utf8String Type = "UnityEngine.UI.InputField";
        public static readonly Utf8String CharacterLimitName = "characterLimit";
        public const int CharacterLimitValue = 0;
        public static readonly Utf8String PasswordName = "password";
        public static readonly Utf8String ReadOnlyName = "readOnly";
        public static readonly Utf8String LineTypeName = "lineType";
        public static readonly Utf8String NeedsKeyboardName = "needsKeyboard";
        public static readonly Utf8String NeedsHudKeyboardName = "hudMenuInput";
        public static readonly Utf8String AutoFocusName = "autofocus";
    }
        
    public static class Countdown
    {
        public static readonly Utf8String Type = "Countdown";
        
        public static readonly Utf8String StartTimeName = "startTime";
        public const float StartTimeValue = 0;
        
        public static readonly Utf8String EndTimeName = "endTime";
        public const float EndTimeValue = 0;
        
        public static readonly Utf8String StepName = "step";
        public const float StepValue = 1;
        
        public static readonly Utf8String IntervalName = "interval";
        public const float IntervalValue = 1;
        
        public static readonly Utf8String TimerFormatName = "timerFormat";
        public const TimerFormat TimeFormatValue = TimerFormat.None;
        
        public static readonly Utf8String NumberFormatName = "numberFormat";
        public const string NumberFormatValue = "0.####";
        
        public static readonly Utf8String DestroyIfDoneName = "destroyIfDone";
        public const bool DestroyIfDone = true;
        public static readonly Utf8String CountdownCommandName = "command";
    }

    public static class ScrollView
    {
        public static readonly Utf8String Type = "UnityEngine.UI.ScrollView";
        
        public static readonly Utf8String MovementTypeName = "movementType";
        public const ScrollRect.MovementType MovementType = ScrollRect.MovementType.Clamped;
        
        public static readonly Utf8String ElasticityName = "elasticity";
        public const float Elasticity = 0.1f;
        
        public static readonly Utf8String InertiaName = "inertia";
        public const bool Inertia = false;
        
        public static readonly Utf8String DecelerationRateName = "decelerationRate";
        public const float DecelerationRate = 0.135f;
        
        public static readonly Utf8String ScrollSensitivityName = "scrollSensitivity";
        public const float ScrollSensitivity = 1f;
        
        public static readonly Utf8String Horizontal = "horizontal";
        public static readonly Utf8String Vertical = "vertical";
        public static readonly Utf8String HorizontalScrollbar = "horizontalScrollbar";
        public static readonly Utf8String VerticalScrollbar = "verticalScrollbar";
        public static readonly Utf8String ContentTransform = "contentTransform";
        
        public static readonly Vector2 Min = new(0, 0);
        public static readonly Vector2 AnchorMax = new(1, 1);
        public static readonly Vector2 OffsetMax = new(0, 0);
    }

    public static class ScrollBar
    {
        public static readonly Utf8String InvertName = "invert";
        public const bool Invert = false;
        public static readonly Utf8String AutoHideName = "autoHide";
        public const bool AutoHide = false;
        public static readonly Utf8String HandleSprite = "handleSprite";
        public static readonly Utf8String TrackSprite = "trackSprite";
        public static readonly Utf8String SizeName = "size";
        public const float Size = 20f;
        public static readonly Utf8String HandleColorName = "handleColor";
        public static readonly Utf8String HighlightColorName = "highlightColor";
        public static readonly Utf8String PressedColorName = "pressedColor";
        public static readonly Utf8String TrackColorName = "trackColor";
        public static readonly UiColor HandleColor = UiColor.ParseHexColor("#262626");
        public static readonly UiColor HighlightColor = UiColor.ParseHexColor("#2B2B2B");
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#333333");
        public static readonly UiColor TrackColor = UiColor.ParseHexColor("#171717");
    }

    public static class ColorBlock
    {
        public static readonly Utf8String HighlightedColorName = "highlightedColor";
        public static readonly UiColor HighlightedColor = UiColor.ParseHexColor("#F5F5F5FF");
        
        public static readonly Utf8String PressedColorName = "pressedColor";
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#C8C8C8FF");
        
        public static readonly Utf8String SelectedColorName = "selectedColor";
        public static readonly UiColor SelectedColor = UiColor.ParseHexColor("#F5F5F5FF");
        
        public static readonly Utf8String ColorMultiplierName = "colorMultiplier";
        public const float ColorMultiplier = 1f;
        
        public static readonly Utf8String FadeDurationName = "fadeDuration";
        public const float FadeDuration = 0.1f;
    }

    public static class Draggable
    {
        public static readonly Utf8String Type = "Draggable";
        
        public static readonly Utf8String LimitToParentName = "limitToParent";
        public const bool LimitToParent = false;
        
        public static readonly Utf8String MaxDistanceName = "maxDistance";
        public const float MaxDistance = -1f;
        
        public static readonly Utf8String AllowSwappingName = "allowSwapping";
        public const bool AllowSwapping = false;
        
        public static readonly Utf8String DropAnywhereName = "dropAnywhere";
        public const bool DropAnywhere = false;
        
        public static readonly Utf8String DragAlphaName = "dragAlpha";
        public const float DragAlpha = 1f;
        
        public static readonly Utf8String ParentLimitIndexName = "parentLimitIndex";
        public const int ParentLimitIndex = 1;
        
        public static readonly Utf8String FilterName = "filter";
        
        public static readonly Utf8String ParentPaddingName = "parentPadding";
        public static readonly Vector2 ParentPadding = new(0, 0);
        
        public static readonly Utf8String AnchorOffsetName = "anchorOffset";
        public static readonly Vector2 AnchorOffset = new(0, 0);
        
        public static readonly Utf8String KeepOnTopName = "keepOnTop";
        public const bool KeepOnTop = false;
        
        public static readonly Utf8String PositionRpcName = "positionRPC";
        public static readonly DraggablePositionSendType? PositionRpc = null;
        
        public static readonly Utf8String MoveToAnchorName = "moveToAnchor";
        public const bool MoveToAnchor = false;
        
        public static readonly Utf8String RebuildAnchorName = "moveToAnchor";
        public const bool RebuildAnchor = false;
    }

    public static class Slot
    {
        public static readonly Utf8String Type = "Slot";
        public static readonly Utf8String FilterName = "filter";
    }
}