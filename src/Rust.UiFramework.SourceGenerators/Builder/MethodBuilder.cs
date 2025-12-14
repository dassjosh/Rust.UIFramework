using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class MethodBuilder : IBuildable, IAccessModifiers, IKeywords, IGenerics, IAttributes, IParameters
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    GenericsBuilder IGenerics.Generics { get; set; }
    List<ParameterBuilder> IParameters.Parameters { get; set; } = [];
    List<AttributeBuilder> IAttributes.Attributes { get; set; }

    private string _name;
    private string _returnType;
    private string _body;
    private readonly List<WhereBuilder> _where = [];
    
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
    
    public MethodBuilder Where(Action<WhereBuilder> where)
    {
        WhereBuilder builder = new();
        _where.Add(builder);
        where(builder);
        return this;
    }
    
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
        this.BuildAttributes(sb, indent, "\r\n");
        sb.Append('\t', indent);
        this.BuildAccessModifiers(sb);
        this.BuildKeywords(sb);
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
        this.BuildGenerics(sb);
        this.BuildParameters(sb, indent);
        
        foreach (WhereBuilder builder in _where)
        {
            sb.Append(builder.Build(indent));
        }

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