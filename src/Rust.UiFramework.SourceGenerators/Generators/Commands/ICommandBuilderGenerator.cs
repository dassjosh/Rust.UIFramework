using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Constants;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Commands;

[Generator]
public class ICommandBuilderGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<InterfaceDeclarationSyntax>(c => c.Name.Equals("ICommandBuilder") && c.ContainingNamespace.ToString() == "Oxide.Ext.UiFramework.Libraries", (spc, compilation, @class, classSymbol) =>
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
                t.Public().Interface().Name(classSymbol.Name)
                    .AddGenerics(g => g.Generics(Enumerable.Range(0, args)), out GenericsBuilder generics)
                    
                    //Build Method
                    .Method(m => m.Public().Returns(SymbolCache.Instance.String.Symbol).Name("Build")
                        .AddParameters(generics, (g, p) => p.Type(g).Name($"arg{g[1..]}")))
                    
                    //Partial Methods
                    .Methods(Enumerable.Range(1, args - 1), (mArg, m) => 
                        m.Public().Returns(GeneratePartialReturnType(generics, mArg)).Name("Partial")
                            .AddParameters(generics.Take(mArg), (g, p) => p.Type(g).Name($"arg{g[1..]}")));

            }).Build();
    }

    private string GeneratePartialReturnType(GenericsBuilder generics, int args)
    {
        return SymbolCache.Instance.Libraries.UiCommands.ICommandBuilder.Symbol.AsGeneric(generics.Skip(args));
    }
}
