using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Rust.UiFramework.SourceGenerators.Extensions;

namespace Rust.UiFramework.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class PoolResetAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "UIFPOOL01";
    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Poolable object member not reset",
        "Member '{0}' is not reset or disposed in any lifecycle method (EnterPool, LeavePool, OnReset)",
        "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSymbolStartAction(startContext =>
        {
            INamedTypeSymbol namedType = (INamedTypeSymbol)startContext.Symbol;
            if (namedType.TypeKind != TypeKind.Class) return;
            if (!InheritsFrom(namedType, "BasePoolable")) return;

            ImmutableHashSet<ISymbol> membersToReset = namedType.GetMembers()
                .Where(MemberFilter)
                .ToImmutableHashSet(SymbolEqualityComparer.Default);

            if (membersToReset.IsEmpty) return;

            ConcurrentBag<ISymbol> handledMembers = [];
            
            startContext.RegisterOperationBlockAction(blockContext =>
            {
                if (blockContext.OwningSymbol is not IMethodSymbol method) return;
                if (!IsPoolingMethod(method.Name)) return;

                foreach (IOperation operation in blockContext.OperationBlocks)
                {
                    foreach (IOperation desc in operation.Descendants())
                    {
                        if (desc is ISimpleAssignmentOperation assignment)
                        {
                            ISymbol sym = UnwrapMember(assignment.Target);
                            if (sym != null) handledMembers.Add(sym);
                        }
                        else if (desc is IInvocationOperation invocation && IsCleanupMethod(invocation.TargetMethod.Name))
                        {
                            // Case A: Instance method (e.g., Item.Dispose())
                            ISymbol sym = UnwrapMember(invocation.Instance);
                    
                            // Case B: Extension method (e.g., Item.TryReturnToPool() where Item is 1st arg)
                            if (sym == null && invocation.Arguments.Length > 0)
                            {
                                sym = UnwrapMember(invocation.Arguments[0].Value);
                            }

                            if (sym != null)
                            {
                                handledMembers.Add(sym);
                            }
                        }
                    }
                }
            });

            startContext.RegisterSymbolEndAction(endContext =>
            {
                ImmutableHashSet<ISymbol> finalHandled = handledMembers.ToImmutableHashSet(SymbolEqualityComparer.Default);
                foreach (ISymbol member in membersToReset)
                {
                    if (!finalHandled.Contains(member))
                    {
                        endContext.ReportDiagnostic(Diagnostic.Create(Rule, member.Locations[0], member.Name));
                    }
                }
            });

        }, SymbolKind.NamedType);
    }

    private static bool MemberFilter(ISymbol symbol)
    {
        if (symbol.IsImplicitlyDeclared || symbol.IsStatic) return false;
        if (symbol.HasAttribute<ObsoleteAttribute>()) return false;

        if (symbol is IFieldSymbol field)
        {
            return !field.IsReadOnly && !IgnoredType(field.Type);
        }
        
        if (symbol is IPropertySymbol prop)
        {
            if (prop is not { IsReadOnly: false, SetMethod: not null, IsIndexer: false, IsPartialDefinition: false, IsOverride: false } || IgnoredType(prop.Type))
            {
                return false;
            }

            // Check if it's an Auto-Property. 
            // If it has a body calling methods or accessing fields, it's not a simple state container.
            return IsSimpleAutoProperty(prop);
        }

        return false;
    }

    private static bool IsSimpleAutoProperty(IPropertySymbol prop)
    {
        // Check all syntax references for the property
        foreach (SyntaxReference syntaxRef in prop.DeclaringSyntaxReferences)
        {
            SyntaxNode syntax = syntaxRef.GetSyntax();
            
            // If the property uses an expression body (prop => _otherField)
            if (syntax is PropertyDeclarationSyntax { ExpressionBody: not null }) return false;

            // Check accessors
            if (syntax is PropertyDeclarationSyntax propertyDeclaration)
            {
                if (propertyDeclaration.AccessorList == null) continue;

                foreach (AccessorDeclarationSyntax accessor in propertyDeclaration.AccessorList.Accessors)
                {
                    // If any accessor has a body { ... } or expression body => ...
                    // we check if it contains method calls or field references.
                    if (accessor.Body != null || accessor.ExpressionBody != null)
                    {
                        IEnumerable<SyntaxNode> nodes = accessor.DescendantNodes();
                        foreach (SyntaxNode node in nodes)
                        {
                            // Ignore if it calls a method or accesses a different member
                            if (node is InvocationExpressionSyntax or MemberAccessExpressionSyntax)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    
    private static bool IsPoolingMethod(string name) => name is "EnterPool" or "LeavePool" or "OnReset" or "Reset";
    
    private static ISymbol UnwrapMember(IOperation op)
    {
        while (op is IConversionOperation conversion)
        {
            op = conversion.Operand;
        }

        return op switch
        {
            IFieldReferenceOperation field => field.Field,
            IPropertyReferenceOperation prop => prop.Property,
            IArgumentOperation arg => UnwrapMember(arg.Value), 
            _ => null
        };
    }

    private static bool IsCleanupMethod(string name) => name.Contains("Dispose") || name.Contains("Free") || name.Contains("TryReturnToPool");
    private static bool IgnoredType(ITypeSymbol symbol) => symbol.HasInterface("IComponent") || InheritsFrom(symbol, "BaseUiComponent") || InheritsFrom(symbol, "BaseUiControl");

    private static bool InheritsFrom(ITypeSymbol symbol, string baseName)
    {
        INamedTypeSymbol current = symbol.BaseType;
        while (current != null)
        {
            if (current.Name == baseName) return true;
            current = current.BaseType;
        }
        return false;
    }
}