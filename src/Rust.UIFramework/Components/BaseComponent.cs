using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : BasePoolable
{
    public abstract ComponentType ComponentType { get; }
    protected BaseComponent() => EnterPool();
    public abstract void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode);
    public abstract void ResetHasChanged();
    protected sealed override void EnterPool() => Reset();
    public abstract void Reset();
}