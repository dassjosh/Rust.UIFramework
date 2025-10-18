using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class TypeBuilder : IType, IAccessModifiers, IKeywords, IBuildable
{
    AccessModifiers IAccessModifiers.AccessModifiers { get; set; }
    Keywords IKeywords.Keywords { get; set; }
    Type IType.Type { get; set; }
    
    private readonly List<string> _implements = [];
    private readonly List<FieldBuilder> _fields = [];
    private readonly List<PropertyBuilder> _properties = [];
    private readonly List<MethodBuilder> _methods = [];

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

    public TypeBuilder Extends(string parent)
    {
        _implements.Insert(0, parent);
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

    public string Build(int indent)
    {
        StringBuilder sb = new();
        sb.Append('\t', indent);
        sb.Append(this.GetAccessModifiers());
        sb.Append(this.GetKeywords());
        sb.Append(this.GetDeclaredType());
        sb.Append(' ');
        sb.Append(_name);

        if (_implements.Count != 0)
        {
            sb.Append(" : ");
            sb.Append(string.Join(", ", _implements));
        }

        sb.AppendLine();
        
        sb.Append('\t', indent);
        sb.AppendLine("{");

        foreach (FieldBuilder field in _fields)
        {
            //sb.Append('\t', indent + 1);
            sb.Append(field.Build(indent + 1));
        }
        
        sb.AppendLine();
        
        foreach (PropertyBuilder property in _properties)
        {
            // sb.Append('\t', indent + 1);
            sb.Append(property.Build(indent + 1));
        }
        
        sb.AppendLine();
        
        foreach (MethodBuilder method in _methods)
        {
            //sb.Append('\t', indent + 1);
            sb.Append(method.Build(indent + 1));
        }
        
        sb.Append('\t', indent);
        sb.AppendLine("}");
        return sb.ToString();
    }
}