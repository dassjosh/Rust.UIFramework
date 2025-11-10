using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Elements;

[Generator]
public class UiElementTrackableGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateUiElementAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            if (attribute?.ConstructorArguments[0].Value is not INamedTypeSymbol interfaceType)
            {
                return;
            }
            
            InitializeCache(compilation);
            GeneratorData data = new(classSymbol, interfaceType);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateInterface(data));
        });
    }

    private string GenerateInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Using("Oxide.Ext.UiFramework.Types")
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.InterfaceName)
                .If(genData.ParentElement is not null, i => i.Implements(genData.ParentElement.GetTrackableInterface()))
                .EndIf()
                
                //Tracked Properties
                .Properties(genData.Properties, data => data.IsTracked, (data, property) => property.Type(SymbolCache.Types.Tracked.Symbol.Construct(data.Type)).Name(data.Name).Get())
                
                //Component Trackable Property
                .If(!genData.SkipComponentField, i => i.Property(property => property.Type(genData.ComponentField.Type.GetTrackableInterface()).Name(genData.ComponentField.Name).Get()))
                .EndIf())
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly string InterfaceName;
        public readonly PropertyData[] Properties;
        public readonly INamedTypeSymbol ParentElement;
        public readonly IFieldSymbol ComponentField;
        public readonly bool SkipComponentField;

        public GeneratorData(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceType)
        {
            InterfaceName = classSymbol.GetTrackableInterface();
            Properties = interfaceType.GetProperties().Select(p => new PropertyData(p)).ToArray();
            ParentElement = classSymbol.GetParentWithAttribute<GenerateUiElementAttribute>();
            ComponentField = classSymbol.GetMembers().OfType<IFieldSymbol>().FirstOrDefault();
            SkipComponentField = interfaceType.HasAttribute<SkipComponentFieldAttribute>();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly bool IsTracked = property.HasAttribute<TrackedAttribute>();
    }
}
