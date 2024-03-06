using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Exceptions
{
    public class UiReferenceException : UiFrameworkException
    {
        private UiReferenceException(string message) : base(message) { }

        public static void ThrowIfInvalidParent(in UiReference reference)
        {
            if (!reference.IsValidParent()) throw new UiReferenceException($"{nameof(UiReference)} parent is not a valid parent reference value. Parent: {reference.Parent}");
        }
        
        public static void ThrowIfInvalidReference(in UiReference reference)
        {
            if (!reference.IsValidParent()) throw new UiReferenceException($"{nameof(UiReference)} parent is not a valid reference value. Parent: {reference.Parent} Name: {reference.Name}");
        }
    }
}