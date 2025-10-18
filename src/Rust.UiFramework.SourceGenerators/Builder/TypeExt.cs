namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class TypeExt
{
    public static T Class<T>(this T type) where T : IType
    {
        type.Type = Type.Class;
        return type;
    }
    
    public static T Struct<T>(this T type) where T : IType
    {
        type.Type = Type.Struct;
        return type;
    }
    
    public static T Interface<T>(this T type) where T : IType
    {
        type.Type = Type.Interface;
        return type;
    }
    
    public static T Enum<T>(this T type) where T : IType
    {
        type.Type = Type.Enum;
        return type;
    }
    
    public static T Delegate<T>(this T type) where T : IType
    {
        type.Type = Type.Delegate;
        return type;
    }
    
    public static string GetDeclaredType(this IType type)
    {
        switch (type.Type)
        {
            case Type.Class:
                return "class";
            case Type.Struct:
                return "struct";
            case Type.Interface:
                return "interface";
            case Type.Enum:
                return "enum";
            case Type.Delegate:
                return "delegate";
            default:
                return "";
        }
    }
}