using System.Text;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

internal static class TypeExt
{
    extension<T>(T type) where T : IType
    {
        public T Class()
        {
            type.BuilderType = BuilderType.Class;
            return type;
        }

        public T Struct()
        {
            type.BuilderType = BuilderType.Struct;
            return type;
        }

        public T Interface()
        {
            type.BuilderType = BuilderType.Interface;
            return type;
        }

        public T Enum()
        {
            type.BuilderType = BuilderType.Enum;
            return type;
        }

        public T Delegate()
        {
            type.BuilderType = BuilderType.Delegate;
            return type;
        }

        public T Extension()
        {
            type.BuilderType = BuilderType.Extension;
            return type;
        }

        public bool IsType(BuilderType matchType) => type.BuilderType == matchType;
        
        public void BuildDeclaredType(StringBuilder sb)
        {
            switch (type.BuilderType)
            {
                case BuilderType.Class:
                    sb.Append("class");
                    break;
                case BuilderType.Struct:
                    sb.Append("struct");
                    break;
                case BuilderType.Interface:
                    sb.Append("interface");
                    break;
                case BuilderType.Enum:
                    sb.Append("enum");
                    break;
                case BuilderType.Delegate:
                    sb.Append("delegate");
                    break;
                case BuilderType.Extension:
                    sb.Append("extension");
                    break;
            }
        }
    }
}