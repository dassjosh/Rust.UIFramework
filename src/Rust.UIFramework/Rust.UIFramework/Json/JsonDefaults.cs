using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Json
{
    public static class JsonDefaults
    {
        //Position & Offset
        public const string RectTransformName = "RectTransform";
        public const string AnchorMinName = "anchormin";
        public const string AnchorMaxName = "anchormax";
        public const string OffsetMinName = "offsetmin";
        public const string OffsetMaxName = "offsetmax";
        public static readonly Vector2 AnchorMin = new Vector2(0, 0);
        public static readonly Vector2 AnchorMax = new Vector2(1, 1);
        public static readonly Vector2Int OffsetMin = new Vector2Int(0, 0);
        public static readonly Vector2Int OffsetMax = new Vector2Int(0, 0);
        public const string DefaultOffsetMax = "0 0";

        //Text
        public const int FontSizeValue = 14;
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public const string FontName = "font";
        public const string TextName = "text";
        public const string FontSizeName = "fontSize";
        public const string AlignName = "align";
        
        //Material & Sprite
        public const string SpriteName = "sprite";
        public const string MaterialName = "material";
        public const string SpriteValue = "Assets/Content/UI/UI.Background.Tile.psd";
        public const string SpriteImageValue = "Assets/Icons/rust.png";
        public const string MaterialValue = "Assets/Icons/IconMaterial.mat";
        
        //Common
        public const string ComponentTypeName = "type";
        public const string ColorName = "color";
        public const string NullValue = null;
        public const string EmptyString = "";
        public const string ComponentName = "name";
        public const string ParentName = "parent";
        public const string FadeInName = "fadeIn";
        public const string FadeOutName = "fadeOut";
        public const float FadeOutValue = 0;
        public static readonly UiColor ColorValue = "1 1 1 1";
        
        //Outline
        public const string DistanceName = "distance";
        public const string UseGraphicAlphaName = "useGraphicAlpha";
        public const string UseGraphicAlphaValue = "True";
        public static readonly Vector2 DistanceValue = new Vector2(1.0f, -1.0f);
        
        //Button
        public const string CommandName = "command";
        public const string CloseName = "close";
        
        //Needs Cursor
        public const string NeedsCursorValue = "NeedsCursor";
        
        //Needs Cursor
        public const string NeedsKeyboardValue = "NeedsKeyboard";
        
        //Image
        public const string PNGName = "png";
        public const string UrlName = "url";
        
        //Item Icon
        public const string ItemIdName = "itemid";
        public const string SkinIdName = "skinid";
        public const ulong DefaultSkinId = 0;
        
        //Input
        public const string CharacterLimitName = "characterLimit";
        public const int CharacterLimitValue = 0;
        public const string PasswordName = "password";
        public const string PasswordValue = "true";
        public const string ReadOnlyName = "password";
        public const bool ReadOnlyValue = true;
        public const string LineTypeName = "lineType";
        public const string InputNeedsKeyboardName = "needsKeyboard";
        public const string InputNeedsKeyboardValue = "";
        
        //Countdown
        public const string StartTimeName = "startTime";
        public const int StartTimeValue = 0;
        public const string EndTimeName = "endTime";
        public const int EndTimeValue = 0;
        public const string StepName = "step";
        public const int StepValue = 1;
        public const string CountdownCommandName = "command";
    }
}