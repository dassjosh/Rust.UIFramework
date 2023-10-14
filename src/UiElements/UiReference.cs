namespace Oxide.Ext.UiFramework.UiElements
{
    public struct UiReference
    {
        public readonly string Parent;
        public readonly string Name;

        public UiReference(string parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public bool IsValidParent() => !string.IsNullOrEmpty(Parent);
        public bool IsValidReference() => IsValidParent() && !string.IsNullOrEmpty(Name);
    }
}