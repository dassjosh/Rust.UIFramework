using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiIcons;

public class UiIcons : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginIcon, IconId> _pluginIconId = new();
    private readonly Dictionary<IconId, PluginIconData> _iconData = new();
    private readonly Dictionary<Type, IconId> _enumIconData = new();
    private ushort _nextIconId;

    public void RegisterIcons<T>(Plugin plugin, Func<T, string> urlLookup) where T : Enum
    {
        PluginIcon pluginIcon = new(plugin.Id(), typeof(T));
        IconId iconId = new(_nextIconId++);
        _pluginIconId[pluginIcon] = iconId;
        _enumIconData[typeof(T)] = iconId;
        _iconData[iconId] = new PluginIconData<T>(urlLookup);
    }

    internal IconId GetIconId(Enum @enum) => GetIconId(@enum.GetType());
    
    internal IconId GetIconId(Type enunType)
    {
        if (_enumIconData.TryGetValue(enunType, out IconId id))
        {
            return id;
        }
        
        throw new Exception($"No icon registered for enum type {enunType}");
    }

    internal string GetIconUrl(IconId id, ushort icon) => _iconData[id]?.GetUrl(icon);
    
    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        foreach (KeyValuePair<PluginIcon, IconId> icons in _pluginIconId.Where(p => p.Key.PluginId == pluginId))
        {
            _iconData.Remove(icons.Value);
        }
        
        _pluginIconId.RemoveAll(p => p.Key.PluginId == pluginId);
    }

    private abstract class PluginIconData
    {
        public abstract string GetUrl(ushort value);
    }
    
    private class PluginIconData<T> : PluginIconData
    {
        public readonly Func<T, string> URLLookup;
        public readonly Dictionary<T, string> CachedUrls = new();

        public PluginIconData(Func<T, string> urlLookup)
        {
            URLLookup = urlLookup;
        }
        
        public override string GetUrl(ushort value)
        {
            T icon = (T) Enum.ToObject(typeof(T), value);
            if (!CachedUrls.TryGetValue(icon, out string url))
            {
                CachedUrls[icon] = url = URLLookup(icon);
            }

            return url;
        }
    }
}