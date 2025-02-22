using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandIdHandler
{
    private readonly Dictionary<IntPtr, uint> _methodIds = new();
    private readonly Dictionary<PluginId, List<uint>> _pluginCommands = new();
    private readonly List<uint> _emptyList = [];
    private uint _nextId;
    
    public uint GetId(PluginId pluginId, MethodInfo method)
    {
        IntPtr ptr = method.MethodHandle.Value;
        if (!_methodIds.TryGetValue(ptr, out uint id))
        {
            _methodIds[ptr] = id = _nextId++;
            AddCommandId(pluginId, id);
        }

        return id;
    }
    
    public List<uint> GetCommandIds(PluginId pluginId) => _pluginCommands.GetValueOrDefault(pluginId, _emptyList);

    private void AddCommandId(PluginId pluginId, uint id)
    {
        if (!_pluginCommands.TryGetValue(pluginId, out List<uint> ids))
        {
            _pluginCommands[pluginId] = ids = new List<uint>();
        }
        
        ids.Add(id);
    }

    public void OnPluginUnloaded(PluginId pluginId)
    {
        if (_pluginCommands.Remove(pluginId, out List<uint> ids))
        {
            _methodIds.RemoveAll(mi => ids.Contains(mi.Value));
        }
    }
}