using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class AccessModifierExt 
{
    public static T Public<T>(this T accessModifiers) where T : IAccessModifiers
    {
        accessModifiers.AccessModifiers |= AccessModifiers.Public;
        return accessModifiers;
    }
    
    public static T Private<T>(this T accessModifiers) where T : IAccessModifiers
    {
        accessModifiers.AccessModifiers |= AccessModifiers.Private;
        return accessModifiers;
    }
    
    public static T Protected<T>(this T accessModifiers) where T : IAccessModifiers
    {
        accessModifiers.AccessModifiers |= AccessModifiers.Protected;
        return accessModifiers;
    }
    
    public static T Internal<T>(this T accessModifiers) where T : IAccessModifiers
    {
        accessModifiers.AccessModifiers |= AccessModifiers.Internal;
        return accessModifiers;
    }
    
    public static string GetAccessModifiers(this IAccessModifiers accessModifiers)
    {
        StringBuilder sb = new();
        if (accessModifiers.AccessModifiers.HasFlag(AccessModifiers.Public))
        {
            sb.Append("public ");
        }
        else if (accessModifiers.AccessModifiers.HasFlag(AccessModifiers.Private))
        {
            sb.Append("private ");
        }
        else if (accessModifiers.AccessModifiers.HasFlag(AccessModifiers.Protected))
        {
            sb.Append("protected ");
        }
        else if (accessModifiers.AccessModifiers.HasFlag(AccessModifiers.Internal))
        {
            sb.Append("internal ");
        }
        return sb.ToString();
    }
}