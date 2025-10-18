using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators;

[Generator]
public class UiElementGenerator : IIncrementalGenerator
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

            INamedTypeSymbol tracked = classes.GetTrackedType(compilation);
            
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
                spc.AddSource($"{classSymbol.Name}.g.cs", GenerateElement(classSymbol, data));
            }
        });
    }
    
    private string GenerateElement(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Types", "Oxide.Ext.UiFramework.Json", "Oxide.Ext.UiFramework.Interfaces"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t => t.Public().Partial().Class().Name(classSymbol.Name)
                .Implements(genData.ClassSymbol.GetTrackableInterface())
                
                //Private Tracked Fields
                .Fields(genData.Properties, (data, field) => field.Private().Readonly().Type(genData.TrackedType.Construct(data.Type)).Name(data.Name.ToPrivateField()).New(data.GetPropertyDefaults()))
               
                //Public Properties Setting Component Fields
                .Properties(genData.Properties, (data, property) => property.Public().Type(data.TypeName).Name(data.Name)
                    .If(data.IsTracked, p => p.Get($"{data.Name.ToPrivateField()}.Value").Set($"{data.Name.ToPrivateField()}.Value = value"))
                    .ElseIf(data.ComponentPropertyTargetType is PropertyTargetType.Self, p => p.Get().Set())
                    .Else(p => p.Get($"{data.GetPropertyTarget() ?? genData.ComponentFieldName}.{data.GetPropertyName()}")
                        .Set($"{data.GetPropertyTarget() ?? genData.ComponentFieldName}.{data.GetPropertyName()} = value"))
                    .EndIf())
               
                //Public Tracked Explicit Interface Implementation Properties
                .Properties(genData.Properties, data => data.IsTracked, (data, property) => property.Type(genData.TrackedType.Construct(data.Type))
                    .Name($"{genData.ClassSymbol.GetTrackableInterface()}.{data.Name}").Get(data.Name.ToPrivateField()))
                
                //AsTrackable for Component and Element
                .If(!genData.ClassSymbol.IsAbstract, t => t.Property(p => 
                    p.Type(genData.ComponentField.Type.GetTrackableInterface())
                        .Name($"{genData.ClassSymbol.GetTrackableInterface()}.{genData.ComponentField.Name}")
                        .Get($"{genData.ComponentFieldName}.AsTrackable()"))
                    .Method(m => m.Public().Name("AsTrackable").Returns(genData.ClassSymbol.GetTrackableInterface()).Body("return this;")))
                .EndIf()
                
                //Builder Methods
                .If(!genData.SkipBuilder, t => t.Methods(genData.Properties, data => !data.SkipBuilder, 
                    (data, method) => method.Public().Returns(classSymbol.Name).Name($"Set{data.Name}")
                        .AddParameter(p => p.Type(data.Type).If(data.Type.ShouldBePassedByIn(), p => p.In()).EndIf().Name(data.Name.ToCamelCase()))
                        .Body($"{data.Name} = {data.Name.ToCamelCase()};\nreturn this;")))
            )
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol ClassSymbol;
        public readonly string ComponentFieldName;
        public readonly PropertyData[] Properties;
        public readonly bool SkipBuilder;
        public readonly INamedTypeSymbol TrackedType;
        public readonly IFieldSymbol ComponentField;

        public GeneratorData(INamedTypeSymbol classSymbol, INamedTypeSymbol interfaceType, INamedTypeSymbol trackedType)
        {
            ClassSymbol = classSymbol;
            TrackedType = trackedType;
            ComponentFieldName = classSymbol.GetMembers().OfType<IFieldSymbol>().FirstOrDefault()?.Name;
            Properties = interfaceType.GetProperties().Select(p => new PropertyData(p)).ToArray();
            SkipBuilder = interfaceType.HasAttribute<SkipBuilderAttribute>();
            ComponentField = classSymbol.GetMembers().OfType<IFieldSymbol>().FirstOrDefault();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly string TypeName = property.Type.ToDisplayString();
        public readonly bool SkipBuilder = property.HasAttribute<SkipBuilderAttribute>();
        public readonly string ComponentPropertyTarget = property.GetAttribute<PropertyTargetAttribute>().GetConstructorString(0, null);
        public readonly PropertyTargetType? ComponentPropertyTargetType = property.GetAttribute<PropertyTargetAttribute>().GetConstructorValue<PropertyTargetType>(1);
        public readonly string ChildComponentPropertyName = property.GetAttribute<PropertyNameAttribute>().GetConstructorString(0, null);
        public readonly bool IsTracked = property.HasAttribute<TrackedAttribute>();
        public readonly AttributeData TrackedDefaults = property.GetAttribute<TrackedDefaultsAttribute>();
        
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
        
        public string GetPropertyTarget()
        {
            string methodCall = ComponentPropertyTargetType is PropertyTargetType.Method ? "()" : "";
            return !string.IsNullOrEmpty(ComponentPropertyTarget) ? $"{ComponentPropertyTarget}{methodCall}" : null;
        }

        public string GetPropertyName()
        {
            return !string.IsNullOrEmpty(ChildComponentPropertyName) ? ChildComponentPropertyName : Name;
        }
    }
}
