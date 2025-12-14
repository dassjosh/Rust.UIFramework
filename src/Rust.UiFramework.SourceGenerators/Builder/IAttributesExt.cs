using System;
using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

public static class IAttributesExt
{
    extension<T>(T attributes) where T : IAttributes
    {
        public T AddAttribute(Action<AttributeBuilder> attribute)
        {
            AttributeBuilder builder = new();
            attributes.Attributes.Add(builder);
            attribute(builder);
            return attributes;
        }

        public void BuildAttributes(StringBuilder sb, int indent, string separator)
        {
            foreach (AttributeBuilder builder in attributes.Attributes)
            {
                sb.Append(builder.Build(indent));
                sb.Append(separator);
            }
        }
    }
}