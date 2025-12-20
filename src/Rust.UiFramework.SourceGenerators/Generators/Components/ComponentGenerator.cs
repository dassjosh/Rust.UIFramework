using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Flags;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Components;

[Generator]
public class ComponentGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            GeneratorData data = new(classSymbol);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateComponent(classSymbol, data));
        });
    }
    
    private string GenerateComponent(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Interfaces"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t =>
            {
                t.Public().Partial().Class().Name(classSymbol.Name).Implements(genData.ComponentInterfaceName)
                    .If(GeneratorFlags.AddTrackableInterface, t => t.Implements(genData.TrackableInterfaceName)).EndIf()
                    
                    //Internal Tracked Fields
                    .Fields(genData.Properties, (data, field) =>
                        field.Internal().Readonly().Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(data.Type)).Name(data.TrackedFieldName).New(data.GetPropertyDefaults()))

                    //Public Properties
                    .Properties(genData.Properties, (data, property) => property.Public().Partial().Type(data.Type).Name(data.Name).AggressiveInlining()
                        .Get($"{data.TrackedFieldName}.Value")
                        .Set($"{data.TrackedFieldName}.Value = value"))
                    
                    .If(GeneratorFlags.AddTrackableInterface, t =>
                        //Explicit Tracked Interface Implementation Properties
                        t.Properties(genData.Properties, (data, property) => property.Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(data.Type)).Name($"{genData.TrackableInterfaceName}.{data.Name}")
                            .Get(data.TrackedFieldName))

                        //As Trackable Method
                        .If(!genData.ClassSymbol.IsAbstract, builder => builder.Method(method => method.Internal()
                            .If(genData.ParentComponent is not null, m => m.New())
                            .EndIf()
                            .Returns(genData.TrackableInterfaceName).Name("AsTrackable").Body("return this;")
                            .AggressiveInlining()))
                        .EndIf()
                    ).EndIf()
                    
                    //HasChangedGenerated Method
                    .Method(method => method.Public().Override().Returns("bool").Name("HasChanged")
                        .Body(statement => statement.Return("false")
                            .Or(genData.Properties.Select(data => $"{data.TrackedFieldName}.HasChanged"))
                            .Or(genData.ChildComponentsProperties.Select(data => $"({data.Name}?.HasChanged() ?? false)"))
                            .Or("base.HasChanged()").Semicolon()))

                    //ResetHasChangedGenerated Method
                    .Method(method => method.Public().Override().Name("ResetHasChanged")
                        .Body(statement => statement.Invoke("base.ResetHasChanged()")
                            .Invoke(genData.Properties.Select(data => $"{data.TrackedFieldName}.ResetHasChanged()"))
                            .Invoke(genData.ChildComponentsProperties.Select(data => $"{data.Name}?.ResetHasChanged()"))))

                    //ResetGenerated Method
                    .Method(method => method.Public().Override().Name("Reset")
                        .Body(statement => statement.Invoke("base.Reset()")
                            .Invoke(genData.Properties.Select(data => $"{data.TrackedFieldName}.Reset()"))
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
        public readonly PropertyData[] ChildComponentsProperties;
        public readonly INamedTypeSymbol ParentComponent;

        public GeneratorData(INamedTypeSymbol classSymbol)
        {
            ClassSymbol = classSymbol;
            ComponentInterfaceName = classSymbol.GetInterface();
            TrackableInterfaceName = classSymbol.GetTrackableInterface();
            Properties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            ParentComponent = classSymbol.GetParentWithAttribute<GenerateComponentAttribute>(s => !s.IsAbstract);
            ChildComponentsProperties = classSymbol.GetProperties().Where(p => p.Type.HasInterface("IComponent")).Select(p => new PropertyData(p)).ToArray();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly string TrackedFieldName = property.TrackedFieldName();
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
