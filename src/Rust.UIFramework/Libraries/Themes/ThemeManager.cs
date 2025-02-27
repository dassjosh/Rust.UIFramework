using System.IO;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.Themes;

public class ThemeManager : BaseUiFrameworkLibrary, ISingleton
{
    private ThemeManager() { }
    
    public T RegisterTheme<T>(Plugin plugin, T theme) where T : ITheme => RegisterTheme(plugin, null, theme);
    
    public T RegisterTheme<T>(Plugin plugin, string name, T theme) where T : ITheme
    {
        string path = Path.Combine(PathConstants.ThemeFolder, plugin.Name);
        Directory.CreateDirectory(path);
        string fileName = string.IsNullOrEmpty(name) ? $"{plugin.Name}.json" : $"{name}.json";
        string filePath = Path.Combine(path, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonConvert.PopulateObject(json, theme);
        }
        
        File.WriteAllText(filePath, JsonConvert.SerializeObject(theme));
        return theme;
    }
}