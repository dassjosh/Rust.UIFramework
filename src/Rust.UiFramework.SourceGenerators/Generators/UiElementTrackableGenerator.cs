using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators;

[Generator]
public class UiElementTrackableGenerator : IIncrementalGenerator
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

                AttributeData attribute = classSymbol?.GetAttribute<GenerateUiElementAttribute>();
                if (attribute?.ConstructorArguments[0].Value is not INamedTypeSymbol interfaceType)
                {
                    continue;
                }

                GeneratorData data = new(classSymbol, interfaceType, tracked);
                spc.AddSource($"{data.InterfaceName}.g.cs", GenerateInterface(data));
            }
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
                .Properties(genData.Properties, data => data.IsTracked, (data, property) => property.Type(genData.TrackedType.Construct(data.Type)).Name(data.Name).Get())
                
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
        public readonly INamedTypeSymbol TrackedType;
        public readonly IFieldSymbol ComponentField;
        public readonly bool SkipComponentField;

        public GeneratorData(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceType, INamedTypeSymbol trackedType)
        {
            InterfaceName = classSymbol.GetTrackableInterface();
            Properties = interfaceType.GetProperties().Select(p => new PropertyData(p)).ToArray();
            ParentElement = classSymbol.GetParentWithAttribute<GenerateUiElementAttribute>();
            TrackedType = trackedType;
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
