using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class AttributeBuilder : IBuildable
{
    private string _attribute;
    private readonly List<string> _parameters = [];

    public AttributeBuilder Type(string name)
    {
        _attribute = name;
        return this;
    }

    public AttributeBuilder Type(INamedTypeSymbol type) => Type(type.ToString());
    
    public AttributeBuilder AddParameter(string parameter)
    {
        _parameters.Add(parameter);
        return this;
    }
    
    public AttributeBuilder AddParameters<T>(IEnumerable<T> parameters, Func<T, string> parameter)
    {
        foreach (T data in parameters)
        {
            _parameters.Add(parameter(data));
        }
        return this;
    }
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append('[');
        sb.Append(_attribute);
        if (_parameters is { Count: > 0 })
        {
            sb.Append('(');
            sb.Append(string.Join(", ", _parameters));
            sb.Append(')');
        }
        sb.Append(']');
        
        return sb.ToString();
    }
}