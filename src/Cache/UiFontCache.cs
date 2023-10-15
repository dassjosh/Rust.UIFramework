using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class UiFontCache
    {
        private const string DroidSansMono = "droidsansmono.ttf";
        private const string PermanentMarker = "permanentmarker.ttf";
        private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
        private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
        private const string PressStart2PRegular = "PressStart2P-Regular.ttf";

        private static readonly Dictionary<UiFont, string> Fonts = new Dictionary<UiFont, string>
        {
            [UiFont.DroidSansMono] = DroidSansMono,
            [UiFont.PermanentMarker] = PermanentMarker,
            [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
            [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
            [UiFont.PressStart2PRegular] = PressStart2PRegular,
        };

        public static string GetUiFont(UiFont font)
        {
            return Fonts[font];
        }
    }
}