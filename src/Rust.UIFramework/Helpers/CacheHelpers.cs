using System;
using System.Reflection;
using Oxide.Ext.UiFramework.Cache;

namespace Oxide.Ext.UiFramework.Helpers;

internal static class CacheHelpers
{
    internal static string[] ExtractCache(Type constantsType, Type enumType)
    {
        string[] values = new string[Enum.GetValues(enumType).Length];
        
        Type stringType = typeof(string);
        foreach (FieldInfo field in constantsType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
        {
            if (field.IsLiteral && !field.IsInitOnly && field.FieldType == stringType)
            {
                string enumName = field.Name;
                string value = (string)field.GetRawConstantValue();
                if(Enum.TryParse(enumType, enumName, out object result))
                {
                    values[(byte)result] = value;
                }
                else
                {
                    OxideLibrary.LogWarning($"Failed to parse {constantsType.Name} enum: {enumName} Value: {value}");
                }
            }
        }

        return values;
    }
}