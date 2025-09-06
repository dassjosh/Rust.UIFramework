using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class CoreComponent : BaseTypedComponent, ICoreComponent
{
    private List<SubComponent> _subComponents;

    internal void WriteSubComponents(JsonFrameworkWriter writer)
    {
        if (_subComponents == null || _subComponents.Count == 0) return;
        int count = _subComponents.Count;
        ReadOnlySpan<SubComponent> span = _subComponents.ListAsReadOnlySpan();
        for (int index = 0; index < count; index++)
        {
            ISubComponent component = span[index];
            component.WriteComponent(writer);
        }
    }

    public T AddSubComponent<T>(bool ignoreIfExists = false) where T : SubComponent, new()
    {
        _subComponents ??= PluginPool.GetList<SubComponent>();
        if (_subComponents.Count != 0 && GetSubComponent<T>() is { } component)
        {
            if (ignoreIfExists)
            {
                return null;
            }

            if (!component.AllowMultiple)
            {
                throw new UiFrameworkException($"Multiple instances of subcomponent {typeof(T).Name} are not allowed.");
            }
        }
        
        T subComponent = PluginPool.Get<T>();
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
        if (_subComponents == null) return;
        int index = _subComponents.FindIndex(sc => sc is T);
        if (index != -1)
        {
            _subComponents!.RemoveAt(index);
        }
    }

    public void RemoveComponent(SubComponent subComponent)
    {
        _subComponents.Remove(subComponent);
    }

    public override void Reset()
    {
        base.Reset();
        if(_subComponents != null)
        {
            _subComponents.FreeValues();
            PluginPool.FreeList(_subComponents);
            _subComponents = null;
        }
    }
}