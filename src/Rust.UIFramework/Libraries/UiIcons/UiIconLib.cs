using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate string IconUrlLookup<in T>(T icon);
public delegate void IconStyling(UiIcon icon);

public class UiIconLib : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginId, List<Type>> _pluginIcons = new();
    private readonly Dictionary<Type, IIconData> _enumIconData = new();
    private readonly IUiLogger<UiIconLib> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiIconLib>();

    private UiIconLib() { }
    
    public void RegisterIcons<T>(Plugin plugin, IconUrlLookup<T> urlLookup, IconStyling styling = null) where T : Enum
    {
        PluginId pluginId = plugin.Id();
        if(!_pluginIcons.TryGetValue(pluginId, out List<Type> icons))
        {
            _pluginIcons[pluginId] = icons = [];
        }

        Type type = typeof(T);
        if (icons.Contains(type))
        {
            throw new Exception($"Icon already registered for {type}");
        }
        
        if(_enumIconData.TryGetValue(type, out IIconData data))
        {
            throw new Exception($"Icon already registered for {type} by {data.Id.FullName()}");
        }
        
        icons.Add(type);
        _enumIconData[type] = new PluginIconData<T>(pluginId, urlLookup, styling);
        _logger.Debug("Registered icon for plugin: {0} type: {1}", pluginId.FullName(), typeof(T));
    }

    internal void PopulateIconData<T>(UiIcon icon, T value) where T : struct, Enum
    {
        if(_enumIconData.TryGetValue(typeof(T), out IIconData data))
        {
            ((PluginIconData<T>)data).PopulateIconData(icon, value);
            return;
        }
        
        throw new Exception($"No icon registered for {typeof(T)}");
    }
    
    protected override void OnPluginUnloaded(Plugin plugin)
    {
        if(_pluginIcons.Remove(plugin.Id(), out List<Type> icons))
        {
            foreach (Type type in icons)
            {
                _enumIconData.Remove(type);
            }
        }
    }
}

internal interface IIconData
{
    PluginId Id { get; }
}
    
file sealed class PluginIconData<T>(PluginId id, IconUrlLookup<T> urlLookup, IconStyling styling) : IIconData
{
    public PluginId Id { get; } = id;
    private readonly Dictionary<T, string> _cachedUrls = new();

    private string GetUrl(T value)
    {
        if (!_cachedUrls.TryGetValue(value, out string url))
        {
            _cachedUrls[value] = url = urlLookup(value);
        }

        return url;
    }

    public void PopulateIconData(UiIcon icon, T value)
    {
        string url = GetUrl(value);
        string png = Singleton<UiImageStorage>.Instance.Get(Id, url, null);
        icon.RawImage.Image = png;
        styling?.Invoke(icon);
    }
}