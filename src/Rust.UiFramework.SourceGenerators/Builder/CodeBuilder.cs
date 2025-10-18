using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Builder;

public class CodeBuilder
{
    private readonly List<string> _usings = [];
    private string _namespace;
    private readonly List<IBuildable> _buildables = [];
    
    public static CodeBuilder Create()
    {
        return new CodeBuilder();
    }
    
    public CodeBuilder Using(string @using)
    {
        _usings.Add(@using);
        return this;
    }
    
    public CodeBuilder Usings(IEnumerable<string> @usings)
    {
        _usings.AddRange(@usings);
        return this;
    }
    
    public CodeBuilder Namespace(string @namespace)
    {
        _namespace = @namespace;
        return this;
    }

    public CodeBuilder Namespace(INamespaceSymbol @namespace) => Namespace(@namespace.ToString());
    
    public CodeBuilder Add(Action<TypeBuilder> type)
    {
        TypeBuilder builder = new();
        type(builder);
        _buildables.Add(builder);
        return this;
    }

    public string Build()
    {
        StringBuilder sb = new();
        foreach (string @using in _usings)
        {
            sb.AppendLine($"using {@using};");
        }
        sb.AppendLine();
        sb.AppendLine($"namespace {_namespace};");

        foreach (IBuildable buildable in _buildables)
        {
            sb.AppendLine(buildable.Build(0));
        }
        
        sb.AppendLine();
        
        return sb.ToString();
    }
}