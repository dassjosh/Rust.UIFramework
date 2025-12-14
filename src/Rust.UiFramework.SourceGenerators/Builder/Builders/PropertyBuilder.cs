using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;
using Rust.UiFramework.SourceGenerators.Extensions;

namespace Rust.UiFramework.SourceGenerators.Builder.Builders;

public class PropertyBuilder : IBuildable, IAccessModifiers, IKeywords
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    
    private string _type;
    private string _name;
    private PropertyOptions _options;
    private string _getTarget;
    private string _setTarget;
    
    public PropertyBuilder Type(INamedTypeSymbol symbol) => Type(symbol.ToString());
    public PropertyBuilder Type(ITypeSymbol symbol) => Type(symbol.ToString());
    
    public PropertyBuilder Type(string type)
    {
        _type = type;
        return this;
    }
    
    public PropertyBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public PropertyBuilder Get()
    {
        _options |= PropertyOptions.Get;
        return this;
    }
    
    public PropertyBuilder Get(string target)
    {
        Get();
        _getTarget = target;
        return this;
    }
    
    public PropertyBuilder Set()
    {
        _options |= PropertyOptions.Set;
        return this;
    }
    
    public PropertyBuilder Set(string target)
    {
        Set();
        _setTarget = target;
        return this;
    }
    
    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        this.BuildAccessModifiers(sb);
        this.BuildKeywords(sb);
        sb.Append($"{_type} {_name}");

        if (CanLambda())
        {
            sb.Append($" => {_getTarget};");
        }
        else
        {
            sb.Append(" { ");
            if (_options.HasFlag(PropertyOptions.Get))
            {
                sb.Append(string.IsNullOrEmpty(_getTarget) ? "get;" : $"get => {_getTarget};");
            
                if (_options.HasFlag(PropertyOptions.Set))
                {
                    sb.Append(' ');
                }
            }
        
            if (_options.HasFlag(PropertyOptions.Set))
            {
                sb.Append(string.IsNullOrEmpty(_getTarget) ? "set;" : $"set => {_setTarget};");
            }

            sb.Append(' ');
            sb.Append("}");
        }
       
        sb.AppendLine();
        return sb.ToString();
    }

    private bool CanLambda()
    {
        if (_options.HasFlag(PropertyOptions.Set))
        {
            return false;
        }
        
        if (string.IsNullOrEmpty(_getTarget))
        {
            return false;
        }

        if (_getTarget.ContainsAny([';', '\n']))
        {
            return false;
        }
        
        return true;
    }
}