using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Helpers;

public static class SymbolCache
{
    private static readonly SymbolCacheData Tracked = new("Oxide.Ext.UiFramework.Types.Tracked`1");
    private static readonly SymbolCacheData Utf8String = new("Oxide.Ext.UiFramework.Types.Utf8String");

    public static INamedTypeSymbol GetTracked(Compilation compilation) => Tracked.Get(compilation);
    public static INamedTypeSymbol GetUtf8String(Compilation compilation) => Tracked.Get(compilation);

    private sealed class SymbolCacheData(string type)
    {
        private INamedTypeSymbol _symbol;
        public INamedTypeSymbol Get(Compilation compilation) => _symbol ??= compilation.GetTypeByMetadataName(type);
    }
}