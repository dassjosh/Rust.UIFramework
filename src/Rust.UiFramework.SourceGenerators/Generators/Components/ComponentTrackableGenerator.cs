using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Components;

[Generator]
public class ComponentTrackableGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            if (attribute?.ConstructorArguments[0].Value is not INamedTypeSymbol interfaceType)
            {
                return;
            }
            
            InitializeCache(compilation);
            GeneratorData data = new(classSymbol, interfaceType);
            spc.AddSource($"{data.InterfaceName}.g.cs", GenerateInterface(data));
        });
    }

    private string GenerateInterface(GeneratorData genData)
    {
        return new CodeBuilder()
            .Using("Oxide.Ext.UiFramework.Types")
            .Namespace("Oxide.Ext.UiFramework.Interfaces")
            .Add(t => t.Public().Interface().Name(genData.InterfaceName)
                .If(genData.ParentComponent is not null, i => i.Implements(genData.ParentComponent.GetTrackableInterface()))
                .EndIf()
                
                //Tracked Properties
                .Properties(genData.Properties, (data, property) => property.Type(SymbolCache.Types.Tracked.Symbol.Construct(data.Type)).Name(data.Name).Get()))
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly string InterfaceName;
        public readonly PropertyData[] Properties;
        public readonly INamedTypeSymbol ParentComponent;

        public GeneratorData(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceType)
        {
            InterfaceName = classSymbol.GetTrackableInterface();
            Properties = interfaceType.GetProperties().Select(p => new PropertyData(p)).ToArray();
            ParentComponent = classSymbol.GetParentWithAttribute<GenerateComponentAttribute>();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
    }
}
