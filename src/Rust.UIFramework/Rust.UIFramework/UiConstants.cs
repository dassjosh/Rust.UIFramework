using Oxide.Plugins;
using UI.Framework.Rust.Enums;

namespace UI.Framework.Rust
{
    public class UiConstants
    {
        public static class UiFonts
        {
            private const string DroidSansMono = "droidsansmono.ttf";
            private const string PermanentMarker = "permanentmarker.ttf";
            private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
            private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";

            private static readonly Hash<UiFont, string> _fonts = new Hash<UiFont, string>
            {
                [UiFont.DroidSansMono] = DroidSansMono,
                [UiFont.PermanentMarker] = PermanentMarker,
                [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
                [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
            };

            public static string GetUiFont(UiFont font)
            {
                return _fonts[font];
            }
        }

        public static class UiLayers
        {
            private const string Overall = "Overall";
            private const string Overlay = "Overlay";
            private const string Hud = "Hud";
            private const string HudMenu = "Hud.Menu";
            private const string Under = "Under";

            private static readonly Hash<UiLayer, string> _layers = new Hash<UiLayer, string>
            {
                [UiLayer.Overall] = Overall,
                [UiLayer.Overlay] = Overlay,
                [UiLayer.Hud] = Hud,
                [UiLayer.HudMenu] = HudMenu,
                [UiLayer.Under] = Under,
            };

            public static string GetLayer(UiLayer layer)
            {
                return _layers[layer];
            }
        }

        public static class RpcFunctions
        {
            public const string AddUiFunc = "AddUI";
            public const string DestroyUiFunc = "DestroyUI";
        }

        public static class Json
        {
            public const char QuoteChar = '\"';
        }
    }
}