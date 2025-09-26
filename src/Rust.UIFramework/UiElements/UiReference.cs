using System.Diagnostics.Contracts;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.UiElements;

public readonly struct UiReference(string parent, string name)
{
    public readonly string Parent = parent;
    public readonly string Name = name;

    public UiReference(UiLayer parent, string name) : this(UiLayerCache.GetLayer(parent), name) { }
    public UiReference(UiReference parent, string name) : this(parent.Name, name) { }
    
    [Pure]
    public UiReference WithChild(string name) => new(Name, name);
    
    [Pure]
    public UiReference WithName(string name) => new(Parent, name);
    
    [Pure]
    public UiReference WithParent(string parent) => new(parent, Name);
    
    [Pure]
    public UiReference WithParent(UiLayer parent) => WithParent(UiLayerCache.GetLayer(parent));
    
    [Pure]
    public UiReference WithParent(in UiReference parent) => WithParent(parent.Name);

    public bool IsValidParent() => !string.IsNullOrEmpty(Parent);
    public bool IsValidName() => !string.IsNullOrEmpty(Name);
    public bool IsValidReference() => IsValidParent() && !string.IsNullOrEmpty(Name);
}