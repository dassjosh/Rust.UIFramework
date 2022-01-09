using UI.Framework.Rust.Enums;

namespace Oxide.Plugins
{
    public class Constants
    {
        public static class UiFonts
        {
            public static readonly string DroidSansMono = "droidsansmono.ttf";
            public static readonly string PermanentMarker = "permanentmarker.ttf";
            public static readonly string RobotoCondensedBold = "robotocondensed-bold.ttf";
            public static readonly string RobotoCondensedRegular = "robotocondensed-regular.ttf";

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
            public static readonly string Overall = "Overall";
            public static readonly string Overlay = "Overlay";
            public static readonly string Hud = "Hud";
            public static readonly string HudMenu = "Hud.Menu";
            public static readonly string Under = "Under";
            
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
            public static readonly string AddUiFunc = "AddUI";
            public static readonly string DestroyUiFunc = "DestroyUI";
        }

        public static class Json
        {
            public static readonly char QuoteChar = '\"';
        }
    }
}