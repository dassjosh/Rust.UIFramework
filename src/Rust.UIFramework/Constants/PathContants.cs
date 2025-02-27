using System.IO;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Constants;

internal static class PathConstants
{
    internal static readonly string ConfigFolder = Path.Combine(Interface.Oxide.ConfigDirectory, "UiFramework");
    internal static readonly string ThemeFolder = Path.Combine(Interface.Oxide.ConfigDirectory, "UiFramework", "Themes");
    internal static readonly string DataFolder = Path.Combine(Interface.Oxide.DataDirectory, "UiFramework");
        
    static PathConstants()
    {
        if (!Directory.Exists(ConfigFolder))
        {
            Directory.CreateDirectory(ConfigFolder);
        }
            
        if (!Directory.Exists(ThemeFolder))
        {
            Directory.CreateDirectory(ThemeFolder);
        }

        if (!Directory.Exists(DataFolder))
        {
            Directory.CreateDirectory(DataFolder);
        }
    }
}