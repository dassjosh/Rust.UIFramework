using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Builders;

public class FieldBuilder : IBuildable, IAccessModifiers, IKeywords
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    
    private string _type;
    private string _name;

    private List<string> _initialize;
    private List<string> _equals;

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

    public FieldBuilder Equals(params string[] args)
    {
        _equals ??= [];
        _equals.AddRange(args);
        return this;
    }
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        this.BuildAccessModifiers(sb);
        this.BuildKeywords(sb);
        sb.Append($"{_type} {_name}");
        if (_initialize is not null)
        {
            sb.Append(" = new(");
            sb.Append(string.Join(", ", _initialize));
            sb.Append(')');
        }
        
        if (_equals is not null)
        {
            sb.Append(" = ");
            sb.Append(string.Join(", ", _equals));
        }
        sb.Append(';');
        sb.AppendLine();
        return sb.ToString();
    }
}