using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Rust.UiFramework.SourceGenerators.Attributes;
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
    
    public static bool ShouldBePassedByIn(this ITypeSymbol type)
    {
        if (!type.IsValueType)
        {
            return false;
        }

        return type.Name switch
        {
            "UiReference" or "UiBorderWidth" or "UiPosition" or "UiOffset" or "UiPadding" or "Vector2" => true,
            _ => false
        };
    }

    public static bool HasAttribute<T>(this ISymbol symbol)
    {
        return symbol.GetAttribute<T>() != null;
    }

    public static AttributeData GetAttribute<T>(this ISymbol symbol)
    {
        string attributeTypeName = typeof(T).Name;
        AttributeData attribute = symbol.GetAttributes().FirstOrDefault(attr => attr.AttributeClass?.Name == attributeTypeName);
        return attribute;
    }

    public static INamedTypeSymbol GetParentWithAttribute<T>(this INamedTypeSymbol symbol, Predicate<INamedTypeSymbol> filter = null)
    {
        return symbol.GetBaseTypes().FirstOrDefault(baseType => baseType.HasAttribute<T>() && (filter is null || filter(baseType)));
    }

    public static IEnumerable<INamedTypeSymbol> GetBaseTypes(this INamedTypeSymbol symbol)
    {
        INamedTypeSymbol baseType = symbol.BaseType;
        
        while (baseType is not null)
        {
            yield return baseType;
            baseType = baseType.BaseType;
        }
    }
    
    public static IEnumerable<IPropertySymbol> GetProperties(this INamedTypeSymbol interfaceType, bool includeProperties = true)
    {
        return GetPropertiesInternal(interfaceType, includeProperties, []);
    }
    
    private static IEnumerable<IPropertySymbol> GetPropertiesInternal(this INamedTypeSymbol interfaceType, bool includeProperties, HashSet<string> included)
    {
        if (interfaceType == null || !included.Add(interfaceType.ToString()))
        {
            yield break;
        }
        
        if (includeProperties)
        {
            foreach (IPropertySymbol property in interfaceType.GetMembers().OfType<IPropertySymbol>().Where(p => !p.HasAttribute<SkipPropertyAttribute>()))
            {
                yield return property;
            }
        }

        foreach (INamedTypeSymbol baseInterface in interfaceType.AllInterfaces.Distinct(SymbolEqualityComparer.Default).OfType<INamedTypeSymbol>())
        {
            foreach (IPropertySymbol symbol in baseInterface.GetPropertiesInternal(baseInterface.HasAttribute<IncludeInParentAttribute>(), included))
            {
                yield return symbol;
            }
        }
    }

    public static IEnumerable<IFieldSymbol> GetEnumValues(this INamedTypeSymbol @enum)
    {
        return @enum.GetMembers().OfType<IFieldSymbol>().Where(f => f.IsConst);
    }
    
    public static string GetTrackableInterface(this ITypeSymbol symbol)
    {
        return $"I{symbol.Name}Trackable";
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