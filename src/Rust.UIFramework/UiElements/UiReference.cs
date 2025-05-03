namespace Oxide.Ext.UiFramework.UiElements;

public readonly struct UiReference(string parent, string name)
{
    public readonly string Parent = parent;
    public readonly string Name = name;

    public UiReference WithChild(string name) => new(Name, name);
    public UiReference WithName(string name) => new(Parent, name);

    public bool IsValidParent() => !string.IsNullOrEmpty(Parent);
    public bool IsValidReference() => IsValidParent() && !string.IsNullOrEmpty(Name);
}