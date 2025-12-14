using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class TypeExt
{
    extension<T>(T type) where T : IType
    {
        public T Class()
        {
            type.Type = Type.Class;
            return type;
        }

        public T Struct()
        {
            type.Type = Type.Struct;
            return type;
        }

        public T Interface()
        {
            type.Type = Type.Interface;
            return type;
        }

        public T Enum()
        {
            type.Type = Type.Enum;
            return type;
        }

        public T Delegate()
        {
            type.Type = Type.Delegate;
            return type;
        }

        public T Extension()
        {
            type.Type = Type.Extension;
            return type;
        }

        public bool IsType(Type matchType) => type.Type == matchType;
        
        public void BuildDeclaredType(StringBuilder sb)
        {
            switch (type.Type)
            {
                case Type.Class:
                    sb.Append("class");
                    break;
                case Type.Struct:
                    sb.Append("struct");
                    break;
                case Type.Interface:
                    sb.Append("interface");
                    break;
                case Type.Enum:
                    sb.Append("enum");
                    break;
                case Type.Delegate:
                    sb.Append("delegate");
                    break;
                case Type.Extension:
                    sb.Append("extension");
                    break;
            }
        }
    }
}