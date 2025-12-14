using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class AccessModifierExt
{
    extension<T>(T accessModifiers) where T : IAccessModifiers
    {
        public T Public()
        {
            accessModifiers.AccessModifiers |= AccessModifiers.Public;
            return accessModifiers;
        }

        public T Private()
        {
            accessModifiers.AccessModifiers |= AccessModifiers.Private;
            return accessModifiers;
        }

        public T Protected()
        {
            accessModifiers.AccessModifiers |= AccessModifiers.Protected;
            return accessModifiers;
        }

        public T Internal()
        {
            accessModifiers.AccessModifiers |= AccessModifiers.Internal;
            return accessModifiers;
        }

        public void BuildAccessModifiers(StringBuilder sb)
        {
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
        }
    }
}