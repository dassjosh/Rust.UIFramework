using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public static class PartialCommand
{
    private static readonly Dictionary<WriterKey, IArgWriter[]> TypedWriters = new();
    
    public static IPartialCommand<T> Create<T>(string command) => new PartialCommand<T>(command, GetWriters<T>(), 0);
    public static IPartialCommand<T0, T1> Create<T0, T1>(string command) => new PartialCommand<T0, T1>(command, GetWriters<T0, T1>(), 0);
    
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

internal class PartialCommand<T0> : BaseCommandBuilder, IPartialCommand<T0>
{
    internal PartialCommand(string command, IArgWriter[] writers, int argIndex) : base(command, writers, argIndex)
    {
        
    }
    
    public string Build(T0 arg0)
    {
        ArgWriterIterator iterator = StartBuilding();
        iterator.WriteArgs(arg0);
        return iterator.ToString();
    }
}

internal class PartialCommand<T0, T1> : BaseCommandBuilder, IPartialCommand<T0, T1>
{
    internal PartialCommand(string command, IArgWriter[] writers, int argIndex) : base(command, writers, argIndex)
    {
        
    }
    
    public string Build(T0 arg0, T1 arg1)
    {
        ArgWriterIterator iterator = StartBuilding();
        iterator.WriteArgs(arg0, arg1);
        return iterator.ToString();
    }
}