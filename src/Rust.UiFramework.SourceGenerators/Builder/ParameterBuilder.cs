using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Extensions;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class ParameterBuilder : IBuildable, IParameterModifier, IAttributes
{
    ParameterModifiers IParameterModifier.Modifiers { get; set; }
    List<AttributeBuilder> IAttributes.Attributes => _attributes;
    
    private string _type;
    private string _name;
    private string _defaultValue;
    private readonly List<AttributeBuilder> _attributes = [];
    
    public ParameterBuilder Type(string type)
    {
        _type = type;
        return this;
    }
    
    public ParameterBuilder Type(ITypeSymbol symbol)
    {
        return Type(symbol.ToString());
    }

    public ParameterBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public ParameterBuilder DefaultValue(string value)
    {
        _defaultValue = value;
        return this;
    }

    public string GetName() => _name;
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        this.BuildAttributes(sb, indent, " ");
        sb.Append(this.GetModifiers());
        sb.Append(_type);
        sb.Append(' ');
        sb.Append(_name.ToCamelCase());

        if (!string.IsNullOrEmpty(_defaultValue))
        {
            sb.Append(" = ");
            sb.Append(_defaultValue);
        }
        
        return sb.ToString();
    }
}