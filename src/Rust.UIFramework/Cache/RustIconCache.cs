using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Rust.UI;

namespace Oxide.Ext.UiFramework.Cache;

public static class RustIconCache
{
    private static readonly Dictionary<Icons, string> RustIcons = new();

    public static string GetIcon(Icons icon)
    {
        if (!RustIcons.TryGetValue(icon, out string url))
        {
            RustIcons[icon] = url = GetIconUrl(icon);
        }
        
        return Singleton<ImageStorage>.Instance.Get(url);
    }

    private static string GetIconUrl(Icons icon)
    {
        return $"https://abc.123/images/uiframework/{(int)icon}.png";
    }
}