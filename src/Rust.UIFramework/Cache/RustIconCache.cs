using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Cache;

internal static class RustIconCache
{
    private static readonly Dictionary<Icons, string> RustIcons = new();

    private const string Format = UiImageDefaults.BaseUrl + "rust-images/{0}.png";
    
    internal static string GetIcon(Icons icon)
    {
        if (!RustIcons.TryGetValue(icon, out string url))
        {
            RustIcons[icon] = url = string.Format(Format, (int)icon);
        }

        return url;
    }
}