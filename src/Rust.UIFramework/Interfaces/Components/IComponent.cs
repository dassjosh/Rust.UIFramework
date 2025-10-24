using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IComponent : IPoolable
{
    ComponentType ComponentType { get; }
    
    void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode);
    bool HasChanged();
    void ResetHasChanged();
    void Reset();
}