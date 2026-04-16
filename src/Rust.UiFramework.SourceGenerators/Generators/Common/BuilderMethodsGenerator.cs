using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Common;

[Generator]
public class BuilderMethodsGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateBuilderMethodsAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            GeneratorData data = new(classSymbol);
            if (data.GenerateBuilderMethods)
            {
                string fileName = classSymbol.IsAbstract ? classSymbol.ToExtensionClass() : classSymbol.Name;
                
                spc.AddSource($"{fileName}.g.cs", GenerateBuilderMethods(classSymbol, data));
            }
        });
    }
    
    private string GenerateBuilderMethods(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        if (classSymbol.IsAbstract)
        {
            return new CodeBuilder()
                .Namespace(classSymbol.ContainingNamespace)
                .Add(t => t.Public().Static().Partial().Class().Name(classSymbol.ToExtensionClass())
                    .Extension(e => e.AddGeneric("T", out string genericT)
                        .AddParameter(p => p.Type(genericT).Name("component"))
                        .Where(w => w.Type(genericT).Constrain(classSymbol))
                        .Methods(genData.Properties, data => !data.SkipBuilder, (data, method) => method.Public().Returns(genericT).Name($"Set{data.Name}")
                            .AggressiveInlining()
                            .AddParameter(p => p.Type(data.Type).If(data.Type.ShouldBePassedByIn(), p => p.In()).EndIf().Name(data.Name.ToCamelCase()))
                            .Body($"component.{data.Name} = {data.Name.ToCamelCase()};\nreturn component;"))))
                .Build();
        }

        return new CodeBuilder()
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t => t.Public().Partial().Class().Name(classSymbol.Name)
                    .Methods(genData.Properties, data => !data.SkipBuilder, (data, method) => method.Public().Returns(classSymbol.Name).Name($"Set{data.Name}")
                        .AggressiveInlining()
                        .AddParameter(p => p.Type(data.Type).If(data.Type.ShouldBePassedByIn(), p => p.In()).EndIf().Name(data.Name.ToCamelCase()))
                        .Body($"{data.Name} = {data.Name.ToCamelCase()};\nreturn this;"))
            )
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly PropertyData[] Properties;
        public readonly bool GenerateBuilderMethods;

        public GeneratorData(INamedTypeSymbol classSymbol)
        {
            Properties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition || p.HasAttribute<GenerateBuilderMethodAttribute>()).Select(p => new PropertyData(p)).ToArray();
            GenerateBuilderMethods = classSymbol.HasAttribute<GenerateBuilderMethodsAttribute>();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly bool SkipBuilder = property.HasAttribute<SkipBuilderAttribute>();
    }
}