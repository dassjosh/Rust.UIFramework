using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class CoreComponent : BaseTypedComponent, ICoreComponent
{
    internal readonly List<SubComponent> SubComponents = [];
    
    public T GetOrAddSubComponent<T>() where T : SubComponent, new() => GetSubComponent<T>() ?? AddSubComponentInternal<T>();

    public T AddSubComponent<T>(bool ignoreIfExists = false) where T : SubComponent, new()
    {
        if (SubComponents.Count != 0 && GetSubComponent<T>() is { } component)
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
        SubComponents.Add(subComponent);
        return subComponent;
    }
    
    protected SubComponent AddSubComponentInternal(ComponentType type)
    {
        SubComponent subComponent;
        switch (type)
        {
            case ComponentType.RectTransform:
                subComponent = PluginPool.Get<RectTransformComponent>();
                break;
            case ComponentType.NeedsKeyboard:
                subComponent = PluginPool.Get<NeedsKeyboardComponent>();
                break;
            case ComponentType.NeedsMouse:
                subComponent = PluginPool.Get<NeedsMouseComponent>();
                break;
            case ComponentType.Outline:
                subComponent = PluginPool.Get<OutlineComponent>();
                break;
            case ComponentType.Countdown:
                subComponent = PluginPool.Get<CountdownComponent>();
                break;
            case ComponentType.Draggable:
                subComponent = PluginPool.Get<DraggableComponent>();
                break;
            case ComponentType.Slot:
                subComponent = PluginPool.Get<SlotComponent>();
                break;
            case ComponentType.HorizontalLayout:
                subComponent = PluginPool.Get<HorizontalLayoutComponent>();
                break;
            case ComponentType.VerticalLayout:
                subComponent = PluginPool.Get<VerticalLayoutComponent>();
                break;
            case ComponentType.GridLayout:
                subComponent = PluginPool.Get<GridLayoutComponent>();
                break;
            case ComponentType.ContentSizeFitter:
                subComponent = PluginPool.Get<ContentSizeFitterComponent>();
                break;
            case ComponentType.LayoutElement:
                subComponent = PluginPool.Get<LayoutElementComponent>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        SubComponents.Add(subComponent);
        return subComponent;
    }

    public T GetSubComponent<T>() where T : SubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T component)
            {
                return component;
            }
        }

        return null;
    }
    
    public T GetSubComponent<T>(Predicate<T> predicate) where T : SubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T component && predicate(component))
            {
                return component;
            }
        }

        return null;
    }

    internal SubComponent GetSubComponentByType(ComponentType type)
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            SubComponent subComponent = SubComponents[index];
            if (subComponent.ComponentType == type)
            {
                return subComponent;
            }
        }

        return null;
    }

    internal SubComponent GetOrAddSubComponentByType(ComponentType type) => GetSubComponentByType(type) ?? AddSubComponentInternal(type);

    public IEnumerable<T> GetSubComponents<T>() where T : SubComponent
    {
        for (int index = 0; index < SubComponents.Count; index++)
        {
            if (SubComponents[index] is T tComponent)
            {
                yield return tComponent;
            }
        }
    }

    public void RemoveSubComponents<T>() where T : SubComponent
    {
        for (int index = SubComponents.Count - 1; index >= 0; index--)
        {
            SubComponent component = SubComponents[index];
            if (component is T)
            {
                component.TryDispose();
                SubComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponents<T>(Predicate<T> predicate) where T : SubComponent
    {
        for (int index = SubComponents.Count - 1; index >= 0; index--)
        {
            SubComponent subComponent = SubComponents[index];
            if (subComponent is T component && predicate(component))
            {
                subComponent.TryDispose();
                SubComponents.RemoveAt(index);
            }
        }
    }
    
    public void RemoveSubComponent<T>() where T : SubComponent
    {
        int index = SubComponents.FindIndex(sc => sc is T);
        if (index != -1)
        {
            SubComponents[index].TryDispose();
            SubComponents.RemoveAt(index);
        }
    }
    
    public void RemoveSubComponent<T>(Predicate<T> predicate) where T : SubComponent
    {
        int index = SubComponents.FindIndex(sc => sc is T component && predicate(component));
        if (index != -1)
        {
            SubComponents[index].TryDispose();
            SubComponents.RemoveAt(index);
        }
    }

    public void RemoveSubComponent(SubComponent subComponent)
    {
        if (SubComponents.Remove(subComponent))
        {
            subComponent.TryDispose();
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
                    SubComponent component = SubComponents[i];
                    if (component.HasChanged())
                    {
                        component.WriteComponent(writer, mode);
                    }
                }

                break;
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        SubComponents.FreeValues();
    } 
}