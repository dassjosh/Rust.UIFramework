using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Rust.UiFramework.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PoolLifecycleBaseCallAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "UIFPOOL02";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Pool lifecycle method must call base implementation",
        "Method '{0}' must call '{1}'",
        "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(compilationContext =>
        {
            INamedTypeSymbol basePoolable = compilationContext.Compilation.GetTypeByMetadataName("Oxide.Ext.UiFramework.Pooling.BasePoolable");
            if (basePoolable == null)
            {
                return;
            }

            compilationContext.RegisterSymbolStartAction(startContext =>
            {
                INamedTypeSymbol type = (INamedTypeSymbol)startContext.Symbol;

                if (type.TypeKind != TypeKind.Class)
                {
                    return;
                }

                if (!InheritsFrom(type, basePoolable))
                {
                    return;
                }

                startContext.RegisterOperationBlockAction(blockContext =>
                {
                    if (blockContext.OwningSymbol is not IMethodSymbol method)
                    {
                        return;
                    }

                    if (!method.IsOverride)
                    {
                        return;
                    }

                    if (method.Name is not ("EnterPool" or "LeavePool"))
                    {
                        return;
                    }

                    if (SymbolEqualityComparer.Default.Equals(type.BaseType, basePoolable))
                    {
                        return;
                    }

                    IMethodSymbol overriddenMethod = method.OverriddenMethod;
                    if (overriddenMethod == null)
                    {
                        return;
                    }

                    bool foundBaseCall = false;

                    foreach (IOperation operationBlock in blockContext.OperationBlocks)
                    {
                        foreach (IInvocationOperation invocation in operationBlock.Descendants().OfType<IInvocationOperation>())
                        {
                            if (SymbolEqualityComparer.Default.Equals(invocation.TargetMethod, overriddenMethod))
                            {
                                foundBaseCall = true;
                                break;
                            }
                        }

                        if (foundBaseCall)
                        {
                            break;
                        }
                    }

                    if (!foundBaseCall)
                    {
                        blockContext.ReportDiagnostic(Diagnostic.Create(Rule, method.Locations[0], method.Name, $"base.{method.Name}()"));
                    }
                });
            }, SymbolKind.NamedType);
        });
    }

    private static bool InheritsFrom(ITypeSymbol symbol, INamedTypeSymbol basePoolable)
    {
        for (INamedTypeSymbol current = symbol.BaseType; current != null; current = current.BaseType)
        {
            if (SymbolEqualityComparer.Default.Equals(current, basePoolable))
            {
                return true;
            }
        }

        return false;
    }
}