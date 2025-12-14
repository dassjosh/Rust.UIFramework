using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Logging;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class IncrementalGeneratorInitializationContextExt
{
    public static void Register<T>(this IncrementalGeneratorInitializationContext context, Action<SourceProductionContext, (Compilation Left, ImmutableArray<T> Right)> callback) where T : SyntaxNode
    {
        IncrementalValuesProvider<T> interfaceDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s is T,
                transform: static (ctx, _) => (T)ctx.Node)
            .Where(m => m is not null);

        IncrementalValueProvider<(Compilation Left, ImmutableArray<T> Right)> compilationAndClasses = context.CompilationProvider.Combine(interfaceDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, callback);
    }

    public static void Register<TNode, TAttribute>(this IncrementalGeneratorInitializationContext context, 
        Action<SourceProductionContext, Compilation, TNode, INamedTypeSymbol, AttributeData> callback) 
        where TNode : SyntaxNode
        where TAttribute : Attribute
    {
        context.Register<TNode>((spc, source) =>
        {
            (Compilation compilation, ImmutableArray<TNode> nodes) = source;
            
            foreach (TNode node in nodes)
            {
                SemanticModel model = compilation.GetSemanticModel(node.SyntaxTree);
#pragma warning disable RS1039
                INamedTypeSymbol classSymbol = model.GetDeclaredSymbol(node) as INamedTypeSymbol;
#pragma warning restore RS1039
                
                AttributeData attribute = classSymbol?.GetAttribute<TAttribute>();
                if (attribute is not null)
                {
                    try
                    {
                        callback(spc, compilation, node, classSymbol, attribute);
                    }
                    catch (Exception ex)
                    {
                        spc.ReportDiagnostic(Diagnostic.Create(ErrorDiagnostic, Location.None, $"{ex.StackTrace}"));
                    }
                }
            }
        });
    }
    
    public static void Register<T>(this IncrementalGeneratorInitializationContext context, Predicate<INamedTypeSymbol> predicate,
        Action<SourceProductionContext, Compilation, T, INamedTypeSymbol> callback) 
        where T : SyntaxNode
    {
        context.Register<T>((spc, source) =>
        {
            (Compilation compilation, ImmutableArray<T> nodes) = source;
            
            foreach (T node in nodes)
            {
                SemanticModel model = compilation.GetSemanticModel(node.SyntaxTree);
#pragma warning disable RS1039
                INamedTypeSymbol symbol = model.GetDeclaredSymbol(node) as INamedTypeSymbol;
#pragma warning restore RS1039

                LoggingHelper.Log($"{symbol.Name} - {symbol.ContainingNamespace}");
                if (predicate.Invoke(symbol))
                {
                    try
                    {
                        callback(spc, compilation, node, symbol);
                    }
                    catch (Exception ex)
                    {
                        spc.ReportDiagnostic(Diagnostic.Create(ErrorDiagnostic, Location.None, $"{ex.StackTrace}"));
                    }
                }
            }
        });
        
    }
    
    private static readonly DiagnosticDescriptor ErrorDiagnostic =
        new(
            id: "UIS001",
            title: "Source Generator Exception",
            messageFormat: "An error occurred in the source generator: {0}",
            category: "SourceGenerator",
            DiagnosticSeverity.Error, true);
}