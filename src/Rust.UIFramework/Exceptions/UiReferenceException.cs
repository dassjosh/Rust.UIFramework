using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Exceptions;

public class UiReferenceException : UiFrameworkException
{
    private UiReferenceException(string message) : base(message) { }

    public static void ThrowIfInvalidParent(in UiReference reference)
    {
        if (!reference.IsValidParent()) throw new UiReferenceException($"{nameof(UiReference)} parent is not a valid parent reference value. Parent: {reference.Parent}");
    }
        
    public static void ThrowIfInvalidReference(in UiReference reference)
    {
        if (!reference.IsValidReference()) throw new UiReferenceException($"{nameof(UiReference)} is not a valid reference value. Parent: {reference.Parent} Name: {reference.Name}");
    }

    public static void ThrowIfInValidRootName(string rootName)
    {
        if (string.IsNullOrEmpty(rootName)) throw new UiReferenceException($"Trying to use {nameof(NamingMode)}.{nameof(NamingMode.Child)} without setting a RootName on the UiBuilder. Please call builder.SetName(\"name of the UI\") before adding this component.");
    }
}