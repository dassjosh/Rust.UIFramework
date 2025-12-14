using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class IPropertySymbolExt
{
    public static bool IsInterfaceImplementation(this IPropertySymbol property)
    {
        INamedTypeSymbol containingType = property.ContainingType;

        foreach (ISymbol member in containingType.AllInterfaces.SelectMany(@interface => @interface.GetMembers()))
        {
            if (member is IPropertySymbol interfaceProperty)
            {
                ISymbol implementation = containingType.FindImplementationForInterfaceMember(interfaceProperty);
                if (SymbolEqualityComparer.Default.Equals(implementation, property))
                {
                    return true;
                }
            }
        }

        return false;
    }
}