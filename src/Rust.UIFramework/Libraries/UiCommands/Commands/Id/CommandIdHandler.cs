using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandIdHandler
{
    private readonly Dictionary<IntPtr, uint> _methodIds = new();
    private readonly Dictionary<PluginId, List<IntPtr>> _pluginMethods = new();
    private uint _nextId;
    
    public uint GetId(PluginId pluginId, MethodInfo method)
    {
        IntPtr ptr = method.MethodHandle.Value;
        if (!_methodIds.TryGetValue(ptr, out uint id))
        {
            _methodIds[ptr] = id = _nextId++;
            AddCommandId(pluginId, ptr);
        }

        return id;
    }
    
    public IEnumerable<CommandId> GetPluginCommands(PluginId pluginId)
    {
        if (_pluginMethods.TryGetValue(pluginId, out List<IntPtr> ids))
        {
            foreach (IntPtr ptr in ids)
            {
                yield return new CommandId(_methodIds[ptr]);
            }
        }
    }

    private void AddCommandId(PluginId pluginId, IntPtr ptr)
    {
        if (!_pluginMethods.TryGetValue(pluginId, out List<IntPtr> ids))
        {
            _pluginMethods[pluginId] = ids = [];
        }
        
        ids.Add(ptr);
    }

    public void OnPluginUnloaded(PluginId pluginId)
    {
        if (_pluginMethods.Remove(pluginId, out List<IntPtr> ids))
        {
            foreach (IntPtr ptr in ids)
            {
                _methodIds.Remove(ptr);
            }
        }
    }
}