using System.Diagnostics.Contracts;

namespace Oxide.Ext.UiFramework.UiElements;

public readonly struct UiReference(string parent, string name)
{
    public readonly string Parent = parent;
    public readonly string Name = name;

    [Pure]
    public UiReference WithChild(string name) => new(Name, name);
    
    [Pure]
    public UiReference WithName(string name) => new(Parent, name);
    
    [Pure]
    public UiReference WithParent(string parent) => new(parent, Name);

    public bool IsValidParent() => !string.IsNullOrEmpty(Parent);
    public bool IsValidReference() => IsValidParent() && !string.IsNullOrEmpty(Name);
}