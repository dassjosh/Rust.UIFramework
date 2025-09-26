using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public static class PartialCommand
{
    private static readonly Dictionary<WriterKey, IArgWriter[]> TypedWriters = new();

    internal static ICommandBuilder<T> Create<T>(string command) => Create<T>(UiFrameworkPlugin.Instance.PluginId, command);
    internal static ICommandBuilder<T0, T1> Create<T0, T1>(string command) => Create<T0, T1>(UiFrameworkPlugin.Instance.PluginId, command);

    public static ICommandBuilder<T> Create<T>(IUiFrameworkPlugin plugin, string command) => Create<T>(plugin.Id(), command);
    public static ICommandBuilder<T0, T1> Create<T0, T1>(IUiFrameworkPlugin plugin, string command)=> Create<T0, T1>(plugin.Id(), command);
    
    private static ICommandBuilder<T> Create<T>(PluginId plugin, string command) => new CommandBuilder<T>(command, null, GetWriters<T>(plugin));
    private static ICommandBuilder<T0, T1> Create<T0, T1>(PluginId plugin, string command) => new CommandBuilder<T0, T1>(command, null, GetWriters<T0, T1>(plugin));
    
    private static IArgWriter[] GetWriters<T0>(PluginId plugin)
    {
        WriterKey key = new(typeof(T0), null);
        if (!TypedWriters.TryGetValue(key, out IArgWriter[] writers))
        {
            TypedWriters[key] = writers = ArgCreator.CreateArgHandler<T0>(plugin);
        }
        
        return writers;
    }
    
    private static IArgWriter[] GetWriters<T0, T1>(PluginId plugin)
    {
        WriterKey key = new(typeof(T0), typeof(T1));
        if (!TypedWriters.TryGetValue(key, out IArgWriter[] writers))
        {
            TypedWriters[key] = writers = ArgCreator.CreateArgHandler<T0, T1>(plugin);
        }
        
        return writers;
    }

    private readonly record struct WriterKey(Type T0, Type T1);
}