using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Logging;

namespace Rust.UiFramework.SourceGenerators.Helpers;

public static class GeneratorHelpers
{
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

    public static INamedTypeSymbol GetTrackedType(this ImmutableArray<ClassDeclarationSyntax> classes, Compilation compilation)
    {
        ClassDeclarationSyntax tracked = classes.FirstOrDefault(c => c.Identifier.Text == "Tracked" && c.TypeParameterList is not null && c.TypeParameterList.Parameters.Count == 1);
        SemanticModel model = compilation.GetSemanticModel(tracked.SyntaxTree);
        return model.GetDeclaredSymbol(tracked) as INamedTypeSymbol;
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
        if (value is null)
        {
            return defaultValue;
        }

        return value.ToString();
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