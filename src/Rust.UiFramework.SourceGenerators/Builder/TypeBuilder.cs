using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class TypeBuilder : IType, IAccessModifiers, IKeywords, IBuildable, IGenerics
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    Type IType.Type { get; set; }
    GenericsBuilder IGenerics.Generics => _generics;
    
    private string _extends;
    private readonly List<string> _implements = [];
    private readonly List<FieldBuilder> _fields = [];
    private readonly List<PropertyBuilder> _properties = [];
    private readonly List<MethodBuilder> _methods = [];
    private readonly List<TypeBuilder> _types = [];
    private List<ParameterBuilder> _parameters;
    private List<string> _extendsParameters;
    private GenericsBuilder _generics;

    private string _name;
    
    public TypeBuilder Name(string name)
    {
        _name = name;
        return this;
    }
    
    public TypeBuilder AddGenerics(Action<GenericsBuilder> generics)
    {
        GenericsBuilder builder = new();
        generics(builder);
        _generics = builder;
        return this;
    }
    
    public TypeBuilder AddGenerics(Action<GenericsBuilder> generics, out GenericsBuilder builder)
    {
        builder = new GenericsBuilder();
        generics(builder);
        _generics = builder;
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
    
    public TypeBuilder Properties<T>(IEnumerable<T> fields, Func<T, bool> filter, Action<T, PropertyBuilder> property) => Properties(fields.Where(filter), property);

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
    
    public TypeBuilder Methods<T>(IEnumerable<T> fields, Func<T, bool> filter, Action<T, MethodBuilder> method) => Methods(fields.Where(filter), method);
    
    public TypeBuilder Type(Action<TypeBuilder> type)
    {
        TypeBuilder builder = new();
        type(builder);
        _types.Add(builder);
        return this;
    }
    
    public TypeBuilder AddParameter(Action<ParameterBuilder> parameter)
    {
        ParameterBuilder builder = new();
        parameter(builder);
        _parameters ??= [];
        _parameters.Add(builder);
        return this;
    }
    
    public TypeBuilder AddParameters<T>(IEnumerable<T> parameters, Action<T, ParameterBuilder> parameter)
    {
        foreach (T data in parameters)
        {
            ParameterBuilder builder = new();
            parameter(data, builder);
            _parameters ??= [];
            _parameters.Add(builder);
        }
        return this;
    }
    
    public IEnumerable<ParameterBuilder> EnumerateParameters() => _parameters;

    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append(this.GetAccessModifiers());
        sb.Append(this.GetKeywords());
        sb.Append(this.GetDeclaredType());
        sb.Append(' ');
        sb.Append(_name);
        if (_generics != null)
        {
            sb.Append($"<{_generics.Build(0)}>");
        }

        if (_parameters != null)
        {
            sb.Append('(');
            sb.Append(string.Join(", ", _parameters.Select(p => p.Build(indent))));
            sb.Append(')');
        }

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

        sb.AppendLine();
        
        sb.Append('\t', indent);
        sb.AppendLine("{");

        bool hasAdded = false;
        ProcessBuildable(_fields, sb, indent, ref hasAdded);
        ProcessBuildable(_properties, sb, indent, ref hasAdded);
        ProcessBuildable(_methods, sb, indent, ref hasAdded);
        ProcessBuildable(_types, sb, indent, ref hasAdded);
        
        sb.Append('\t', indent);
        sb.AppendLine("}");
        return sb.ToString();
    }

    private void ProcessBuildable<T>(List<T> buildables, StringBuilder sb, int indent, ref bool hasAdded) where T : IBuildable
    {
        if (buildables.Count != 0)
        {
            if (hasAdded)
            {
                sb.AppendLine();
            }
            
            foreach (T buildable in buildables)
            {
                sb.Append(buildable.Build(indent + 1));
            }

            hasAdded = true;
        }
    }
}