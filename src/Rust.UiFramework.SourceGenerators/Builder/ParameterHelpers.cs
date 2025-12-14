using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class ParameterHelpers
{
    extension<T>(T modifier) where T : IParameterModifier
    {
        public T Ref()
        {
            modifier.Modifiers |= ParameterModifiers.Ref;
            return modifier;
        }

        public T Out()
        {
            modifier.Modifiers |= ParameterModifiers.Out;
            return modifier;
        }

        public T In()
        {
            modifier.Modifiers |= ParameterModifiers.In;
            return modifier;
        }

        public T Readonly()
        {
            modifier.Modifiers |= ParameterModifiers.Readonly;
            return modifier;
        }

        public T This()
        {
            modifier.Modifiers |= ParameterModifiers.This;
            return modifier;
        }
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