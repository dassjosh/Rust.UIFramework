using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Logging;

namespace Rust.UiFramework.SourceGenerators.Helpers;

public static class GeneratorHelpers
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
                    callback(spc, compilation, node, classSymbol, attribute);
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
                    callback(spc, compilation, node, symbol);
                }
            }
        });
    }
    
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        
        return char.ToLower(value[0]) + value[1..];
    }

    public static string ToPrivateField(this string value) => $"_{value.ToCamelCase()}";
    
    extension(ITypeSymbol type)
    {
        public bool ShouldBePassedByIn()
        {
            if (!type.IsValueType || type.TypeKind != TypeKind.Struct || !type.IsReadOnly)
            {
                return false;
            }

            const int useInMinSize = 16;
            return type.CalculateObjectSize() >= useInMinSize;
        }

        public int CalculateObjectSize()
        {
            const int x64Alignment = 8;
            int offset = 0;
            int maxAlignment = 1;

            foreach (IFieldSymbol field in type.GetMembers().OfType<IFieldSymbol>())
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
            return type.SpecialType switch
            {
                SpecialType.System_Byte or SpecialType.System_SByte => 1,
                SpecialType.System_Int16 or SpecialType.System_UInt16 => 2,
                SpecialType.System_Int32 or SpecialType.System_UInt32 or SpecialType.System_Single => 4,
                SpecialType.System_Int64 or SpecialType.System_UInt64 or SpecialType.System_Double => 8,
                SpecialType.System_Boolean => 1,
                _ => 8 // assume reference or unknown type
            };
        }
    }

    extension(ISymbol symbol)
    {
        public bool HasAttribute<T>()
        {
            return symbol.GetAttribute<T>() != null;
        }

        public AttributeData GetAttribute<T>()
        {
            string attributeTypeName = typeof(T).Name;
            AttributeData attribute = symbol.GetAttributes().FirstOrDefault(attr => attr.AttributeClass?.Name == attributeTypeName);
            return attribute;
        }
    }

    extension(INamedTypeSymbol symbol)
    {
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

        public IEnumerable<IPropertySymbol> GetProperties(bool includeProperties = true)
        {
            return GetPropertiesInternal(symbol, includeProperties, []);
        }

        private IEnumerable<IPropertySymbol> GetPropertiesInternal(bool includeProperties, HashSet<string> included)
        {
            if (symbol == null || !included.Add(symbol.ToString()))
            {
                yield break;
            }
        
            if (includeProperties)
            {
                foreach (IPropertySymbol property in symbol.GetMembers().OfType<IPropertySymbol>())
                {
                    yield return property;
                }
            }
        }

        public IEnumerable<IFieldSymbol> GetEnumValues()
        {
            return symbol.GetMembers().OfType<IFieldSymbol>().Where(f => f.IsConst);
        }
    }

    public static bool IsInterfaceImplementation(this IPropertySymbol property)
    {
        INamedTypeSymbol containingType = property.ContainingType;

        foreach (INamedTypeSymbol @interface in containingType.AllInterfaces)
        {
            foreach (ISymbol member in @interface.GetMembers())
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
        }

        return false;
    }

    public static string GetTrackableInterface(this ITypeSymbol symbol)
    {
        return $"{symbol.GetInterface()}Trackable";
    }

    public static string GetInterface(this ITypeSymbol symbol)
    {
        return $"I{symbol.Name}";
    }

    public static T? GetConstructorValue<T>(this AttributeData attribute, int index) where T : struct, Enum
    {
        object value = attribute.GetConstructorValue(index);
        if (value is null)
        {
            return default;
        }

        return (T)Enum.ToObject(typeof(T), value);
    }

    public static string GetConstructorString(this AttributeData attribute, int index, string defaultValue = "")
    {
        object value = attribute.GetConstructorValue(index);
        return value is null ? defaultValue : value.ToString();
    }
    
    private static object GetConstructorValue(this AttributeData attribute, int index)
    {
        if (attribute == null || attribute.ConstructorArguments.IsEmpty || attribute.ConstructorArguments.Length < index || attribute.ConstructorArguments[index].IsNull)
        {
            return default;
        }

        return attribute.ConstructorArguments[index].Value!;
    }
}