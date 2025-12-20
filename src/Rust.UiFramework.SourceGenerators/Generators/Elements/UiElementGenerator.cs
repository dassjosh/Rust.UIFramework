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

namespace Rust.UiFramework.SourceGenerators.Generators.Elements;

[Generator]
public class UiElementGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateUiElementAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            GeneratorData data = new(classSymbol);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateElement(classSymbol, data));
        });
    }
    
    private string GenerateElement(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Interfaces"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t => t.Public().Partial().Class().Name(classSymbol.Name)
                .Implements(genData.Symbol.GetInterface())
                .If(GeneratorFlags.AddTrackableInterface, t => t.Implements(genData.Symbol.GetTrackableInterface())).EndIf()
                
                //Internal Tracked Fields
                .Fields(genData.TrackedProperties, (data, field) => field.Internal().Readonly().Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(data.Type)).Name(data.Symbol.TrackedFieldName()).New(data.GetPropertyDefaults()))
               
                //Public Properties Setting Component Fields
                .Properties(genData.PartialProperties, (data, property) => property.Public().Partial().Type(data.TypeName).Name(data.Name)
                    .If(data.IsTracked, p => p.Get($"{data.Symbol.TrackedFieldName()}.Value").Set($"{data.Symbol.TrackedFieldName()}.Value = value").AggressiveInlining())
                    .ElseIf(data.ComponentPropertyTargetType is PropertyTargetType.Self, p => p.Get().Set())
                    .ElseIf(data.HasPropertyTarget(), p => p.Get($"{data.GetPropertyTarget()}.{data.GetPropertyName()}").Set($"{data.GetPropertyTarget()}.{data.GetPropertyName()} = value"))
                    .Else(p => p.Get($"{genData.ComponentFieldName}.{data.GetPropertyName()}").Set($"{genData.ComponentFieldName}.{data.GetPropertyName()} = value").AggressiveInlining())
                    .EndIf())
               
                .If(GeneratorFlags.AddTrackableInterface, _ => t
                    //Public Tracked Explicit Interface Implementation Properties
                    .Properties(genData.PartialProperties, data => data.IsTracked, (data, property) => property.Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(data.Type))
                        .Name($"{genData.Symbol.GetTrackableInterface()}.{data.Name}").Get(data.Symbol.TrackedFieldName()))
                    
                    //AsTrackable for Component and Element
                    .If(!genData.Symbol.IsAbstract, t => t.Property(p => 
                        p.Type(genData.ComponentField.Type.GetTrackableInterface())
                            .Name($"{genData.Symbol.GetTrackableInterface()}.{genData.ComponentField.Name}")
                            .Get($"{genData.ComponentFieldName}.AsTrackable()"))
                        .Method(m => m.Internal().Name("AsTrackable").Returns(genData.Symbol.GetTrackableInterface()).Body("return this;")
                            .AggressiveInlining()))
                    .EndIf()
                ).EndIf()
            )
            .Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol Symbol;
        public readonly string ComponentFieldName;
        public readonly PropertyData[] PartialProperties;
        public readonly PropertyData[] TrackedProperties;
        public readonly IFieldSymbol ComponentField;

        public GeneratorData(INamedTypeSymbol symbol)
        {
            Symbol = symbol;
            PartialProperties = symbol.GetProperties(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            TrackedProperties = symbol.GetProperties(p => p.HasAttribute<TrackedAttribute>()).Select(p => new PropertyData(p)).ToArray();
            ComponentField = symbol.GetFields().FirstOrDefault(f => f.IsReadOnly && f.Type.HasInterface("IComponent"));
            ComponentFieldName = ComponentField?.Name;
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly IPropertySymbol Symbol = property;
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
        public readonly string TypeName = property.Type.ToDisplayString();
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
        
        public bool HasPropertyTarget() => !string.IsNullOrEmpty(ComponentPropertyTarget);
        
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
