using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Json;

public static class UiFrameworkSerializer
{
    private static readonly ConcurrentDictionary<Type, IUiFrameworkSerializer> Serializers = new();
    
    public static void Serialize(JsonFrameworkWriter writer, object serializable, object defaults = null, SerializeMode mode = SerializeMode.Create)
    {
        IUiFrameworkSerializer serialize = GetSerializer(serializable);
        serialize.Serialize(writer, serializable, defaults, mode);
    }

    // public static void Serialize<T>(JsonFrameworkWriter writer, T serializable, T defaults = null, SerializeMode mode = SerializeMode.Create) where T : class
    // {
    //     IUiFrameworkSerializer serializer = GetSerializer(serializable);
    //     IUiFrameworkSerializer<T> serialize = (IUiFrameworkSerializer<T>)serializer;
    //     serialize.Serialize(writer, serializable, defaults, mode);
    // }
    
    private static IUiFrameworkSerializer GetSerializer(object serializable)
    {
        IUiFrameworkSerializer serialize = Serializers.GetOrAdd(serializable.GetType(), type =>
        {
            UiFrameworkSerializerAttribute serializerType = type.GetAttribute<UiFrameworkSerializerAttribute>(true) ?? throw new InvalidOperationException($"Type {type} does not have a {nameof(UiFrameworkSerializerAttribute)} attribute.");
            return (IUiFrameworkSerializer)Activator.CreateInstance(serializerType.SerializerType);
        });
        return serialize;
    }
}