using System.IO;
using Oxide.Ext.UiFramework.Cache;

namespace Oxide.Ext.UiFramework.Constants;

internal static class PathConstants
{
    internal static readonly string ConfigFolder = Path.Combine(OxideLibrary.ConfigFolder, "UiFramework");
    internal static readonly string ThemeFolder = Path.Combine(OxideLibrary.ConfigFolder, "UiFramework", "Themes");
    internal static readonly string DataFolder = Path.Combine(OxideLibrary.DataFolder, "UiFramework");
        
    static PathConstants()
    {
        if (!Directory.Exists(ConfigFolder))
        {
            Directory.CreateDirectory(ConfigFolder);
        }

        if (!Directory.Exists(DataFolder))
        {
            Directory.CreateDirectory(DataFolder);
        }
    }
}