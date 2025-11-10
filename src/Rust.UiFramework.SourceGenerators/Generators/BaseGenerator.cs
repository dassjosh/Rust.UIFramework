using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators;

public abstract class BaseGenerator
{
    protected SymbolCache SymbolCache;
    
    protected void InitializeCache(Compilation compilation) => SymbolCache ??= new SymbolCache(compilation);
}