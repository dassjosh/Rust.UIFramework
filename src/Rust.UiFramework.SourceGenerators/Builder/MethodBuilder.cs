using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class MethodBuilder : IBuildable, IAccessModifiers, IKeywords, IGenerics
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    public GenericsBuilder Generics { get; private set; }

    private string _name;
    private string _returnType;
    private string _body;

    private readonly List<ParameterBuilder> _parameters = [];
    
    public MethodBuilder Returns(INamedTypeSymbol symbol) => Returns(symbol.ToString());
    
    public MethodBuilder Returns(string type)
    {
        _returnType = type;
        return this;
    }

    public MethodBuilder Void() => Returns("void");
    
    public MethodBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MethodBuilder AddGenerics(Action<GenericsBuilder> generics) => AddGenerics(generics, out GenericsBuilder _);

    public MethodBuilder AddGenerics(Action<GenericsBuilder> generics, out GenericsBuilder builder)
    {
        builder = new GenericsBuilder();
        generics(builder);
        Generics = builder;
        return this;
    }

    public MethodBuilder AddParameter(Action<ParameterBuilder> parameter)
    {
        ParameterBuilder builder = new();
        parameter(builder);
        _parameters.Add(builder);
        return this;
    }
    
    public MethodBuilder AddParameters<T>(IEnumerable<T> parameters, Action<T, ParameterBuilder> parameter)
    {
        foreach (T data in parameters)
        {
            ParameterBuilder builder = new();
            parameter(data, builder);
            _parameters.Add(builder);
        }
        return this;
    }
    
    public IEnumerable<ParameterBuilder> Parameters => _parameters.Select(p => p);
    
    public MethodBuilder Body(string body)
    {
        _body = body;
        return this;
    }

    public MethodBuilder Body(Action<StatementBuilder> builder)
    {
        StatementBuilder sb = new();
        builder(sb);
        _body = sb.Build(0);
        return this;
    }
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append(this.GetAccessModifiers());
        sb.Append(this.GetKeywords());
        if (!string.IsNullOrEmpty(_returnType))
        {
            sb.Append(_returnType);
            sb.Append(' ');
        }
        else
        {
            sb.Append("void ");
        }

        sb.Append(_name);
        if (Generics != null && Generics.Count != 0)
        {
            sb.Append($"<{Generics.Build(0)}>");
        }

        sb.Append('(');
        sb.Append(string.Join(", ", _parameters.Select(p => p.Build(indent))));
        sb.Append(')');

        if (_body == null)
        {
            sb.Append(";");
        }
        else if (CanLambda())
        {
            sb.Append(" => ");
            if (_body.StartsWith("return "))
            {
                sb.Append(_body[7..]);
            }
            else
            {
                sb.Append(_body);
            }
        }
        else
        {
            sb.AppendLine();
            sb.Append('\t', indent);
            sb.Append('{');
            sb.AppendLine();

            string[] lines = _body.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        
            foreach (string line in lines)
            {
                sb.Append('\t', indent + 1);
                sb.AppendLine(line);
            }
        
            sb.Append('\t', indent);
            sb.Append('}');
        }
        
        sb.AppendLine();
        return sb.ToString();
    }

    private bool CanLambda()
    {
        if (_body.Count(c => c == ';') > 1)
        {
            return false;
        }

        return true;
    }
}