using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal static class ArgWriterCreator
{
    private static readonly Dictionary<Type, IArgWriter> ArgReaders = new();
    private static readonly Dictionary<PluginWriter, IArgWriter> PluginWriters = new();
    
    public static IArgWriter[] CreateWriters<T0>(PluginId pluginId) => [GetArgWriter<T0>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2, T3>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId), GetArgWriter<T3>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2, T3, T4>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId), GetArgWriter<T3>(pluginId), GetArgWriter<T4>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2, T3, T4, T5>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId), GetArgWriter<T3>(pluginId), GetArgWriter<T4>(pluginId), GetArgWriter<T5>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId), GetArgWriter<T3>(pluginId), GetArgWriter<T4>(pluginId), GetArgWriter<T5>(pluginId), GetArgWriter<T6>(pluginId)];
    public static IArgWriter[] CreateWriters<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId) => [GetArgWriter<T0>(pluginId), GetArgWriter<T1>(pluginId), GetArgWriter<T2>(pluginId), GetArgWriter<T3>(pluginId), GetArgWriter<T4>(pluginId), GetArgWriter<T5>(pluginId), GetArgWriter<T6>(pluginId), GetArgWriter<T7>(pluginId)];
    
    private static IArgWriter GetArgWriter<T>(PluginId pluginId)
    {
        Type type = typeof(T);
        PluginWriter pluginReader = new(pluginId, type);
        if (PluginWriters.TryGetValue(pluginReader, out IArgWriter writer))
        {
            return writer;
        }
        
        if (!ArgReaders.TryGetValue(type, out writer))
        {
            ArgReaders[type] = writer = CreateArgWriter<T>(type);
        }

        return writer;
    }
    
    private static IArgWriter CreateArgWriter<T>(Type type)
    {
        if (type == typeof(byte)) return new ArgWriter<byte>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(sbyte)) return new ArgWriter<sbyte>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(short)) return new ArgWriter<short>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ushort)) return new ArgWriter<ushort>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(int)) return new ArgWriter<int>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(uint)) return new ArgWriter<uint>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(long)) return new ArgWriter<long>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ulong)) return new ArgWriter<ulong>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(float)) return new ArgWriter<float>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(double)) return new ArgWriter<double>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(decimal)) return new ArgWriter<decimal>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(bool)) return new ArgWriter<bool>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(byte?)) return new ArgWriter<byte?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(sbyte?)) return new ArgWriter<sbyte?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(short?)) return new ArgWriter<short?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ushort?)) return new ArgWriter<ushort?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(int?)) return new ArgWriter<int?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(uint?)) return new ArgWriter<uint?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(long?)) return new ArgWriter<long?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ulong?)) return new ArgWriter<ulong?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(float?)) return new ArgWriter<float?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(double?)) return new ArgWriter<double?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(decimal?)) return new ArgWriter<decimal?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(bool?)) return new ArgWriter<bool?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTime)) return new ArgWriter<DateTime>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTime?)) return new ArgWriter<DateTime?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTimeOffset)) return new ArgWriter<DateTimeOffset>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTimeOffset?)) return new ArgWriter<DateTimeOffset?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(TimeSpan)) return new ArgWriter<TimeSpan>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(TimeSpan?)) return new ArgWriter<TimeSpan?>((sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(string)) return new ArgWriter<string>((sb, arg) =>
        {
            sb.Append('"');
            sb.Append(arg);
            sb.Append('"');
        }); 
        if(type == typeof(BasePlayer)) return new ArgWriter<BasePlayer>((sb, arg) => sb.AppendSpan(arg.UserIDString));
        if(typeof(BaseNetworkable).IsAssignableFrom(type)) return new ArgWriter<BaseNetworkable>((sb, arg) => sb.AppendSpan(arg.net.ID.Value));
        if (type.IsEnum) return new ArgWriter<T>((sb, arg) => sb.Append(StringCache<T>.ToString(arg)));
        throw new Exception($"No ArgWriter found for type: {type}"); //TODO: better exception
    }
    
    internal static void RegisterPluginWriter<T>(PluginId pluginId, IArgWriter<T> reader) => PluginWriters[new PluginWriter(pluginId, typeof(T))] = reader;
    internal static void RemovePluginWriters(PluginId pluginId) => PluginWriters.RemoveAll(r => r.Key.Id == pluginId);

    private record struct PluginWriter(PluginId Id, Type Type);
}