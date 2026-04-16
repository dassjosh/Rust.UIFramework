using System;
using System.Text;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

public static class IGenericsExt
{
    extension<T>(T builder) where T : IGenerics
    {
        public T AddGeneric(string type, out string generic)
        {
            builder.Generics ??= new GenericsBuilder();
            builder.Generics.Generic(type);
            generic = type;
            return builder;
        }
    
        public T AddGenerics(Action<GenericsBuilder> generics)
        {
            builder.Generics ??= new GenericsBuilder();
            generics(builder.Generics);
            return builder;
        }
    
        public T AddGenerics(Action<GenericsBuilder> generics, out GenericsBuilder gen)
        {
            gen = builder.Generics ??= new GenericsBuilder();
            generics(builder.Generics);
            return builder;
        }
        
        public void BuildGenerics(StringBuilder sb)
        {
            if (builder.Generics is {Count: > 0})
            {
                sb.Append($"<{builder.Generics.Build(0)}>");
            }
        }
    }
}