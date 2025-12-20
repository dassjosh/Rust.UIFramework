using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;
using Rust.UiFramework.SourceGenerators.Extensions;

namespace Rust.UiFramework.SourceGenerators.Builder.Builders;

public class PropertyBuilder : IBuildable, IAccessModifiers, IKeywords, IAttributes
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    
    List<AttributeBuilder> IAttributes.Attributes { get; set; }
    
    private string _type;
    private string _name;
    private PropertyOptions _options;
    private string _getTarget;
    private string _setTarget;

    private PropertyAttributes _getAttributes;
    private PropertyAttributes _setAttributes;

    public IAttributes GetAttributes => _getAttributes ??= new PropertyAttributes(this); 
    public IAttributes SetAttributes => _setAttributes ??= new PropertyAttributes(this);
    
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

        bool hasGetAttributes = HasAttributes(_getAttributes);
        bool hasSetAttributes = HasAttributes(_setAttributes);
        bool hasAnyAttributes = hasGetAttributes || hasSetAttributes;
        
        if (CanLambda())
        {
            sb.Append($" => {_getTarget};");
        }
        else
        {
            if (hasAnyAttributes)
            {
                sb.AppendLine();
                sb.Append('\t', indent);
                sb.Append('{');
            }
            else
            {
                sb.Append(" { ");
            }
            
            if (_options.HasFlag(PropertyOptions.Get))
            {
                if (hasGetAttributes)
                {
                    sb.AppendLine();
                    this.BuildAttributes(sb, indent + 1, "\n");
                    _getAttributes.BuildAttributes(sb, indent + 1, "\n");
                    sb.Append('\t', indent + 1);
                }
                
                sb.Append(string.IsNullOrEmpty(_getTarget) ? "get;" : $"get => {_getTarget};");

                if (!hasGetAttributes && _options.HasFlag(PropertyOptions.Set))
                {
                    sb.Append(' ');
                }
            }
        
            if (_options.HasFlag(PropertyOptions.Set))
            {

                if (hasSetAttributes)
                {
                    sb.AppendLine();
                    this.BuildAttributes(sb, indent + 1, "\n");
                    _setAttributes.BuildAttributes(sb, indent + 1, "\n");
                    sb.Append('\t', indent + 1);
                }
                
                sb.Append(string.IsNullOrEmpty(_getTarget) ? "set;" : $"set => {_setTarget};");
                
                if (hasSetAttributes)
                {
                    sb.AppendLine();
                }
            }

            if (hasAnyAttributes)
            {
                sb.Append('\t', indent);
            }
            else
            {
                sb.Append(' ');
            }
            
         
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

        if (HasAttributes(_getAttributes) || HasAttributes(_setAttributes))
        {
            return false;
        }
        
        return true;
    }

    public bool HasAttributes(PropertyAttributes attributes) => attributes is not null && attributes.Attributes.Count != 0 || ((IAttributes)this).Attributes is not null && ((IAttributes)this).Attributes.Count != 0;

    public sealed class PropertyAttributes(PropertyBuilder builder) : IAttributes
    {
        public readonly PropertyBuilder Parent = builder;
        public List<AttributeBuilder> Attributes { get; set; }
    }
}