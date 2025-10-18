using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class ParameterHelpers
{
    public static T Ref<T>(this T modifier) where T : IParameterModifier
    {
        modifier.Modifiers |= ParameterModifiers.Ref;
        return modifier;
    }
    
    public static T Out<T>(this T modifier) where T : IParameterModifier
    {
        modifier.Modifiers |= ParameterModifiers.Out;
        return modifier;
    }
    
    public static T In<T>(this T modifier) where T : IParameterModifier
    {
        modifier.Modifiers |= ParameterModifiers.In;
        return modifier;
    }
    
    public static T Readonly<T>(this T modifier) where T : IParameterModifier
    {
        modifier.Modifiers |= ParameterModifiers.Readonly;
        return modifier;
    }
    
    public static T This<T>(this T modifier) where T : IParameterModifier
    {
        modifier.Modifiers |= ParameterModifiers.This;
        return modifier;
    }
    
    public static string GetModifiers(this IParameterModifier modifier)
    {
        StringBuilder sb = new();
        if (modifier.Modifiers.HasFlag(ParameterModifiers.Ref))
        {
            sb.Append("ref ");
        }
        if (modifier.Modifiers.HasFlag(ParameterModifiers.Out))
        {
            sb.Append("out ");
        }
        if (modifier.Modifiers.HasFlag(ParameterModifiers.In))
        {
            sb.Append("in ");
        }
        if (modifier.Modifiers.HasFlag(ParameterModifiers.Readonly))
        {
            sb.Append("readonly ");
        }
        if (modifier.Modifiers.HasFlag(ParameterModifiers.This))
        {
            sb.Append("this ");
        }
        return sb.ToString();
    }
}