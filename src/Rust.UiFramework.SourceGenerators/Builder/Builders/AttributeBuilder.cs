using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Builders;

public class AttributeBuilder : IParameters, IBuildable
{
    List<ParameterBuilder> IParameters.Parameters { get; set; }
    
    private string _attribute;

    public AttributeBuilder Type(string name)
    {
        _attribute = name;
        return this;
    }

    public AttributeBuilder Type(INamedTypeSymbol type) => Type(type.ToString());
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append('[');
        sb.Append(_attribute);
        this.BuildParameters(sb, indent);
        sb.Append(']');
        
        return sb.ToString();
    }
}