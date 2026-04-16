using System;
using System.Runtime.CompilerServices;
using System.Text;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

public static class IAttributesExt
{
    extension<T>(T attributes) where T : IAttributes
    {
        public T AddAttribute(Action<AttributeBuilder> attribute)
        {
            AttributeBuilder builder = new();
            attributes.Attributes ??= [];
            attributes.Attributes.Add(builder);
            attribute(builder);
            return attributes;
        }

        public T AggressiveInlining()
        {
            return attributes.AddAttribute(a => a.Type(SymbolCache.Instance.MethodImpl.Symbol).AddParameter(MethodImplOptions.AggressiveInlining.ToParameterValue()));
        }

        public void BuildAttributes(StringBuilder sb, int indent, string separator)
        {
            if (attributes is not null && attributes.Attributes is { Count: > 0 })
            {
                foreach (AttributeBuilder builder in attributes.Attributes)
                {
                    sb.Append(builder.Build(indent));
                    sb.Append(separator);
                }
            }
        }
    }
}