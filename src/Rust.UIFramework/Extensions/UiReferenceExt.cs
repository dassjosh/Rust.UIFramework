using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions;

public static class UiReferenceExt
{
    extension(in UiReference element)
    {
        public UiReference WithName(string name) => new(element.Parent, name);
        public UiReference WithParent(string parent) => new(parent, element.Name);
    }
}