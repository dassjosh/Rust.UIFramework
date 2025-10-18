using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;
using Rust.UiFramework.SourceGenerators.Logging;

namespace Rust.UiFramework.SourceGenerators.Generators;

[Generator]
public class ComponentGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s is ClassDeclarationSyntax,
                transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node)
            .Where(static m => m is not null);

        IncrementalValueProvider<(Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, (spc, source) =>
        {
            (Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes) = source;

            INamedTypeSymbol tracked = SymbolCache.GetTracked(compilation);
            
            foreach (ClassDeclarationSyntax @class in classes)
            {
                SemanticModel model = compilation.GetSemanticModel(@class.SyntaxTree);
                INamedTypeSymbol classSymbol = model.GetDeclaredSymbol(@class) as INamedTypeSymbol;

                AttributeData attribute = classSymbol?.GetAttribute<GenerateComponentAttribute>();
                if (attribute?.ConstructorArguments[0].Value is not INamedTypeSymbol interfaceType)
                {
                    continue;
                }

                GeneratorData data = new(classSymbol, interfaceType, tracked);
                spc.AddSource($"{classSymbol.Name}.g.cs", GenerateComponent(classSymbol, data));
            }
        });
    }
    
    private string GenerateComponent(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Types", "Oxide.Ext.UiFramework.Json", "Oxide.Ext.UiFramework.Interfaces"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t =>
            {
                t.Public().Partial().Class().Name(classSymbol.Name).Implements(genData.InterfaceName)

                    //Private Tracked Fields
                    .Fields(genData.Properties, (data, field) =>
                        field.Private().Readonly().Type(genData.TrackedType.Construct(data.Type)).Name(data.PrivateFieldName).New(data.GetPropertyDefaults()))

                    //Public Properties
                    .Properties(genData.Properties, (data, property) => property.Public().Type(data.Type).Name(data.Name)
                        .Get($"{data.PrivateFieldName}.Value")
                        .Set($"{data.PrivateFieldName}.Value = value"))

                    //Explicit Tracked Interface Implementation Properties
                    .Properties(genData.Properties, (data, property) => property.Type(genData.TrackedType.Construct(data.Type)).Name($"{genData.InterfaceName}.{data.Name}")
                        .Get(data.PrivateFieldName))

                    //Builder Methods
                    .If(genData.GenerateBuilderMethods, builder => builder.Methods(genData.Properties, data => !data.SkipBuilder, (data, method) =>
                        method.Public().Returns(genData.ClassSymbol).Name($"Set{data.Name}")
                            .AddParameter(parameter => parameter.Type(data.Type).Name(data.Name)
                                .If(data.Type.ShouldBePassedByIn(), p => p.In())
                                .EndIf())
                            .Body($"{data.Name} = {data.Name.ToCamelCase()};" +
                                  $"\nreturn this;")))
                    .EndIf()

                    //As Trackable Method
                    .If(!genData.ClassSymbol.IsAbstract, builder => builder.Method(method => method.Public()
                        .If(genData.ParentComponent is not null, m => m.New())
                        .EndIf()
                        .Returns(genData.InterfaceName).Name("AsTrackable").Body("return this;")))
                    .EndIf()

                    //HasChangedGenerated Method
                    .Method(method => method.Protected().Override().Returns("bool").Name("HasChangedGenerated")
                        .Body(statement => statement.Return("base.HasChangedGenerated()").Or(genData.Properties.Select(data => $"{data.PrivateFieldName}.HasChanged")).Semicolon()))

                    //ResetHasChangedGenerated Method
                    .Method(method => method.Protected().Override().Name("ResetHasChangedGenerated")
                        .Body(statement => statement.Invoke("base.ResetHasChangedGenerated()").Invoke(genData.Properties.Select(data => $"{data.PrivateFieldName}.ResetHasChanged()"))))

                    //ResetGenerated Method
                    .Method(method => method.Protected().Override().Name("ResetGenerated")
                        .Body(statement => statement.Invoke("base.ResetGenerated()").Invoke(genData.Properties.Select(data => $"{data.PrivateFieldName}.Reset()"))));

            }).Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol ClassSymbol;
        public readonly string InterfaceName;
        public readonly PropertyData[] Properties;
        public readonly bool GenerateBuilderMethods;
        public readonly INamedTypeSymbol ParentComponent;
        public readonly INamedTypeSymbol TrackedType;

        public GeneratorData(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceType, INamedTypeSymbol trackedType)
        {
            ClassSymbol = classSymbol;
            InterfaceName = classSymbol.GetTrackableInterface();
            Properties = interfaceType.GetProperties().Select(p => new PropertyData(p)).ToArray();
            GenerateBuilderMethods = classSymbol.HasAttribute<GenerateBuilderMethodsAttribute>();
            ParentComponent = classSymbol.GetParentWithAttribute<GenerateComponentAttribute>(s => !s.IsAbstract);
            TrackedType = trackedType;
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly string PrivateFieldName = property.Name.ToPrivateField();
        public readonly AttributeData TrackedDefaults = property.GetAttribute<TrackedDefaultsAttribute>();
        public readonly bool SkipBuilder = property.HasAttribute<SkipBuilderAttribute>();
        
        public string GetPropertyDefaults()
        {
            if (TrackedDefaults == null)
            {
                return string.Empty;
            }

            return TrackedDefaults.ConstructorArguments.Length switch
            {
                1 => TrackedDefaults.ConstructorArguments[0].Value?.ToString().ToLower(),
                2 => GetArgValue(TrackedDefaults.ConstructorArguments, 0),
                4 => $"{GetArgValue(TrackedDefaults.ConstructorArguments, 0)}, {GetArgValue(TrackedDefaults.ConstructorArguments, 2)}",
                _ => string.Empty
            };
        }

        private static string GetArgValue(ImmutableArray<TypedConstant> args, int index)
        {
            if (args[index].IsNull || args[index + 1].IsNull)
            {
                return "null";
            }

            return $"{args[index].Value}.{args[index + 1].Value}";
        }
    }
}
