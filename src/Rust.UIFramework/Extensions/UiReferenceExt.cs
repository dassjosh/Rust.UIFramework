using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class UiReferenceExt
{
    public static UiReference WithName(in this UiReference element, string name) => new(element.Parent, name);
    public static UiReference WithParent(in this UiReference element, string parent) => new(parent, element.Name);
}