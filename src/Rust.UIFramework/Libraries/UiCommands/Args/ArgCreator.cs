using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Exceptions.UiCommands;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ArgCreator
{
    private static readonly Dictionary<Type, IArgHandler> BuiltInHandlers = new();
    private static readonly Dictionary<PluginArgHandler, IArgHandler> PluginHandlers = new();
    
    internal static IArgHandler[] CreateArgHandler<T0>(PluginId pluginId) => [GetArgHandler<T0>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId), GetArgHandler<T7>(pluginId)];

    private static IArgHandler GetArgHandler<T>(PluginId pluginId)
    {
        Type type = typeof(T);
        PluginArgHandler pluginReader = new(pluginId, type);
        if (PluginHandlers.TryGetValue(pluginReader, out IArgHandler reader))
        {
            return reader;
        }
        
        if (!BuiltInHandlers.TryGetValue(type, out reader))
        {
            BuiltInHandlers[type] = reader = CreateArgHandler<T>(type);
        }

        return reader;
    }
    
    private static IArgHandler CreateArgHandler<T>(Type type)
    {
        if (type == typeof(byte)) return new ArgHandler<byte>(span => byte.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(byte?)) return new ArgHandler<byte?>(span => span is StringBuilderExt.Null ? null : byte.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(sbyte)) return new ArgHandler<sbyte>(span => sbyte.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(sbyte?)) return new ArgHandler<sbyte?>(span => span is StringBuilderExt.Null ? null : sbyte.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(short)) return new ArgHandler<short>(span => short.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(short?)) return new ArgHandler<short?>(span => span is StringBuilderExt.Null ? null : short.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ushort)) return new ArgHandler<ushort>(span => ushort.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ushort?)) return new ArgHandler<ushort?>(span => span is StringBuilderExt.Null ? null : ushort.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(int)) return new ArgHandler<int>(span => int.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(int?)) return new ArgHandler<int?>(span => span is StringBuilderExt.Null ? null : int.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(uint)) return new ArgHandler<uint>(span => uint.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(uint?)) return new ArgHandler<uint?>(span => span is StringBuilderExt.Null ? null : uint.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(long)) return new ArgHandler<long>(span => long.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(long?)) return new ArgHandler<long?>(span => span is StringBuilderExt.Null ? null : long.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ulong)) return new ArgHandler<ulong>(span => ulong.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(ulong?)) return new ArgHandler<ulong?>(span => span is StringBuilderExt.Null ? null : ulong.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(float)) return new ArgHandler<float>(span => float.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(float?)) return new ArgHandler<float?>(span => span is StringBuilderExt.Null ? null : float.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(double)) return new ArgHandler<double>(span => double.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(double?)) return new ArgHandler<double?>(span => span is StringBuilderExt.Null ? null : double.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(decimal)) return new ArgHandler<decimal>(span => decimal.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(decimal?)) return new ArgHandler<decimal?>(span => span is StringBuilderExt.Null ? null : decimal.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(bool)) return new ArgHandler<bool>(span => bool.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(bool?)) return new ArgHandler<bool?>(span => span is StringBuilderExt.Null ? null : bool.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTime)) return new ArgHandler<DateTime>(span => DateTime.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTime?)) return new ArgHandler<DateTime?>(span => span is StringBuilderExt.Null ? null : DateTime.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTimeOffset)) return new ArgHandler<DateTimeOffset>(span => DateTimeOffset.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(DateTimeOffset?)) return new ArgHandler<DateTimeOffset?>(span => span is StringBuilderExt.Null ? null : DateTimeOffset.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(TimeSpan)) return new ArgHandler<TimeSpan>(span => TimeSpan.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(TimeSpan?)) return new ArgHandler<TimeSpan?>(span => span is StringBuilderExt.Null ? null : TimeSpan.Parse(span), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(NetworkableId)) return new ArgHandler<NetworkableId>(span => new NetworkableId(ulong.Parse(span)), (sb, arg) => sb.AppendSpan(arg.Value));
        if (type == typeof(NetworkableId?)) return new ArgHandler<NetworkableId?>(span => span is StringBuilderExt.Null ? null : new NetworkableId(ulong.Parse(span)), (sb, arg) => sb.AppendSpan(arg));
        if (type == typeof(string)) return new ArgHandler<string>(span => span.ToString(), (sb, arg) =>
        {
            sb.Append('"');
            sb.Append(arg);
            sb.Append('"');
        });
        if(type == typeof(BasePlayer)) return new ArgHandler<BasePlayer>(span =>
        {
            if(span.SequenceEqual(StringBuilderExt.Null)) return default;
            ulong playerId = ulong.Parse(span);
            BasePlayer player = BasePlayer.FindAwakeOrSleepingByID(playerId);
            return player ? player : BasePlayer.FindBot(playerId);
        }, (sb, arg) => sb.AppendSpan(arg?.UserIDString));
        if(typeof(BaseNetworkable).IsAssignableFrom(type)) return new ArgHandler<T>(span =>
        {
            if(span.SequenceEqual(StringBuilderExt.Null)) return default;
            BaseNetworkable networkable = BaseNetworkable.serverEntities.Find(new NetworkableId(ulong.Parse(span)));
            return networkable is T entity ? entity : default;
        }, (sb, arg) => sb.AppendSpan((arg as BaseNetworkable)?.net.ID.Value));
        if(type.IsEnum) return new ArgHandler<T>(span => Enum.TryParse(type, span.ToString(), out object result) && result is T @enum ? @enum : default, (sb, arg) => sb.Append(StringCache<T>.ToString(arg))); //TODO: Try to avoid string allocation

        throw new NoArgHandlerException(type);
    }
    
    internal static void RegisterPluginHandler<T>(PluginId pluginId, IArgHandler<T> handler) => PluginHandlers[new PluginArgHandler(pluginId, typeof(T))] = handler;
    internal static void RemovePluginHandler(PluginId pluginId) => PluginHandlers.RemoveAll(r => r.Key.Id == pluginId);
    
    private record struct PluginArgHandler(PluginId Id, Type Type);
}