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
        
        public static readonly Utf8String ActiveName = "activeSelf";
        public const bool Active = true;
        
        public static readonly Utf8String FadeInName = "fadeIn";
        public const float FadeIn = 0;
        public static readonly Utf8String FadeOutName = "fadeOut";
        public const float FadeOut = 0;

        public static readonly Utf8String RectTransformName = "RectTransform";
        public const string NullValue = null;
        public static readonly Utf8String NeedsCursorValue = "NeedsCursor";
        public static readonly Utf8String NeedsKeyboardValue = "NeedsKeyboard";
        public static readonly Utf8String Replace = "destroyUi";
        public static readonly Utf8String CommandName = "command";
        public static readonly Utf8String EnabledName = "enabled";
        public static readonly Utf8String PlaceholderInputId = "placeholderParentId";
        
        public static readonly UiPadding TextPadding = new(2, 0);
        public static readonly UiPadding Padding = new(2);

        public static readonly Utf8String AllowRaycastName = "blocksRaycast";
        public const bool AllowRaycast = true;

        public static readonly Utf8String InteractableName = "interactable";
        public const bool Interactable = true;

    }

    public static class RectTransform
    {
        public static readonly Utf8String AnchorMinName = "anchormin";
        public static readonly Vector2 AnchorMin = new(0, 0);
        public static readonly Utf8String AnchorMaxName = "anchormax";
        public static readonly Vector2 AnchorMax = new(1, 1);
        
        public static readonly Utf8String OffsetMinName = "offsetmin";
        public static readonly Vector2 OffsetMin = new(0, 0);
        public static readonly Utf8String OffsetMaxName = "offsetmax";
        public static readonly Vector2 OffsetMax = new(1, 1);

        public static readonly UiOffset FpOffset = new(0, 0, 1, 1);
        
        public static readonly Utf8String RotationName = "rotation";
        public static readonly UiRotation Rotation = new(0);

        public static readonly Utf8String PivotName = "pivot";
        public static readonly UiPivot Pivot = new(0.5f, 0.5f);
        
        public static readonly Utf8String SetParentName = "setParent";
        
        public static readonly Utf8String SetTransformIndexName = "setTransformIndex";
        public const int SetTransformIndex = -1;
        
        public static readonly UiScale Scale = UiScale.One;
        public static readonly UiTranslate Translate = UiTranslate.ZeroPx;
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

    public static class Text
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Text";
       
        public static readonly Utf8String FontName = "font";
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        
        public static readonly Utf8String TextName = "text";
        
        public static readonly Utf8String FontSizeName = "fontSize";
        public const int FontSize = 14;
        
        public static readonly Utf8String AlignName = "align";
        public const TextAnchor Align = TextAnchor.UpperLeft;
        
        public static readonly Utf8String VerticalOverflowName = "verticalOverflow";
        public const VerticalWrapMode VerticalOverflow = VerticalWrapMode.Truncate;
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
        public static readonly Utf8String ImageTypeName = "imagetype";
        public const UnityEngine.UI.Image.Type ImageType = UnityEngine.UI.Image.Type.Simple;
        
        public static readonly Utf8String FillCenterName = "fillCenter";
        public const bool FillCenter = true;
        
        public static readonly Utf8String SliceName = "slice";
        public static readonly UiBorderWidth Slice = UiBorderWidth.Empty;

        public static readonly Utf8String PixelPerUnitMultiplierName = "ppuMultiplier";
        public const float PixelPerUnitMultiplier = 1f;
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
        public const int CharacterLimit = 0;
        public static readonly Utf8String PasswordName = "password";
        public static readonly Utf8String ReadOnlyName = "readOnly";
        public static readonly Utf8String LineTypeName = "lineType";
        public const InputField.LineType LineType = InputField.LineType.SingleLine;
        public static readonly Utf8String NeedsKeyboardName = "needsKeyboard";
        public static readonly Utf8String NeedsHudKeyboardName = "hudMenuInput";
        public static readonly Utf8String AutoFocusName = "autofocus";
        public static readonly Utf8String PlaceholderName = "placeholderId";
        public const InputMode Mode = InputMode.Default;
    }
        
    public static class Countdown
    {
        public static readonly Utf8String Type = "Countdown";
        
        public static readonly Utf8String StartTimeName = "startTime";
        public const float StartTime = 0;
        
        public static readonly Utf8String EndTimeName = "endTime";
        public const float EndTime = 0;
        
        public static readonly Utf8String StepName = "step";
        public const float Step = 1;
        
        public static readonly Utf8String IntervalName = "interval";
        public const float Interval = 1;
        
        public static readonly Utf8String TimerFormatName = "timerFormat";
        public const TimerFormat TimerFormat = Enums.TimerFormat.None;
        
        public static readonly Utf8String NumberFormatName = "numberFormat";
        public const string NumberFormat = "0.####";
        
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
        
        public static readonly Vector2 OffsetMax = new(0, 0);
        
        public static readonly Utf8String PivotName = "pivot";
        public static readonly UiPivot Pivot = UiPivot.Center;
        
        public static readonly Utf8String HorizontalScrollProgressName = "horizontalNormalizedPosition";
        public const float HorizontalScrollProgress = 0f;
        
        public static readonly Utf8String VerticalScrollProgressName = "verticalNormalizedPosition";
        public const float VerticalScrollProgress = 1f;
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

        public static readonly Utf8String FadeDurationName = "fadeDuration";
        public const float FadeDuration = 0.1f;
    }

    public static class ColorBlock
    {
        public static readonly Utf8String HighlightedColorName = "highlightedColor";
        public static readonly UiColor HighlightedColor = UiColor.ParseHexColor("#F5F5F5FF");
        
        public static readonly Utf8String PressedColorName = "pressedColor";
        public static readonly UiColor PressedColor = UiColor.ParseHexColor("#C8C8C8FF");
        
        public static readonly Utf8String SelectedColorName = "selectedColor";
        public static readonly UiColor SelectedColor = UiColor.ParseHexColor("#F5F5F5FF");

        public static readonly Utf8String DisabledColorName = "selectedColor";
        public static readonly UiColor DisabledColor = UiColor.ParseHexColor("##808080FF");
        
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
        public const bool DropAnywhere = true;
        
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
        public static readonly CommunityEntity.DraggablePositionSendType? PositionRpc = null;
        
        public static readonly Utf8String MoveToAnchorName = "moveToAnchor";
        public const bool MoveToAnchor = false;
        
        public static readonly Utf8String RebuildAnchorName = "rebuildAnchor";
        public const bool RebuildAnchor = false;
    }

    public static class Slot
    {
        public static readonly Utf8String Type = "Slot";
        public static readonly Utf8String FilterName = "filter";
    }

    public static class Layout
    {
        public static readonly Utf8String PaddingName = "padding";
        public static readonly UiPadding Padding = default;
        
        public static readonly Utf8String ChildAlignmentName = "childAlignment";
        public const TextAnchor ChildAlignment = TextAnchor.UpperLeft;
    }
    
    public static class DirectionalLayout
    {
        public static readonly Utf8String HorizontalType = "UnityEngine.UI.HorizontalLayoutGroup";
        public static readonly Utf8String VerticalType = "UnityEngine.UI.VerticalLayoutGroup";
        
        public static readonly Utf8String SpacingName = "spacing";
        public const float Spacing = 0f;
        
        public static readonly Utf8String ChildForceExpandWidthName = "childForceExpandWidth";
        public const bool ChildForceExpandWidth = true;
        
        public static readonly Utf8String ChildForceExpandHeightName = "childForceExpandHeight";
        public const bool ChildForceExpandHeight = true;
        
        public static readonly Utf8String ChildControlWidthName = "childControlWidth";
        public const bool ChildControlWidth = false;
        
        public static readonly Utf8String ChildControlHeightName = "childControlHeight";
        public const bool ChildControlHeight = false;
        
        public static readonly Utf8String ChildScaleWidthName = "childScaleWidth";
        public const bool ChildScaleWidth = false;
        
        public static readonly Utf8String ChildScaleHeightName = "childScaleHeight";
        public const bool ChildScaleHeight = false;
    }

    public static class GridLayout
    {
        public static readonly Utf8String Type = "UnityEngine.UI.GridLayoutGroup";
        
        public static readonly Utf8String CellSizeName = "cellSize";
        public static readonly Vector2 CellSize = new(100, 100);
        
        public static readonly Utf8String SpacingName = "spacing";
        public static readonly Vector2 Spacing = new(0, 0);
        
        public static readonly Utf8String StartCornerName = "startCorner";
        public const GridLayoutGroup.Corner StartCorner = GridLayoutGroup.Corner.UpperLeft;
        
        public static readonly Utf8String StartAxisName = "startAxis";
        public const GridLayoutGroup.Axis StartAxis = GridLayoutGroup.Axis.Horizontal;
        
        public static readonly Utf8String ConstraintName = "constraint";
        public const GridLayoutGroup.Constraint Constraint = GridLayoutGroup.Constraint.Flexible;
        
        public static readonly Utf8String ConstraintCountName = "constraintCount";
        public const int ConstraintCount = 2;
    }

    public static class ContentSizeFitterData
    {
        public static readonly Utf8String Type = "UnityEngine.UI.ContentSizeFitter";
        
        public static readonly Utf8String HorizontalFitName = "horizontalFit";
        public const ContentSizeFitter.FitMode HorizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        
        public static readonly Utf8String VerticalFitName = "verticalFit";
        public const ContentSizeFitter.FitMode VerticalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    public static class LayoutElement
    {
        public static readonly Utf8String Type = "UnityEngine.UI.LayoutElement";
        
        public static readonly Utf8String PreferredWidthName = "preferredWidth";
        public const float PreferredWidth = -1f;
        
        public static readonly Utf8String PreferredHeightName = "preferredHeight";
        public const float PreferredHeight = -1f;
        
        public static readonly Utf8String MinWidthName = "minWidth";
        public const float MinWidth = -1f;
        public const float FpMinWidth = 0f;
        
        public static readonly Utf8String MinHeightName = "minHeight";
        public const float MinHeight = -1f;
        public const float FpMinHeight = 0f;
        
        public static readonly Utf8String FlexibleWidthName = "flexibleWidth";
        public const float FlexibleWidth = -1f;
        public const float FpFlexibleWidth = 0f;
        
        public static readonly Utf8String FlexibleHeightName = "flexibleHeight";
        public const float FlexibleHeight = -1f;
        public const float FpFlexibleHeight = 0f;
        
        public static readonly Utf8String IgnoreLayoutName = "ignoreLayout";
        public const bool IgnoreLayout = false;
    }

    public static class CanvasGroup
    {
        public static readonly Utf8String Type = "UnityEngine.UI.CanvasGroup";

        public static readonly Utf8String AlphaName = "alpha";
        public const float Alpha = 1f;

        public static readonly Utf8String FadeName = "fade";
        public static readonly UiCanvasGroupFade Fade = new(0, 1);
    }

    public static class Mask
    {
        public static readonly Utf8String Type = "UnityEngine.UI.Mask";

        public static readonly Utf8String ShowMaskGraphicName = "showMaskGraphic";
        public const bool ShowMaskGraphic = true;
    }

    public static class ToolTip
    {
        public static readonly Utf8String Type = "Tooltip";

        public static readonly Utf8String TextName = "text";
        public const string Text = null;

        public static readonly Utf8String TooltipTypeName = "tooltipType";
        public const CommunityEntity.TooltipType TooltipType = CommunityEntity.TooltipType.Default;

        public static readonly Utf8String DelayName = "delay";
        public const Tooltip.DelayType Delay = Tooltip.DelayType.Short;

        public static readonly Utf8String PositionName = "position";
        public const TooltipContainer.PositionMode Position = TooltipContainer.PositionMode.Auto;

        public static readonly Utf8String OffsetName = "offset";
        public static readonly Vector2 Offset = new(8, 8);

        public static readonly Utf8String UseCenterName = "useCentre";
        public const bool UseCenter = true;
    }
}