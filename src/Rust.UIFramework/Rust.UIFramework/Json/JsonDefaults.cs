using UI.Framework.Rust.Colors;
// ReSharper disable ConvertToConstant.Global

namespace UI.Framework.Rust.Json
{
    public class JsonDefaults
    {
        //Position & Offset
        private const string DefaultMin = "0.0 0.0";
        private const string DefaultMax = "1.0 1.0";
        public static readonly string RectTransformName = "RectTransform";
        public static readonly string AnchorMinName = "anchormin";
        public static readonly string AnchorMaxName = "anchormax";
        public static readonly string OffsetMinName = "offsetmin";
        public static readonly string OffsetMaxName = "offsetmax";
        public static readonly string[] DefaultMinValues = { DefaultMin, "0 0" };
        public static readonly string[] DefaultMaxValues = { DefaultMax, "1 1" };
        public static readonly string OffsetMaxValue = "0 0";
        
        //Text
        public static readonly string AlignValue = "UpperLeft";
        public static int FontSizeValue = 14;
        public static readonly string FontValue = "RobotoCondensed-Bold.ttf";
        public static readonly string FontName = "font";
        public static readonly string TextName = "text";
        public static readonly string TextValue = "Text";
        public static readonly string FontSizeName = "fontSize";
        public static readonly string AlignName = "align";
        
        //Material & Sprite
        public static readonly string SpriteName = "sprite";
        public static readonly string MaterialName = "material";
        public static readonly string SpriteValue = "Assets/Content/UI/UI.Background.Tile.psd";
        public static readonly string SpriteImageValue = "Assets/Icons/rust.png";
        public static readonly string MaterialValue = "Assets/Icons/IconMaterial.mat";
        
        //Common
        public static readonly string ComponentTypeName = "type";
        public static readonly string ColorName = "color";
        public static readonly string NullValue = null;
        public static readonly string EmptyString = "";
        public static readonly string ComponentName = "name";
        public static readonly string ParentName = "parent";
        public static readonly string FadeInName = "fadeIn";
        public static readonly string FadeOutName = "fadeOut";
        public static float FadeOutValue;
        public static readonly UiColor ColorValue = "1 1 1 1";
        
        //Outline
        public static readonly string DistanceName = "distance";
        public static readonly string UseGraphicAlphaName = "useGraphicAlpha";
        public static readonly string UseGraphicAlphaValue = "True";
        public static readonly string DistanceValue = "1.0 -1.0";
        
        //Button
        public static readonly string CommandName = "command";
        public static readonly string CloseName = "close";
        
        //Needs Cursor
        public static readonly string NeedsCursorValue = "NeedsCursor";
        
        //Image
        public static readonly string PNGName = "png";
        public static readonly string URLName = "url";
        
        //Input
        public static readonly string CharacterLimitName = "characterLimit";
        public static int CharacterLimitValue;
        public static readonly string PasswordName = "password";
        public static readonly string PasswordValue = "true";
    }
}