using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
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
            RustIcons[icon] = url = $"https://rust-images.joshdass.dev/icons/{(int)icon}.png";
        }
        
        return Singleton<UiImageStorage>.Instance.Get(UiFrameworkPlugin.Instance,  url);
    }
}