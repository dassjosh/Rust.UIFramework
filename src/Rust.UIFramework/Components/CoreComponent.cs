using System.Collections.Generic;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class CoreComponent : ICoreComponent
{
    public bool Enabled = true;
    private List<SubComponent> _subComponents;

    protected CoreComponent()
    {
        Reset();
    }
        
    public virtual void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
    }

    internal void WriteSubComponents(JsonFrameworkWriter writer)
    {
        if (_subComponents == null) return;
        for (int index = 0; index < _subComponents.Count; index++)
        {
            ISubComponent component = _subComponents[index];
            component.WriteComponent(writer);
        }
    }

    public T AddSubComponent<T>() where T : SubComponent, new()
    {
        T subComponent = UiFrameworkPool.Get<T>();
        _subComponents ??= UiFrameworkPool.GetList<SubComponent>();
        if (_subComponents.Count != 0 && !subComponent.AllowMultiple && GetSubComponent<T>() != null)
        {
            throw new UiFrameworkException($"Multiple instances of subcomponent {typeof(T).Name} are not allowed.");
        }
        _subComponents.Add(subComponent);
        return subComponent;
    }

    public T GetSubComponent<T>() where T : SubComponent
    {
        if (_subComponents == null) return null;
        for (int index = 0; index < _subComponents.Count; index++)
        {
            SubComponent component = _subComponents[index];
            if (component is T t)
            {
                return t;
            }
        }

        return null;
    }

    public IEnumerable<T> GetSubComponents<T>() where T : SubComponent
    {
        if (_subComponents == null) yield break;
        foreach (SubComponent component in _subComponents)
        {
            if (component is T tComponent)
            {
                yield return tComponent;
            }
        }
    }

    public void RemoveComponents<T>() where T : SubComponent
    {
        _subComponents?.RemoveAll(sc => sc is T);
    }
    
    public void RemoveComponent<T>() where T : SubComponent
    {
        int index = _subComponents?.FindIndex(sc => sc is T) ?? -1;
        if (index != -1)
        {
            _subComponents!.RemoveAt(index);
        }
    }

    public void RemoveComponent(SubComponent subComponent)
    {
        _subComponents.Remove(subComponent);
    }

    public virtual void Reset()
    {
        if (_subComponents != null)
        {
            for (int index = 0; index < _subComponents.Count; index++)
            {
                _subComponents[index].Dispose();
            }

            UiFrameworkPool.FreeList(_subComponents);
            _subComponents = null;
        }
        Enabled = true;
    }
}