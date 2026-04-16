using System.Collections.Generic;
using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ICoreComponent : IComponent
{
    T GetOrAddSubComponent<T>() where T : BaseComponent, ISubComponent, new();
    T AddSubComponent<T>(bool ignoreIfExists = false) where T : BaseComponent, ISubComponent, new();
    T GetSubComponent<T>() where T : ISubComponent;
    IEnumerable<T> GetSubComponents<T>() where T : ISubComponent;
    void RemoveSubComponents<T>() where T : ISubComponent;
    void RemoveSubComponent<T>() where T : ISubComponent;
    void RemoveSubComponent(ISubComponent subComponent);   
}