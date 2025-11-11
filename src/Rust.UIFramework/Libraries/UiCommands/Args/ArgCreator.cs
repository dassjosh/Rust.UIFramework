using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Libraries;

internal static class ArgCreator
{
    private static readonly Dictionary<Type, IArgHandler> BuiltInHandlers = new();
    private static readonly Dictionary<PluginArgHandler, IArgHandler> PluginHandlers = new();
    
    internal static IArgHandler[] CreateArgHandler(PluginId pluginId) => [];
    internal static IArgHandler[] CreateArgHandler<T0>(PluginId pluginId) => [GetArgHandler<T0>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId), GetArgHandler<T7>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId), GetArgHandler<T7>(pluginId), GetArgHandler<T8>(pluginId)];
    internal static IArgHandler[] CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(PluginId pluginId) => [GetArgHandler<T0>(pluginId), GetArgHandler<T1>(pluginId), GetArgHandler<T2>(pluginId), GetArgHandler<T3>(pluginId), GetArgHandler<T4>(pluginId), GetArgHandler<T5>(pluginId), GetArgHandler<T6>(pluginId), GetArgHandler<T7>(pluginId), GetArgHandler<T8>(pluginId), GetArgHandler<T9>(pluginId)];

    private static IArgHandler GetArgHandler<T>(PluginId pluginId)
    {
        Type type = typeof(T);
        PluginArgHandler pluginReader = new(pluginId, type);
        if (PluginHandlers.TryGetValue(pluginReader, out IArgHandler handler) || BuiltInHandlers.TryGetValue(type, out handler))
        {
            return handler;
        }

        handler = CreateArgHandler<T>(type);
        if (handler != null)
        {
            BuiltInHandlers[type] = handler;
            return handler;
        }
        
        handler = CreatePluginArgHandler<T>(pluginId, type);
        if (handler != null)
        {
            PluginHandlers[pluginReader] = handler;
            return handler;
        }
        
        throw new NoArgHandlerException(type);
    }
    
    private static IArgHandler CreateArgHandler<T>(Type type)
    {
        if (type == typeof(char)) return new ArgHandler<char>(span => span[0], (writer, arg) => writer.Append(arg));
        if (type == typeof(char?)) return new ArgHandler<char?>(span => span is UiCommands.NullArg ? null : span[0], (writer, arg) => writer.Append(arg));
        if (type == typeof(string)) return new ArgHandler<string>(span => span.ToString(), (writer, arg) =>
        {
            writer.AppendStartQuote();
            writer.Append(arg);
            writer.AppendEndQuote();
        });
        if (type == typeof(byte)) return new ArgHandler<byte>(span => byte.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(byte?)) return new ArgHandler<byte?>(span => span is UiCommands.NullArg ? null : byte.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(sbyte)) return new ArgHandler<sbyte>(span => sbyte.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(sbyte?)) return new ArgHandler<sbyte?>(span => span is UiCommands.NullArg ? null : sbyte.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(short)) return new ArgHandler<short>(span => short.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(short?)) return new ArgHandler<short?>(span => span is UiCommands.NullArg ? null : short.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(ushort)) return new ArgHandler<ushort>(span => ushort.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(ushort?)) return new ArgHandler<ushort?>(span => span is UiCommands.NullArg ? null : ushort.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(int)) return new ArgHandler<int>(span => int.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(int?)) return new ArgHandler<int?>(span => span is UiCommands.NullArg ? null : int.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(uint)) return new ArgHandler<uint>(span => uint.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(uint?)) return new ArgHandler<uint?>(span => span is UiCommands.NullArg ? null : uint.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(long)) return new ArgHandler<long>(span => long.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(long?)) return new ArgHandler<long?>(span => span is UiCommands.NullArg ? null : long.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(ulong)) return new ArgHandler<ulong>(span => ulong.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(ulong?)) return new ArgHandler<ulong?>(span => span is UiCommands.NullArg ? null : ulong.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(float)) return new ArgHandler<float>(span => float.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(float?)) return new ArgHandler<float?>(span => span is UiCommands.NullArg ? null : float.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(double)) return new ArgHandler<double>(span => double.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(double?)) return new ArgHandler<double?>(span => span is UiCommands.NullArg ? null : double.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(decimal)) return new ArgHandler<decimal>(span => decimal.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(decimal?)) return new ArgHandler<decimal?>(span => span is UiCommands.NullArg ? null : decimal.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(bool)) return new ArgHandler<bool>(span => span is "1", (writer, arg) => writer.Append(arg));
        if (type == typeof(bool?)) return new ArgHandler<bool?>(span => span is UiCommands.NullArg ? null : bool.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(DateTime)) return new ArgHandler<DateTime>(span => DateTime.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(DateTime?)) return new ArgHandler<DateTime?>(span => span is UiCommands.NullArg ? null : DateTime.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(DateTimeOffset)) return new ArgHandler<DateTimeOffset>(span => DateTimeOffset.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(DateTimeOffset?)) return new ArgHandler<DateTimeOffset?>(span => span is UiCommands.NullArg ? null : DateTimeOffset.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(TimeSpan)) return new ArgHandler<TimeSpan>(span => TimeSpan.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(TimeSpan?)) return new ArgHandler<TimeSpan?>(span => span is UiCommands.NullArg ? null : TimeSpan.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(NetworkableId)) return new ArgHandler<NetworkableId>(span => new NetworkableId(ulong.Parse(span)), (writer, arg) => writer.Append(arg));
        if (type == typeof(NetworkableId?)) return new ArgHandler<NetworkableId?>(span => span is UiCommands.NullArg ? null : new NetworkableId(ulong.Parse(span)), (writer, arg) => writer.Append(arg));
        if (type == typeof(UiReference) || type == typeof(UiReference?)) return Singleton<UiReferenceHandler>.Instance;
        if (type == typeof(Vector2) || type == typeof(Vector2?) || type == typeof(Vector3) || type == typeof(Vector3?) || type == typeof(Vector4) || type == typeof(Vector4?)) return Singleton<UnityArgHandler>.Instance;
        if (type == typeof(UiColor)) return new ArgHandler<UiColor>(UiColor.ParseHexColor, (writer, arg) => writer.Append(arg));
        if (type == typeof(UiColor?)) return new ArgHandler<UiColor?>(span => span is UiCommands.NullArg ? null : UiColor.ParseHexColor(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(InputArg)) return new InputArgHandler();
        if (type == typeof(UiRotation)) return new ArgHandler<UiRotation>(UiRotation.Parse, (writer, arg) => writer.Append(arg));
        if (type == typeof(UiRotation?)) return new ArgHandler<UiRotation?>(span => span is UiCommands.NullArg ? null : UiRotation.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(UiPadding)) return new ArgHandler<UiPadding>(UiPadding.Parse, (writer, arg) => writer.Append(arg));
        if (type == typeof(UiPadding?)) return new ArgHandler<UiPadding?>(span => span is UiCommands.NullArg ? null : UiPadding.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(UiScale)) return new ArgHandler<UiScale>(UiScale.Parse, (writer, arg) => writer.Append(arg));
        if (type == typeof(UiScale?)) return new ArgHandler<UiScale?>(span => span is UiCommands.NullArg ? null : UiScale.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(UiBorderWidth)) return new ArgHandler<UiBorderWidth>(UiBorderWidth.Parse, (writer, arg) => writer.Append(arg));
        if (type == typeof(UiBorderWidth?)) return new ArgHandler<UiBorderWidth?>(span => span is UiCommands.NullArg ? null : UiBorderWidth.Parse(span), (writer, arg) => writer.Append(arg));
        if (type == typeof(BasePlayer)) return Singleton<BasePlayerHandler>.Instance;
        if (typeof(BaseNetworkable).IsAssignableFrom(type)) return new BaseNetworkableHandler<T>();
        if (type.IsEnum) return new EnumHandler<T>();
        
        return null;
    }

    private static IArgHandler CreatePluginArgHandler<T>(PluginId pluginId, Type type)
    {
        if(typeof(INamedStore).IsAssignableFrom(type)) return new ArgHandler<T>(span => (T)Singleton<UiNameStore>.Instance.GetOrCreateStore(pluginId, span.ToString()) , (writer, arg) => writer.Append(((INamedStore)arg).Name));
        if(typeof(IPlayerStore).IsAssignableFrom(type)) return new ArgHandler<T>(span => (T)Singleton<UiPlayerStore>.Instance.GetOrCreateStore(pluginId, ulong.Parse(span)) , (writer, arg) => writer.Append(((IPlayerStore)arg).PlayerId));
        return null;
    }
    
    internal static void RegisterPluginHandler<T>(PluginId pluginId, IArgHandler<T> handler) => PluginHandlers[new PluginArgHandler(pluginId, typeof(T))] = handler;
    internal static void RemovePluginHandler(PluginId pluginId)
    {
        PluginHandlers.RemoveAll(r => r.Key.Id == pluginId);
    }

    private record struct PluginArgHandler(PluginId Id, Type Type);
}