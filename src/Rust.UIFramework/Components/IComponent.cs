using System.Collections.Generic;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public interface IComponent
{
    void WriteComponent(JsonFrameworkWriter writer);
    void Reset();
}

public interface ICoreComponent : IComponent
{
    T AddSubComponent<T>(bool ignoreIfExists = false) where T : SubComponent, new();
    T GetSubComponent<T>() where T : SubComponent;
    IEnumerable<T> GetSubComponents<T>() where T : SubComponent;
    void RemoveSubComponents<T>() where T : SubComponent;
    void RemoveSubComponent<T>() where T : SubComponent;
    void RemoveSubComponent(SubComponent subComponent);   
}

public interface ISubComponent : IComponent
{
    bool AllowMultiple { get; }    
}

public interface IChildComponent : IComponent;