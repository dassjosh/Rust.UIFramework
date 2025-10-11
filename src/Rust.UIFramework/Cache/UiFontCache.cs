using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Helpers;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiFontCache
{
    private static readonly string[] Fonts = CacheHelpers.ExtractCache(typeof(UiFonts), typeof(UiFont));
    public static string GetUiFont(UiFont font) => Fonts[(byte)font];
}