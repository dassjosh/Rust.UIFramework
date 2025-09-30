using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public interface IUiFrameworkSerializer<in T> : IUiFrameworkSerializer
{
    void Serialize(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode);
}

public interface IUiFrameworkSerializer
{
    void Serialize(JsonFrameworkWriter writer, object component, object defaults, SerializeMode mode);
}