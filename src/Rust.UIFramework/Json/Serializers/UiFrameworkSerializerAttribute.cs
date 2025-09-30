using System;

namespace Oxide.Ext.UiFramework.Json;

[AttributeUsage(AttributeTargets.Class)]
public class UiFrameworkSerializerAttribute(Type serializerType) : Attribute
{
    public readonly Type SerializerType = serializerType;
}