using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public static class PartialCommand
{
    private static readonly Dictionary<WriterKey, IArgWriter[]> TypedWriters = new();
    
    public static ICommandBuilder<T> Create<T>(string command) => new CommandBuilder<T>(command, null, GetWriters<T>());
    public static ICommandBuilder<T0, T1> Create<T0, T1>(string command) => new CommandBuilder<T0, T1>(command, null, GetWriters<T0, T1>());
    
    private static IArgWriter[] GetWriters<T0>()
    {
        WriterKey key = new(typeof(T0), null);
        if (!TypedWriters.TryGetValue(key, out IArgWriter[] writers))
        {
            TypedWriters[key] = writers = ArgCreator.CreateArgHandler<T0>(UiFrameworkPlugin.Instance.Id());
        }
        
        return writers;
    }
    
    private static IArgWriter[] GetWriters<T0, T1>()
    {
        WriterKey key = new(typeof(T0), typeof(T1));
        if (!TypedWriters.TryGetValue(key, out IArgWriter[] writers))
        {
            TypedWriters[key] = writers = ArgCreator.CreateArgHandler<T0, T1>(UiFrameworkPlugin.Instance.Id());
        }
        
        return writers;
    }

    private readonly record struct WriterKey(Type T0, Type T1);
}