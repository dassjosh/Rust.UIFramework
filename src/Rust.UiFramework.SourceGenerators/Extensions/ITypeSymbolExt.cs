using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class ITypeSymbolExt
{
    extension(ITypeSymbol symbol)
    {
        public bool HasInterface(ISymbol @interface) => symbol.AllInterfaces.Any(i => i.SymbolEquals(@interface));
        public bool HasInterface(string name) => symbol.AllInterfaces.Any(i => i.Name == name);
        
        public bool ShouldBePassedByIn()
        {
            if (!symbol.IsValueType || symbol.TypeKind != TypeKind.Struct || !symbol.IsReadOnly)
            {
                return false;
            }

            const int useInMinSize = 16;
            return symbol.CalculateObjectSize() >= useInMinSize;
        }

        public int CalculateObjectSize()
        {
            const int x64Alignment = 8;
            int offset = 0;
            int maxAlignment = 1;

            foreach (IFieldSymbol field in symbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (field.IsConst || field.IsStatic)
                {
                    continue;
                }
                
                int size = field.Type.GetSpecialTypeSize();
                int alignment = Math.Min(size, x64Alignment); // x64 max alignment
                maxAlignment = Math.Max(maxAlignment, alignment);

                // Align offset
                offset = (offset + alignment - 1) / alignment * alignment;
                offset += size;
            }

            // Round up to struct alignment
            offset = (offset + maxAlignment - 1) / maxAlignment * maxAlignment;
            return offset;
        }

        public int GetSpecialTypeSize()
        {
            return symbol.SpecialType switch
            {
                SpecialType.System_Byte or SpecialType.System_SByte => 1,
                SpecialType.System_Int16 or SpecialType.System_UInt16 => 2,
                SpecialType.System_Int32 or SpecialType.System_UInt32 or SpecialType.System_Single => 4,
                SpecialType.System_Int64 or SpecialType.System_UInt64 or SpecialType.System_Double => 8,
                SpecialType.System_Boolean => 1,
                _ => 8 // assume reference or unknown type
            };
        }
        
        public INamedTypeSymbol GetParentWithAttribute<T>(Predicate<INamedTypeSymbol> filter = null)
        {
            return symbol.GetBaseTypes().FirstOrDefault(baseType => baseType.HasAttribute<T>() && (filter is null || filter(baseType)));
        }

        public IEnumerable<INamedTypeSymbol> GetBaseTypes()
        {
            INamedTypeSymbol baseType = symbol.BaseType;
        
            while (baseType is not null)
            {
                yield return baseType;
                baseType = baseType.BaseType;
            }
        }

        public IEnumerable<IPropertySymbol> GetProperties()
        {
            if (symbol == null)
            {
                yield break;
            }
        
            foreach (IPropertySymbol property in symbol.GetMembers().OfType<IPropertySymbol>())
            {
                yield return property;
            }
        }
        
        public IEnumerable<IPropertySymbol> GetProperties(Func<IPropertySymbol, bool> predicate) => symbol.GetProperties().Where(predicate);

        public IEnumerable<IFieldSymbol> GetFields()
        {
            if (symbol == null)
            {
                yield break;
            }
        
            foreach (IFieldSymbol field in symbol.GetMembers().OfType<IFieldSymbol>())
            {
                yield return field;
            }
        }
        
        public IEnumerable<IFieldSymbol> GetFields(Func<IFieldSymbol, bool> predicate) => symbol.GetFields().Where(predicate);

        public IEnumerable<IFieldSymbol> GetEnumValues()
        {
            return symbol.GetMembers().OfType<IFieldSymbol>().Where(f => f.IsConst);
        }
        
        public string AsGeneric(string generic)
        {
            return $"{symbol.ContainingNamespace}.{symbol.Name}<{generic}>";
        }
        
        public string AsGeneric(IEnumerable<string> generics)
        {
            string[] genArray = generics.ToArray();
            return genArray.Length == 0 ? symbol.ToString() : $"{symbol.ContainingNamespace}.{symbol.Name}<{string.Join(", ", genArray)}>";
        }
    }
}