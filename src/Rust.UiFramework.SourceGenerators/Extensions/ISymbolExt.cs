using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class ISymbolExt
{
    extension(ISymbol symbol)
    {
        public string ToPrivateField() => $"_{symbol.Name.ToCamelCase()}";
        public string ToExtensionClass() => $"{symbol.Name}Ext";
        public string GetTrackableInterface() => $"{symbol.GetInterface()}Trackable";
        public string GetInterface() => $"I{symbol.Name}";
        
        public bool HasAttribute<T>() => symbol.GetAttribute<T>() != null;

        public AttributeData GetAttribute<T>()
        {
            string attributeTypeName = typeof(T).Name;
            AttributeData attribute = symbol.GetAttributes().FirstOrDefault(attr => attr.AttributeClass?.Name == attributeTypeName);
            return attribute;
        }

        public bool SymbolEquals(ISymbol other) => SymbolEqualityComparer.Default.Equals(symbol, other);
    }
}