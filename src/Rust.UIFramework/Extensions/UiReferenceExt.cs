using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Extensions
{
    //Define:ExtensionMethods
    public static class UiReferenceExt
    {
        public static UiReference WithName(this UiReference element, string name) => new UiReference(element.Parent, name);
        public static UiReference WithParent(this UiReference element, string parent) => new UiReference(parent, element.Name);
    }
}