using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public static class PartialCommand
{
    private static readonly Dictionary<WriterKey, IArgWriter[]> TypedWriters = new();

    internal static ICommandBuilder<T> Create<T>(string command) => Create<T>(UiFrameworkPlugin.Instance, command);
    internal static ICommandBuilder<T0, T1> Create<T0, T1>(string command) => Create<T0, T1>(UiFrameworkPlugin.Instance, command);

    public static ICommandBuilder<T> Create<T>(IUiFrameworkPlugin plugin, string command)
    {
        return new CommandBuilder<T>(new PartialWriterCommand(plugin, command, GetWriters<T>(plugin.Id())));
    }

    public static ICommandBuilder<T0, T1> Create<T0, T1>(IUiFrameworkPlugin plugin, string command)
    {
        return new CommandBuilder<T0, T1>(new PartialWriterCommand(plugin, command, GetWriters<T0, T1>(plugin.Id())));
    }
    
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
    
    private sealed class PartialWriterCommand(IUiFrameworkPlugin plugin, string command, IArgWriter[] writers) : ICommandBuilderData
    {
        public IUiFrameworkPlugin Plugin { get; } = plugin;
        public string Command { get; } = command;

        public IArgWriter[] Writers { get; } = writers;
        public ICommandProtection Protection => null;
    }

    private readonly record struct WriterKey(Type T0, Type T1);
}