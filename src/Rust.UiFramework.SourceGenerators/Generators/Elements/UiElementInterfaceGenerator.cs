using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Elements;

[Generator]
public class UiElementInterfaceGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateUiElementAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            InitializeCache(compilation);
            GeneratorData data = new(classSymbol);
            spc.AddSource($"{classSymbol.GetInterface()}.g.cs", GenerateInterface(data));
            spc.AddSource($"{classSymbol.GetTrackableInterface()}.g.cs", GenerateTrackableInterface(data));
        });
    }
    
    private string GenerateInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Using("Oxide.Ext.UiFramework.Types")
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.ClassSymbol.GetInterface())
                .Implements(genData.ClassSymbol.Interfaces)
                .If(genData.ParentElement is not null, i => i.Implements(genData.ParentElement.GetInterface()))
                .EndIf()
                
                //Properties
                .Properties(genData.Properties, data => !data.IsInterfaceProperty, (data, property) => property.Type(data.Type).Name(data.Name).Get()) 
            
                //Builder Methods
                .If(genData.GenerateBuilderMethods, b => b.Methods(genData.PartialProperties, p => !p.IsInterfaceProperty,
                    (data, method) => method.Returns(genData.ClassSymbol).Name($"Set{data.Name}")
                        .AddParameter(p => p.Type(data.Type).If(data.Type.ShouldBePassedByIn(), p => p.In()).EndIf().Name(data.Name.ToCamelCase()))))
            )
            .Build();
    }

    private string GenerateTrackableInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Using("Oxide.Ext.UiFramework.Types")
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.ClassSymbol.GetTrackableInterface())
                .If(genData.ParentElement is not null, i => i.Implements(genData.ParentElement.GetTrackableInterface()))
                .EndIf()
                
                //Tracked Properties
                .Properties(genData.PartialProperties, data => data.IsTracked, (data, property) => property.Type(SymbolCache.Types.Tracked.Symbol.Construct(data.Type)).Name(data.Name).Get())
                
                //Component Trackable Property
                .If(!genData.SkipComponentField, i => i.Property(property => property.Type(genData.ComponentField.Type.GetTrackableInterface()).Name(genData.ComponentField.Name).Get()))
                .EndIf())
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol ClassSymbol;
        public readonly PropertyData[] PartialProperties;
        public readonly PropertyData[] Properties;
        public readonly INamedTypeSymbol ParentElement;
        public readonly IFieldSymbol ComponentField;
        public readonly bool SkipComponentField;
        public readonly bool GenerateBuilderMethods;

        public GeneratorData(INamedTypeSymbol classSymbol)
        {
            ClassSymbol = classSymbol;
            PartialProperties = classSymbol.GetProperties().Where(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            Properties = classSymbol.GetProperties().Select(p => new PropertyData(p)).ToArray();
            ParentElement = classSymbol.GetParentWithAttribute<GenerateUiElementAttribute>();
            ComponentField = classSymbol.GetMembers().OfType<IFieldSymbol>().FirstOrDefault(f => f.IsReadOnly && f.Type.AllInterfaces.Any(i => i.Name == "IComponent"));
            SkipComponentField = classSymbol.IsAbstract;
            GenerateBuilderMethods = classSymbol.HasAttribute<GenerateBuilderMethodsAttribute>();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly bool IsTracked = property.HasAttribute<TrackedAttribute>();
        public readonly bool IsInterfaceProperty = property.IsInterfaceImplementation();

    }
}
