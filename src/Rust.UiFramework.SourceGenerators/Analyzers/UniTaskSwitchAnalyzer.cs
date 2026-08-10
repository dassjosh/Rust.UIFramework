using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Rust.UiFramework.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UniTaskSwitchAnalyzer : DiagnosticAnalyzer
{
    private const string ThreadPoolRuleId = "UIUT0001";
    private const string MainThreadRuleId = "UIUT0002";

    private static readonly DiagnosticDescriptor ThreadPoolRule =
        new(ThreadPoolRuleId,
            "UniTask.SwitchToThreadPool is forbidden",
            "Do not call UniTask.SwitchToThreadPool() directly. Use UniTaskExt.SwitchToThreadPool() instead.",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor MainThreadRule =
        new(MainThreadRuleId,
            "UniTask.SwitchToMainThread is forbidden",
            "Do not call UniTask.SwitchToMainThread() directly. Use UniTaskExt.SwitchToMainThread() instead.",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [ThreadPoolRule, MainThreadRule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeInvocation, Microsoft.CodeAnalysis.CSharp.SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        InvocationExpressionSyntax invocation = (InvocationExpressionSyntax)context.Node;

        IMethodSymbol symbol = context.SemanticModel.GetSymbolInfo(invocation).Symbol as IMethodSymbol;
        if (symbol?.Name is not ("SwitchToThreadPool" or "SwitchToMainThread"))
        {
            return;
        }

        INamedTypeSymbol containingType = symbol.ContainingType;
        if (containingType?.Name != "UniTask")
        {
            return;
        }

        // Ignore calls inside UniTaskExt
        INamedTypeSymbol enclosingType = context.ContainingSymbol?.ContainingType;
        if (enclosingType?.Name == "UniTaskExt")
        {
            return;
        }

        if (symbol.Name == "SwitchToThreadPool")
        {
            context.ReportDiagnostic(Diagnostic.Create(ThreadPoolRule, invocation.GetLocation()));
        }
        else
        {
            context.ReportDiagnostic(Diagnostic.Create(MainThreadRule, invocation.GetLocation()));
        }
    }
}