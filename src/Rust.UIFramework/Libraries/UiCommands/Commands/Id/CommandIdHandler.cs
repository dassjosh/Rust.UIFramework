using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandIdHandler : ISingleton
{
    private readonly Dictionary<IntPtr, uint> _methodIds = new();
    private uint _nextId;

    private CommandIdHandler() { }
    
    public uint GetId(MethodInfo method)
    {
        IntPtr ptr = method.MethodHandle.Value;
        if (!_methodIds.TryGetValue(ptr, out uint id))
        {
            _methodIds[ptr] = id = _nextId++;
        }

        return id;
    }
}