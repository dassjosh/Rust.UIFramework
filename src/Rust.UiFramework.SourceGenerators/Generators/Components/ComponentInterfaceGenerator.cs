using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Components;

[Generator]
public class ComponentInterfaceGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            GeneratorData data = new(classSymbol);
            spc.AddSource($"{data.ComponentInterfaceName}.g.cs", GenerateInterface(data));
            spc.AddSource($"{data.TrackableInterfaceName}.g.cs", GenerateTrackableInterface(data));
        });
    }
    
    private string GenerateInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.ComponentInterfaceName)
                .If(genData.ParentComponent is not null, i => i.Implements(genData.ParentComponent.GetInterface()))
                .EndIf()
                
                //Properties
                .Properties(genData.Properties, (data, property) => property.Type(data.Type).Name(data.Name).Get().Set())
            
                //Builder Methods
                .If(genData.GenerateBuilderMethods && !genData.ClassSymbol.IsAbstract, builder => builder.Methods(genData.Properties, data => !data.SkipBuilder, (data, method) =>
                    method.Returns(genData.ClassSymbol).Name($"Set{data.Name}")
                        .AddParameter(parameter => parameter.Type(data.Type).Name(data.Name)
                            .If(data.Type.ShouldBePassedByIn(), p => p.In())
                            .EndIf())))
                .EndIf()
            )
            
            .Build();
    }

    private string GenerateTrackableInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Using("Oxide.Ext.UiFramework.Types")
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.TrackableInterfaceName)
                .If(genData.ParentComponent is not null, i => i.Implements(genData.ParentComponent.GetTrackableInterface()))
                .EndIf()
                
                //Tracked Properties
                .Properties(genData.Properties, (data, property) => property.Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(data.Type)).Name(data.Name).Get()))
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol ClassSymbol;
        public readonly string ComponentInterfaceName;
        public readonly string TrackableInterfaceName;
        public readonly PropertyData[] Properties;
        public readonly INamedTypeSymbol ParentComponent;
        public readonly bool GenerateBuilderMethods;

        public GeneratorData(INamedTypeSymbol classSymbol)
        {
            ClassSymbol = classSymbol;
            ComponentInterfaceName = classSymbol.GetInterface();
            TrackableInterfaceName = classSymbol.GetTrackableInterface();
            Properties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            ParentComponent = classSymbol.GetParentWithAttribute<GenerateComponentAttribute>();
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
