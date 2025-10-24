using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class ParameterBuilder : IBuildable, IParameterModifier
{
    ParameterModifiers IParameterModifier.Modifiers { get; set; }
    
    private string _type;
    private string _name;
    
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
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append(this.GetModifiers());
        sb.Append(_type);
        sb.Append(' ');
        sb.Append(_name.ToCamelCase());
        return sb.ToString();
    }
}