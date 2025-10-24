using System.Collections.Generic;
using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ICoreComponent : IComponent
{
    T AddSubComponent<T>(bool ignoreIfExists = false) where T : SubComponent, new();
    T GetSubComponent<T>() where T : SubComponent;
    IEnumerable<T> GetSubComponents<T>() where T : SubComponent;
    void RemoveSubComponents<T>() where T : SubComponent;
    void RemoveSubComponent<T>() where T : SubComponent;
    void RemoveSubComponent(SubComponent subComponent);   
}