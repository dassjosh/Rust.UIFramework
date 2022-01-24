using UI.Framework.Rust.Colors;
// ReSharper disable ConvertToConstant.Global

namespace UI.Framework.Rust.Json
{
    public class JsonDefaults
    {
        //Position & Offset
        private const string DefaultMin = "0.0 0.0";
        private const string DefaultMax = "1.0 1.0";
        public const string RectTransformName = "RectTransform";
        public const string AnchorMinName = "anchormin";
        public const string AnchorMaxName = "anchormax";
        public const string OffsetMinName = "offsetmin";
        public const string OffsetMaxName = "offsetmax";
        public static readonly string[] DefaultMinValues = { DefaultMin, "0 0" };
        public static readonly string[] DefaultMaxValues = { DefaultMax, "1 1" };
        public const string OffsetMaxValue = "0 0";
        
        //Text
        public const string AlignValue = "UpperLeft";
        public const int FontSizeValue = 14;
        public const string FontValue = "RobotoCondensed-Bold.ttf";
        public const string FontName = "font";
        public const string TextName = "text";
        public const string TextValue = "Text";
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
        public const string DistanceValue = "1.0 -1.0";
        
        //Button
        public const string CommandName = "command";
        public const string CloseName = "close";
        
        //Needs Cursor
        public const string NeedsCursorValue = "NeedsCursor";
        
        //Image
        public const string PNGName = "png";
        public const string URLName = "url";
        
        //Input
        public const string CharacterLimitName = "characterLimit";
        public const int CharacterLimitValue = 0;
        public const string PasswordName = "password";
        public const string PasswordValue = "true";
    }
}