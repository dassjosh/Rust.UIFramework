using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class CoreComponent : BaseTypedComponent, ICoreComponent, ISubComponent
{
    internal readonly List<ISubComponent> SubComponents = [];
    public bool AllowMultiple => false;
    
    public T GetOrAddSubComponent<T>() where T : BaseComponent, ISubComponent, new() => GetSubComponent<T>() ?? AddSubComponentInternal<T>();

    public T AddSubComponent<T>(bool ignoreIfExists = false) where T : BaseComponent, ISubComponent, new()
    {
        if (SubComponents.Count != 0 && GetSubComponent<T>() is { } component)
        {
            if (ignoreIfExists)
            {
                return default;
            }

            if (!component.AllowMultiple)
            {
                throw new UiFrameworkException($"Multiple instances of subcomponent {typeof(T).Name} are not allowed.");
            }
        }
        
        return AddSubComponentInternal<T>();
    }

    private T AddSubComponentInternal<T>() where T : BaseComponent, ISubComponent, new()
    {
        T subComponent = PluginPool.Get<T>();
        if (subComponent is IGraphicalComponent && (this is IGraphicalComponent || SubComponents.OfType<IGraphicalComponent>().Any()))
        {
            throw new UiFrameworkException("Multiple instances of a graphical subcomponent are not allowed per UI element.");
        }
        
        SubComponents.Add(subComponent);
        return subComponent;
    }
    
    protected ISubComponent AddSubComponentInternal(ComponentType type)
    {
        ISubComponent subComponent = type.GetSubComponent(PluginPool);
        SubComponents.Add(subComponent);
        return subComponent;
    }

    public void AddSubComponent(ISubComponent component)
    {
        SubComponents.Add(component);
    }

    public T GetSubComponent<T>() where T : ISubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T component)
            {
                return component;
            }
        }

        return default;
    }
    
    public T GetSubComponent<T>(Predicate<T> predicate) where T : ISubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T component && predicate(component))
            {
                return component;
            }
        }

        return default;
    }

    internal ISubComponent GetSubComponentByType(ComponentType type)
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            ISubComponent subComponent = SubComponents[index];
            if (subComponent.ComponentType == type)
            {
                return subComponent;
            }
        }

        return null;
    }

    internal ISubComponent GetOrAddSubComponentByType(ComponentType type) => GetSubComponentByType(type) ?? AddSubComponentInternal(type);

    public IEnumerable<T> GetSubComponents<T>() where T : ISubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T tComponent)
            {
                yield return tComponent;
            }
        }
    }

    public void RemoveSubComponents<T>() where T : ISubComponent
    {
        for (int index = SubComponents.Count - 1; index >= 0; index--)
        {
            ISubComponent component = SubComponents[index];
            if (component is T)
            {
                component.TryDispose();
                SubComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponents<T>(Predicate<T> predicate) where T : ISubComponent
    {
        for (int index = SubComponents.Count - 1; index >= 0; index--)
        {
            ISubComponent subComponent = SubComponents[index];
            if (subComponent is T component && predicate(component))
            {
                subComponent.TryDispose();
                SubComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponent<T>() where T : ISubComponent
    {
        RemoveSubComponentAtIndex(SubComponents.FindIndex(sc => sc is T));
    }

    public void RemoveSubComponent<T>(Predicate<T> predicate) where T : ISubComponent
    {
        RemoveSubComponentAtIndex(SubComponents.FindIndex(sc => sc is T component && predicate(component)));
    }

    public void RemoveSubComponent(ISubComponent subComponent)
    {
        if (SubComponents.Remove(subComponent))
        {
            subComponent.TryDispose();
        }
    }

    public void RemoveSubComponent(ComponentType type)
    {
        RemoveSubComponentAtIndex(SubComponents.FindIndex(sc => sc.ComponentType == type));
    }
    
    private void RemoveSubComponentAtIndex(int index)
    {
        if (index > -1)
        {
            SubComponents[index].TryDispose();
            SubComponents.RemoveAt(index);
        }
    }

    public void WriteSubComponents(JsonFrameworkWriter writer, SerializeMode mode)
    {
        switch (mode)
        {
            case SerializeMode.Create:
            {
                for (int i = 0; i < SubComponents.Count; i++)
                {
                    SubComponents[i].WriteComponent(writer, mode);
                }

                break;
            }
            case SerializeMode.Update:
            {
                for (int i = 0; i < SubComponents.Count; i++)
                {
                    ISubComponent component = SubComponents[i];
                    if (component.HasChanged())
                    {
                        component.WriteComponent(writer, mode);
                    }
                }

                break;
            }
        }
    }
    
    public override bool HasChanged()
    {
        if (base.HasChanged())
        {
            return true;
        }
        
        for (int i = 0; i < SubComponents.Count; i++)
        {
            if (SubComponents[i].HasChanged())
            {
                return true;
            }
        }

        return false;
    }

    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        for (int i = 0; i < SubComponents.Count; i++)
        {
            SubComponents[i].ResetHasChanged();
        }
    }

    public override void Reset()
    {
        base.Reset();
        SubComponents.FreeValues();
    }
}