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
public class ComponentAnimationsGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateComponentAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            if (!classSymbol.IsAbstract)
            {
                SymbolCache.Initialize(compilation);
                ComponentGeneratorData data = new(classSymbol);
                if (data.IsSubComponent)
                {
                    spc.AddSource($"{classSymbol.Name}AnimationExt.g.cs", GenerateSubComponents(classSymbol, data));
                } 
                else if (data.IsCoreComponent && data.ChildComponents.Length != 0)
                {
                    spc.AddSource($"{classSymbol.Name}AnimationExt.g.cs", GenerateCoreComponents(classSymbol, data));
                }
            }
        });
    }
    
    private string GenerateSubComponents(INamedTypeSymbol classSymbol, ComponentGeneratorData genData)
    {
        return new CodeBuilder()
            .Usings([])
            .Namespace("Oxide.Ext.UiFramework.Animation")
                .Add(t => t.Public().Static().Partial().Class().Name($"{classSymbol.Name}AnimationExt")
                .Extension(e => e.AddGeneric("T", out string genericT).Where(w => w.Type(genericT).Constrain(SymbolCache.Instance.UiElements.BaseUiComponent.Symbol))
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.AsGeneric(SymbolCache.Instance.Animations.IElementAnimation.Symbol.AsGeneric(genericT)))
                        .Name("animation"))
                
                    .Method(m => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(classSymbol)))
                        .Name($"Animate{classSymbol.Name}")
                        .Body($"return animation.AnimateComponent(static a => a.Component.GetOrAddSubComponent<{classSymbol}>());"))
                )
                
                .Extension(e => e.AddGeneric("T", out string genericT).Where(w => w.Type(genericT).Constrain(SymbolCache.Instance.Components.BaseComponent.Symbol).Constrain(SymbolCache.Instance.Interfaces.ICoreComponent.Symbol))
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.AsGeneric(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.AsGeneric(genericT)))
                        .Name("animation"))
                
                    .Method(m => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(classSymbol)))
                        .Name($"Animate{classSymbol.Name}")
                        .Body($"return animation.AnimateComponent(static a => a.GetOrAddSubComponent<{classSymbol}>());"))
                ))
            .Build();
    }
    
    private string GenerateCoreComponents(INamedTypeSymbol classSymbol, ComponentGeneratorData genData)
    {
        return new CodeBuilder()
            .Usings([])
            .Namespace("Oxide.Ext.UiFramework.Animation")
            .Add(t => t.Public().Static().Partial().Class().Name($"{classSymbol.Name}AnimationExt")
                .Extension(e => e
                    .AddParameter(p => p.Type(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(classSymbol)))
                        .Name("animation"))
                
                    .Methods(genData.ChildComponents, (p, m) => m.Public().Returns(SymbolCache.Instance.Animations.AnimationRef.Symbol.Construct(SymbolCache.Instance.Animations.IComponentAnimation.Symbol.Construct(p.Type)))
                        .Name($"Animate{p.Name}")
                        .Body($"return animation.AnimateComponent(static a => a.{p.Name});"))
                ))
            .Build();
    }
    
    private sealed class ComponentGeneratorData
    {
        public readonly INamedTypeSymbol Symbol;
        public readonly PropertyData[] ChildComponents;
        public bool IsSubComponent => Symbol.HasInterface("ISubComponent") && !Symbol.HasInterface("ICoreComponent");
        public bool IsCoreComponent => Symbol.HasInterface("ICoreComponent");
        public bool IsChildComponent => Symbol.HasInterface("IChildComponent");

        public ComponentGeneratorData(INamedTypeSymbol symbol)
        {
            Symbol = symbol;
            ChildComponents = symbol.GetProperties(p => p.Type.HasInterface("IChildComponent")).Select(p => new PropertyData(p)).ToArray();
        }
    }
    
    private sealed class PropertyData(IPropertySymbol property)
    {
        public readonly IPropertySymbol Symbol = property;
        public readonly string Name = property.Name;
        public readonly ITypeSymbol Type = property.Type;
    }
}