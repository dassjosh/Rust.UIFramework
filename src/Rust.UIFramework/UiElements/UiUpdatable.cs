using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiUpdatable<T> : UiUpdatable where T : BaseUiComponent, new()
{
#if SERVER
    private T _previous;
#else
    internal T _previous;
#endif
    
    public T Current { get; private set; }

    public static UiUpdatable<T> Create(UpdatableBuilder builder, T element)
    {
        UiUpdatable<T> updatable = builder.PluginPool.Get<UiUpdatable<T>>().Init(element);
        builder.AddUpdatable(updatable);
        return updatable;
    }

    private UiUpdatable<T> Init(T source)
    {
        _previous = PluginPool.Get<T>();
        _previous.CopyFrom(source);
        _previous.Update = UpdateMode.Update;
        Current = PluginPool.Get<T>();
        Current.CopyFrom(source);
        Current.Update = UpdateMode.Update;
        return this;
    }
        
    public override void Serialize(JsonFrameworkWriter writer)
    {
        if (!Current.AreEquivalent(_previous))
        {
            UiFrameworkSerializer.Serialize(writer, Current, _previous, SerializeMode.Update);
        }
    }

    public override void Swap()
    {
        (_previous, Current) = (Current, _previous);
        Current.CopyFrom(_previous);
    }

    protected override void LeavePool()
    {
        _previous = PluginPool.Get<T>();
        Current = PluginPool.Get<T>();
    }

    protected override void EnterPool()
    {
        _previous.TryDispose();
        _previous = null;
        Current.TryDispose();
        Current = null;
    }
}

public abstract class UiUpdatable : BasePoolable
{
    public abstract void Serialize(JsonFrameworkWriter writer);
    public abstract void Swap();
}