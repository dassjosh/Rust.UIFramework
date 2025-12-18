using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Animations;

[Generator]
public class FieldAnimationsGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateUiElementAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            if (!classSymbol.IsAbstract)
            {
                SymbolCache.Initialize(compilation);
                GeneratorData data = new(classSymbol);
                spc.AddSource($"{classSymbol.Name}.g.cs", GenerateElement(classSymbol, data));
            }

        });
    }
    
    private string GenerateElement(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Usings([])
            .Namespace("Oxide.Ext.UiFramework.Animation")
            .Add(t => t.Public().Static().Class().Name($"ElementAnimation{classSymbol.ToExtensionClass()}")
                .Extension(e => e
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IElementAnimation.Symbol.Construct(classSymbol)))
                        .Name("animation"))
                
                    .Methods(genData.PartialProperties, (p, m) => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IFieldAnimation.Symbol.Construct(p.Type))).Name($"Animate{p.Name}")
                        .Body($"return animation.AnimateField(static a => a.AsTrackable().{genData.ComponentField.Name}.{p.Name});"))
                ))
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
            ComponentField = symbol.GetFields().FirstOrDefault(f => f.IsReadOnly && f.Type.HasInterface("IComponent"));
            PartialProperties = ComponentField.Type.GetProperties(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
            TrackedProperties = symbol.GetProperties(p => p.HasAttribute<TrackedAttribute>()).Select(p => new PropertyData(p)).ToArray();
            
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