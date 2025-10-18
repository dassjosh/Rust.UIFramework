using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class FieldBuilder : IBuildable, IAccessModifiers, IKeywords
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    
    private string _type;
    private string _name;

    private List<string> _initialize;

    public FieldBuilder Type(INamedTypeSymbol symbol) => Type(symbol.ToString());
    
    public FieldBuilder Type(string type)
    {
        _type = type;
        return this;
    }
    
    public FieldBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public FieldBuilder New(params string[] args)
    {
        _initialize ??= [];
        _initialize.AddRange(args);
        return this;
    }
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append(this.GetAccessModifiers());
        sb.Append(this.GetKeywords());
        sb.Append($"{_type} {_name}");
        if (_initialize is not null)
        {
            sb.Append(" = new(");
            sb.Append(string.Join(", ", _initialize));
            sb.Append(')');
        }
        sb.Append(';');
        sb.AppendLine();
        return sb.ToString();
    }
}