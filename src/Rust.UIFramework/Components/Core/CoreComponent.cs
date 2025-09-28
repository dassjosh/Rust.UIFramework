using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class CoreComponent : BaseTypedComponent, ICoreComponent
{
    private readonly List<SubComponent> _subComponents = [];

    internal void WriteSubComponents(JsonFrameworkWriter writer)
    {
        if (_subComponents.Count == 0) return;
        
        int count = _subComponents.Count;
        SubComponent[] array = _subComponents.GetInternalArray();
        for (int index = 0; index < count; index++)
        {
            array[index].WriteComponent(writer);
        }
    }

    public T GetOrAddSubComponent<T>() where T : SubComponent, new() => GetSubComponent<T>() ?? AddSubComponentInternal<T>();

    public T AddSubComponent<T>(bool ignoreIfExists = false) where T : SubComponent, new()
    {
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
        
        return AddSubComponentInternal<T>();
    }

    private T AddSubComponentInternal<T>() where T : SubComponent, new()
    {
        T subComponent = PluginPool.Get<T>();
        _subComponents.Add(subComponent);
        return subComponent;
    }

    public T GetSubComponent<T>() where T : SubComponent
    {
        for (int index = 0; index < _subComponents.Count; index++)
        {
            if (_subComponents[index] is T t)
            {
                return t;
            }
        }

        return null;
    }

    public IEnumerable<T> GetSubComponents<T>() where T : SubComponent
    {
        for (int index = 0; index < _subComponents.Count; index++)
        {
            if (_subComponents[index] is T tComponent)
            {
                yield return tComponent;
            }
        }
    }

    public void RemoveSubComponents<T>() where T : SubComponent
    {
        for (int index = _subComponents.Count - 1; index >= 0; index--)
        {
            SubComponent component = _subComponents[index];
            if (component is T)
            {
                component.TryDispose();
                _subComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponents<T>(Predicate<T> predicate) where T : SubComponent
    {
        for (int index = _subComponents.Count - 1; index >= 0; index--)
        {
            SubComponent subComponent = _subComponents[index];
            if (subComponent is T component && predicate(component))
            {
                subComponent.TryDispose();
                _subComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponent<T>() where T : SubComponent
    {
        int index = _subComponents.FindIndex(sc => sc is T);
        if (index != -1)
        {
            _subComponents[index].TryDispose();
            _subComponents.RemoveAt(index);
        }
    }
    
    public void RemoveSubComponent<T>(Predicate<T> predicate) where T : SubComponent
    {
        int index = _subComponents.FindIndex(sc => sc is T component && predicate(component));
        if (index != -1)
        {
            _subComponents[index].TryDispose();
            _subComponents.RemoveAt(index);
        }
    }

    public void RemoveSubComponent(SubComponent subComponent)
    {
        if (_subComponents.Remove(subComponent))
        {
            subComponent.TryDispose();
        }
    }

    public override void Reset()
    {
        base.Reset();
        _subComponents.FreeValues();
    }
}