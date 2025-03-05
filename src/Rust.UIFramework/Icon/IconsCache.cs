using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Libraries;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Icon;

internal static class IconsCache
{
    // private static readonly Dictionary<Icons, string> RustIcons = new();
    // private static readonly Dictionary<FontAwesomeRegularIcons, string> FontAwesomeRegular = new();
    // private static readonly Dictionary<FontAwesomeSolidIcons, string> FontAwesomeSolid = new();

    public const string RustIconsFormat = UiImageDefaults.BaseUrl + "rust-icons/{0}.png";
    public const string FontAwesomeRegularFormat = UiImageDefaults.BaseUrl + "font-awesome/regular/{0}.png";
    public const string FontAwesomeSolidFormat = UiImageDefaults.BaseUrl + "font-awesome/solid/{0}.png";
    //
    // internal static string GetIcon(Icons icon)
    // {
    //     if (!RustIcons.TryGetValue(icon, out string url))
    //     {
    //         RustIcons[icon] = url = string.Format(RustIconsFormat, EnumCache<Icons>.ToNumber(icon));
    //     }
    //
    //     return url;
    // }
    //
    // internal static string GetIcon(FontAwesomeRegularIcons icon)
    // {
    //     if (!FontAwesomeRegular.TryGetValue(icon, out string url))
    //     {
    //         FontAwesomeRegular[icon] = url = string.Format(FontAwesomeRegularFormat, EnumCache<FontAwesomeRegularIcons>.ToString(icon));
    //     }
    //
    //     return url;
    // }
    //
    // internal static string GetIcon(FontAwesomeSolidIcons icon)
    // {
    //     if (!FontAwesomeSolid.TryGetValue(icon, out string url))
    //     {
    //         FontAwesomeSolid[icon] = url = string.Format(FontAwesomeSolidFormat, EnumCache<FontAwesomeSolidIcons>.ToString(icon));
    //     }
    //
    //     return url;
    // }
}