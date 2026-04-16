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
                ElementsGeneratorData data = new(classSymbol);
                spc.AddSource($"{classSymbol.Name}AnimationExt.g.cs", GenerateElement(classSymbol, data));
            }
        });
        
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            ComponentGeneratorData data = new(classSymbol);
            spc.AddSource($"{classSymbol.Name}AnimationExt.g.cs", GenerateSubComponent(classSymbol, data));
        });
    }
    
    private string GenerateElement(INamedTypeSymbol classSymbol, ElementsGeneratorData genData)
    {
        return new CodeBuilder()
            .Usings([])
            .Namespace("Oxide.Ext.UiFramework.Animation")
                .Add(t => t.Public().Static().Class().Name($"{classSymbol.Name}AnimationExt")
                .Extension(e => e
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IElementAnimation.Symbol.Construct(classSymbol)))
                        .Name("animation"))
                
                    .Method(m => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(genData.ComponentField.Type)))
                            .Name($"Animate{genData.ComponentField.Name}Component")
                            .Body($"return animation.AnimateComponent(static a => a.{genData.ComponentField.Name});"))
                    
                    .Methods(genData.PartialProperties, (p, m) => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IFieldAnimation.Symbol.Construct(p.Type)))
                        .Name($"Animate{p.Name}")
                        .Body($"return animation.AnimateField(static a => a.{genData.ComponentField.Name}.{p.Symbol.TrackedFieldName()});"))
                ))
            .Build();
    }
    
    private string GenerateSubComponent(INamedTypeSymbol classSymbol, ComponentGeneratorData genData)
    {
        if (classSymbol.IsAbstract)
        {
            return new CodeBuilder()
                .Usings([])
                .Namespace("Oxide.Ext.UiFramework.Animation")
                .Add(t => t.Public().Static().Partial().Class().Name($"{classSymbol.Name}AnimationExt")
                    .Extension(e => e.AddGeneric("T", out string genericT).Where(w => w.Type(genericT).Constrain(classSymbol))
                        .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.AsGeneric(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.AsGeneric(genericT)))
                            .Name("animation"))
                
                        .Methods(genData.PartialProperties, (p, m) => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IFieldAnimation.Symbol.Construct(p.Type))).Name($"Animate{p.Name}")
                            .Body($"return animation.AnimateField(static a => a.{p.Symbol.TrackedFieldName()});"))
                    ))
                .Build();
        }
        
        return new CodeBuilder()
            .Usings([])
            .Namespace("Oxide.Ext.UiFramework.Animation")
            .Add(t => t.Public().Static().Partial().Class().Name($"{classSymbol.Name}AnimationExt")
                .Extension(e => e
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(classSymbol)))
                        .Name("animation"))
                
                    .Methods(genData.PartialProperties, (p, m) => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IFieldAnimation.Symbol.Construct(p.Type))).Name($"Animate{p.Name}")
                        .Body($"return animation.AnimateField(static a => a.{p.Symbol.TrackedFieldName()});"))
                ))
            .Build();
    }

    private sealed class ElementsGeneratorData
    {
        public readonly PropertyData[] PartialProperties;
        public readonly IFieldSymbol ComponentField;

        public ElementsGeneratorData(INamedTypeSymbol symbol)
        {
            ComponentField = symbol.GetFields().FirstOrDefault(f => f.IsReadOnly && f.Type.HasInterface("IComponent"));
            PartialProperties = ComponentField!.Type.GetProperties(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
        }
    }
    
    private sealed class ComponentGeneratorData
    {
        public readonly PropertyData[] PartialProperties;

        public ComponentGeneratorData(INamedTypeSymbol symbol)
        {
            PartialProperties = symbol.GetProperties(p => p.IsPartialDefinition).Select(p => new PropertyData(p)).ToArray();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly IPropertySymbol Symbol = property;
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
    }
}