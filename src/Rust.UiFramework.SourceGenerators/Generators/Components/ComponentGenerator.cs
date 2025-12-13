using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Components;

[Generator]
public class ComponentGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            InitializeCache(compilation);
            GeneratorData data = new(classSymbol, SymbolCache);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateComponent(classSymbol, data));
        });
    }
    
    private string GenerateComponent(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Types", "Oxide.Ext.UiFramework.Json", "Oxide.Ext.UiFramework.Interfaces"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t =>
            {
                t.Public().Partial().Class().Name(classSymbol.Name).Implements(genData.ComponentInterfaceName).Implements(genData.TrackableInterfaceName)

                    //Private Tracked Fields
                    .Fields(genData.Properties, (data, field) =>
                        field.Protected().Readonly().Type(SymbolCache.Types.Tracked.Symbol.Construct(data.Type)).Name(data.PrivateFieldName).New(data.GetPropertyDefaults()))

                    //Public Properties
                    .Properties(genData.Properties, (data, property) => property.Public().Partial().Type(data.Type).Name(data.Name)
                        .Get($"{data.PrivateFieldName}.Value")
                        .Set($"{data.PrivateFieldName}.Value = value"))

                    //Explicit Tracked Interface Implementation Properties
                    .Properties(genData.Properties, (data, property) => property.Type(SymbolCache.Types.Tracked.Symbol.Construct(data.Type)).Name($"{genData.TrackableInterfaceName}.{data.Name}")
                        .Get(data.PrivateFieldName))

                    //Builder Methods
                    .If(genData.GenerateBuilderMethods, builder => builder.Methods(genData.BuilderProperties, data => !data.SkipBuilder, (data, method) =>
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
                        .Returns(genData.TrackableInterfaceName).Name("AsTrackable").Body("return this;")))
                    .EndIf()

                    //HasChangedGenerated Method
                    .Method(method => method.Public().Override().Returns("bool").Name("HasChanged")
                        .Body(statement => statement.Return("false")
                            .Or(genData.Properties.Select(data => $"{data.PrivateFieldName}.HasChanged"))
                            .Or(genData.ChildComponentsProperties.Select(data => $"({data.Name}?.HasChanged() ?? false)"))
                            .Or("base.HasChanged()").Semicolon()))

                    //ResetHasChangedGenerated Method
                    .Method(method => method.Public().Override().Name("ResetHasChanged")
                        .Body(statement => statement.Invoke("base.ResetHasChanged()")
                            .Invoke(genData.Properties.Select(data => $"{data.PrivateFieldName}.ResetHasChanged()"))
                            .Invoke(genData.ChildComponentsProperties.Select(data => $"{data.Name}?.ResetHasChanged()"))))

                    //ResetGenerated Method
                    .Method(method => method.Public().Override().Name("Reset")
                        .Body(statement => statement.Invoke("base.Reset()")
                            .Invoke(genData.Properties.Select(data => $"{data.PrivateFieldName}.Reset()"))
                            .Invoke(genData.ChildComponentsProperties.Select(data => $"{data.Name}?.TryDispose()"))
                            .Invoke(genData.ChildComponentsProperties.Select(data => $"{data.Name} = null"))));

            }).Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol ClassSymbol;
        public readonly string ComponentInterfaceName;
        public readonly string TrackableInterfaceName;
        public readonly PropertyData[] Properties;
        public readonly PropertyData[] BuilderProperties;
        public readonly PropertyData[] ChildComponentsProperties;
        public readonly bool GenerateBuilderMethods;
        public readonly INamedTypeSymbol ParentComponent;

        public GeneratorData(INamedTypeSymbol classSymbol, SymbolCache symbolCache)
        {
            ClassSymbol = classSymbol;
            ComponentInterfaceName = classSymbol.GetInterface();
            TrackableInterfaceName = classSymbol.GetTrackableInterface();
            Properties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            BuilderProperties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition || p.HasAttribute<GenerateBuilderMethodAttribute>()).Select(p => new PropertyData(p)).ToArray();
            GenerateBuilderMethods = classSymbol.HasAttribute<GenerateBuilderMethodsAttribute>();
            ParentComponent = classSymbol.GetParentWithAttribute<GenerateComponentAttribute>(s => !s.IsAbstract);
            ChildComponentsProperties = classSymbol.GetProperties().Where(p => p.Type.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, symbolCache.Interfaces.IChildComponent.Symbol))).Select(p => new PropertyData(p)).ToArray();
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
