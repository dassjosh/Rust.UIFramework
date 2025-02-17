using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal static class ArgReaderCreator
{
    private static readonly Dictionary<Type, IArgReader> BuiltInReaders = new();
    private static readonly Dictionary<PluginReader, IArgReader> PluginReaders = new();
    
    public static IArgReader[] CreateReaders<T0>(PluginId pluginId) => [GetArgReader<T0>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2, T3>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId), GetArgReader<T3>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2, T3, T4>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId), GetArgReader<T3>(pluginId), GetArgReader<T4>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2, T3, T4, T5>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId), GetArgReader<T3>(pluginId), GetArgReader<T4>(pluginId), GetArgReader<T5>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId), GetArgReader<T3>(pluginId), GetArgReader<T4>(pluginId), GetArgReader<T5>(pluginId), GetArgReader<T6>(pluginId)];
    public static IArgReader[] CreateReaders<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId) => [GetArgReader<T0>(pluginId), GetArgReader<T1>(pluginId), GetArgReader<T2>(pluginId), GetArgReader<T3>(pluginId), GetArgReader<T4>(pluginId), GetArgReader<T5>(pluginId), GetArgReader<T6>(pluginId), GetArgReader<T7>(pluginId)];

    private static IArgReader GetArgReader<T>(PluginId pluginId)
    {
        Type type = typeof(T);
        PluginReader pluginReader = new(pluginId, type);
        if (PluginReaders.TryGetValue(pluginReader, out IArgReader reader))
        {
            return reader;
        }
        
        if (!BuiltInReaders.TryGetValue(type, out reader))
        {
            BuiltInReaders[type] = reader = CreateArgReader<T>(type);
        }

        return reader;
    }
    
    private static IArgReader CreateArgReader<T>(Type type)
    {
        if (type == typeof(byte)) return new ArgReader<byte>(span => byte.Parse(span));
        if (type == typeof(byte?)) return new ArgReader<byte?>(span => span is StringBuilderExt.Null ? null : byte.Parse(span));
        if (type == typeof(sbyte)) return new ArgReader<sbyte>(span => sbyte.Parse(span));
        if (type == typeof(sbyte?)) return new ArgReader<sbyte?>(span => span is StringBuilderExt.Null ? null : sbyte.Parse(span));
        if (type == typeof(short)) return new ArgReader<short>(span => short.Parse(span));
        if (type == typeof(short?)) return new ArgReader<short?>(span => span is StringBuilderExt.Null ? null : short.Parse(span));
        if (type == typeof(ushort)) return new ArgReader<ushort>(span => ushort.Parse(span));
        if (type == typeof(ushort?)) return new ArgReader<ushort?>(span => span is StringBuilderExt.Null ? null : ushort.Parse(span));
        if (type == typeof(int)) return new ArgReader<int>(span => int.Parse(span));
        if (type == typeof(int?)) return new ArgReader<int?>(span => span is StringBuilderExt.Null ? null : int.Parse(span));
        if (type == typeof(uint)) return new ArgReader<uint>(span => uint.Parse(span));
        if (type == typeof(uint?)) return new ArgReader<uint?>(span => span is StringBuilderExt.Null ? null : uint.Parse(span));
        if (type == typeof(long)) return new ArgReader<long>(span => long.Parse(span));
        if (type == typeof(long?)) return new ArgReader<long?>(span => span is StringBuilderExt.Null ? null : long.Parse(span));
        if (type == typeof(ulong)) return new ArgReader<ulong>(span => ulong.Parse(span));
        if (type == typeof(ulong?)) return new ArgReader<ulong?>(span => span is StringBuilderExt.Null ? null : ulong.Parse(span));
        if (type == typeof(float)) return new ArgReader<float>(span => float.Parse(span));
        if (type == typeof(float?)) return new ArgReader<float?>(span => span is StringBuilderExt.Null ? null : float.Parse(span));
        if (type == typeof(double)) return new ArgReader<double>(span => double.Parse(span));
        if (type == typeof(double?)) return new ArgReader<double?>(span => span is StringBuilderExt.Null ? null : double.Parse(span));
        if (type == typeof(decimal)) return new ArgReader<decimal>(span => decimal.Parse(span));
        if (type == typeof(decimal?)) return new ArgReader<decimal?>(span => span is StringBuilderExt.Null ? null : decimal.Parse(span));
        if (type == typeof(bool)) return new ArgReader<bool>(span => bool.Parse(span));
        if (type == typeof(bool?)) return new ArgReader<bool?>(span => span is StringBuilderExt.Null ? null : bool.Parse(span));
        if (type == typeof(DateTime)) return new ArgReader<DateTime>(span => DateTime.Parse(span));
        if (type == typeof(DateTime?)) return new ArgReader<DateTime?>(span => span is StringBuilderExt.Null ? null : DateTime.Parse(span));
        if (type == typeof(DateTimeOffset)) return new ArgReader<DateTimeOffset>(span => DateTimeOffset.Parse(span));
        if (type == typeof(DateTimeOffset?)) return new ArgReader<DateTimeOffset?>(span => span is StringBuilderExt.Null ? null : DateTimeOffset.Parse(span));
        if (type == typeof(TimeSpan)) return new ArgReader<TimeSpan>(span => TimeSpan.Parse(span));
        if (type == typeof(TimeSpan?)) return new ArgReader<TimeSpan?>(span => span is StringBuilderExt.Null ? null : TimeSpan.Parse(span));
        if(type == typeof(BasePlayer)) return new ArgReader<BasePlayer>(span =>
        {
            ulong playerId = ulong.Parse(span);
            BasePlayer player = BasePlayer.FindAwakeOrSleepingByID(playerId);
            return player ? player : BasePlayer.FindBot(playerId);
        });
        if(typeof(BaseNetworkable).IsAssignableFrom(type)) return new ArgReader<T>(span =>
        {
            BaseNetworkable networkable = BaseNetworkable.serverEntities.Find(new NetworkableId(ulong.Parse(span)));
            return networkable is T entity ? entity : default;
        });
        if(type.IsEnum) return new ArgReader<T>(span => Enum.TryParse(type, span.ToString(), out object result) && result is T @enum ? @enum : default); //TODO: Try to avoid string allocation

        throw new Exception($"No ArgReader found for type: {type}");  //TODO: better exception
    }

    internal static void RegisterPluginReader<T>(PluginId pluginId, IArgReader<T> reader) => PluginReaders[new PluginReader(pluginId, typeof(T))] = reader;
    internal static void RemovePluginReaders(PluginId pluginId) => PluginReaders.RemoveAll(r => r.Key.Id == pluginId);

    private record struct PluginReader(PluginId Id, Type Type);
}