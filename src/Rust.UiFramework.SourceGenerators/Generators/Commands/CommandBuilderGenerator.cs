using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Constants;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Commands;

[Generator]
public class CommandBuilderGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax>(c => c.Name.Equals("CommandBuilder") && c.ContainingNamespace.ToString() == "Oxide.Ext.UiFramework.Libraries", (spc, compilation, @class, classSymbol) =>
        {
            SymbolCache.Initialize(compilation);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateParser(classSymbol));
        });
    }
    
    private string GenerateParser(INamedTypeSymbol classSymbol)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Extensions"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(Enumerable.Range(1, UiCommands.MaxArgs), (args, t) =>
            {
                t.Internal().Class().Name(classSymbol.Name)
                    .AddGenerics(g => g.Generics(Enumerable.Range(0, args)), out GenericsBuilder generics)
                    .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.ICommandBuilderData.Symbol).Name("data"))
                    .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.PartialArgs.Symbol).Name("partial").DefaultValue("default"))
                    .Extends(SymbolCache.Instance.Libraries.UiCommands.BaseCommandBuilder.Symbol)
                    .Implements(SymbolCache.Instance.Libraries.UiCommands.ICommandBuilder.Symbol.AsGeneric(generics))
                    .AddExtendParameter("data")
                    .AddExtendParameter("partial")
                    
                    //Build Method
                    .Method(m => m.Public().Returns(SymbolCache.Instance.String.Symbol).Name("Build")
                        .AddParameters(generics, (g, p) => p.Type(g).Name($"arg{g[1..]}"))
                        .Body(GenerateBuildBody(m.Parameters)))
                    
                    //Partial Methods
                    .Methods(Enumerable.Range(1, args - 1), (mArg, m) => 
                        m.Public().Returns(GeneratePartialReturnType(generics, mArg)).Name("Partial")
                            .AddParameters(generics.Take(mArg), (g, p) => p.Type(g).Name($"arg{g[1..]}"))
                            .Body(GeneratePartialBody(m.Parameters, generics, mArg)));

            }).Build();
    }

    private string GenerateBuildBody(IEnumerable<ParameterBuilder> parameters)
    {
        StringBuilder sb = new();
        sb.AppendLine($"{SymbolCache.Instance.Libraries.UiCommands.ArgWriterIterator.Symbol} writer = StartBuilding();");
        sb.AppendLine($"writer.WriteArgs({string.Join(", ", parameters.Select(p => p.GetName()))});");
        sb.AppendLine("return FinishBuilding(writer);");
        return sb.ToString();
    }

    private string GeneratePartialReturnType(GenericsBuilder generics, int args)
    {
        return SymbolCache.Instance.Libraries.UiCommands.ICommandBuilder.Symbol.AsGeneric(generics.Skip(args));
    }

    private string GeneratePartialBody(IEnumerable<ParameterBuilder> parameters, GenericsBuilder generics, int args)
    {
        StringBuilder sb = new();
        sb.AppendLine($"{SymbolCache.Instance.Libraries.UiCommands.ArgWriterIterator.Symbol} writer = StartBuilding();");
        sb.AppendLine($"writer.WriteArgs({string.Join(", ", parameters.Select(p => p.GetName()))});");
        sb.AppendLine($"return new {SymbolCache.Instance.Libraries.UiCommands.CommandBuilder.Symbol.AsGeneric(generics.Skip(args))}(Data, new {SymbolCache.Instance.Libraries.UiCommands.PartialArgs.Symbol}(writer));");
        return sb.ToString();
    }
}
