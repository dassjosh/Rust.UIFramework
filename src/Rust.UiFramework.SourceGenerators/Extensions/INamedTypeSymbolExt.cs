using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class INamedTypeSymbolExt
{
    public static string AsGeneric(this INamedTypeSymbol symbol, IEnumerable<string> generics)
    {
        string[] genArray = generics.ToArray();
        return genArray.Length == 0 ? symbol.ToString() : $"{symbol.ContainingNamespace}.{symbol.Name}<{string.Join(", ", genArray)}>";
    }
}