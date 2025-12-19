using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Builder.Enums;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Builders;

public class TypeBuilder : IType, IAccessModifiers, IKeywords, IBuildable, IGenerics, IAttributes, IParameters, IWhereBuildable
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    BuilderType IType.BuilderType { get; set; }
    GenericsBuilder IGenerics.Generics { get; set; }
    List<AttributeBuilder> IAttributes.Attributes { get; set; }
    List<ParameterBuilder> IParameters.Parameters { get; set; }
    List<WhereBuilder> IWhereBuildable.Where { get; set; }
    
    private string _extends;
    private readonly List<string> _implements = [];
    private readonly List<FieldBuilder> _fields = [];
    private readonly List<PropertyBuilder> _properties = [];
    private readonly List<MethodBuilder> _methods = [];
    private readonly List<TypeBuilder> _types = [];
    private List<string> _extendsParameters;

    private string _name;
    
    public TypeBuilder Name(string name)
    {
        _name = name;
        return this;
    }
    
    public TypeBuilder Implements(string @interface)
    {
        _implements.Add(@interface);
        return this;
    }
    
    public TypeBuilder Implements(ImmutableArray<INamedTypeSymbol> interfaces)
    {
        _implements.AddRange(interfaces.Select(i => i.ToString()));
        return this;
    }

    public TypeBuilder Extends(string parent)
    {
        _extends = parent;
        return this;
    }

    public TypeBuilder Extends(INamedTypeSymbol parent) => Extends(parent.ToString());
    
    public TypeBuilder AddExtendParameter(string parameter)
    {
        _extendsParameters ??= [];
        _extendsParameters.Add(parameter);
        return this;
    }
    
    public TypeBuilder AddExtendParameters(IEnumerable<string> parameters)
    {
        _extendsParameters ??= [];
        _extendsParameters.AddRange(parameters);
        return this;
    }

    public TypeBuilder Field(Action<FieldBuilder> field)
    {
        FieldBuilder builder = new();
        field(builder);
        _fields.Add(builder);
        return this;
    }
    
    public TypeBuilder Fields<T>(IEnumerable<T> fields, Action<T, FieldBuilder> field)
    {
        foreach (T item in fields)
        {
            Field(m => field(item, m));
        }
        return this;
    }

    public TypeBuilder Fields<T>(IEnumerable<T> fields, Func<T, bool> filter, Action<T, FieldBuilder> field) => Fields(fields.Where(filter), field);

    public TypeBuilder Property(Action<PropertyBuilder> property)
    {
        PropertyBuilder builder = new();
        property(builder);
        _properties.Add(builder);
        return this;
    }
    
    public TypeBuilder Properties<T>(IEnumerable<T> properties, Action<T, PropertyBuilder> property)
    {
        foreach (T item in properties)
        {
            Property(m => property(item, m));
        }
        return this;
    }
    
    public TypeBuilder Properties<T>(IEnumerable<T> properties, Func<T, bool> filter, Action<T, PropertyBuilder> property) => Properties(properties.Where(filter), property);

    public TypeBuilder Method(Action<MethodBuilder> method)
    {
        MethodBuilder builder = new();
        method(builder);
        _methods.Add(builder);
        return this;
    }
    
    public TypeBuilder Methods<T>(IEnumerable<T> methods, Action<T, MethodBuilder> method)
    {
        foreach (T item in methods)
        {
            Method(m => method(item, m));
        }
        return this;
    }
    
    public TypeBuilder Methods<T>(IEnumerable<T> methods, Func<T, bool> filter, Action<T, MethodBuilder> method) => Methods(methods.Where(filter), method);
    
    public TypeBuilder Type(Action<TypeBuilder> type)
    {
        TypeBuilder builder = new();
        type(builder);
        _types.Add(builder);
        return this;
    }
    
    public TypeBuilder Extension(Action<TypeBuilder> type)
    {
        TypeBuilder builder = new();
        builder.Extension();
        type(builder);
        _types.Add(builder);
        return this;
    }

    public string Build(int indent)
    {
        StringBuilder sb = new();
        this.BuildAttributes(sb, indent, "\n");
        sb.Append('\t', indent);
        this.BuildAccessModifiers(sb);
        this.BuildKeywords(sb);
        this.BuildDeclaredType(sb);
        if (!string.IsNullOrEmpty(_name))
        {
            sb.Append(' ');
            sb.Append(_name);
        }
        this.BuildGenerics(sb);
        this.BuildParameters(sb, indent);

        if (!string.IsNullOrEmpty(_extends) || _implements.Count != 0)
        {
            sb.Append(" : ");
        }
        
        if (!string.IsNullOrEmpty(_extends))
        {
            sb.Append(_extends);
            
            if (_extendsParameters != null)
            {
                sb.Append('(');
                sb.Append(string.Join(", ", _extendsParameters));
                sb.Append(')');
            }
        }
        
        if (_implements.Count != 0)
        {
            if (!string.IsNullOrEmpty(_extends))
            {
                sb.Append(", ");
            }
            
            sb.Append(string.Join(", ", _implements));
        }

        this.BuildWhere(sb, indent);

        sb.AppendLine();
        
        sb.Append('\t', indent);
        sb.AppendLine("{");

        bool hasAdded = false;
        ProcessBuildable(_fields, sb, indent, ref hasAdded, false);
        ProcessBuildable(_properties, sb, indent, ref hasAdded, false);
        ProcessBuildable(_methods, sb, indent, ref hasAdded, !this.IsType(BuilderType.Interface));
        ProcessBuildable(_types, sb, indent, ref hasAdded, !this.IsType(BuilderType.Interface));
        
        sb.Append('\t', indent);
        sb.AppendLine("}");
        return sb.ToString();
    }

    private static void ProcessBuildable<T>(List<T> buildables, StringBuilder sb, int indent, ref bool hasAdded, bool spaceBetween) where T : IBuildable
    {
        if (buildables.Count != 0)
        {
            if (hasAdded)
            {
                sb.AppendLine();
            }

            for (int index = 0; index < buildables.Count; index++)
            {
                T buildable = buildables[index];
                if (spaceBetween && index != 0)
                {
                    sb.AppendLine();
                }

                sb.Append(buildable.Build(indent + 1));
            }

            hasAdded = true;
        }
    }
}