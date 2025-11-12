using System.Reflection;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using Random = System.Random;

namespace Rust.UiFramework.UnitTests.Global.Generators;

public static class Permutations
{
    private static readonly bool[] BoolArray = [true, false];
    private static readonly Random Random = new();
    
    public static IEnumerable<T> Generate<T>(IEnumerable<T> t1 = null)
    {
        return GenerateType(t1);
    }
    
    public static IEnumerable<(T1, T2)> Generate<T1, T2>(IEnumerable<T1> t1 = null, IEnumerable<T2> t2 = null)
    {
        foreach (T1 value1 in GenerateType(t1))
        {
            foreach (T2 value2 in GenerateType(t2))
            {
                yield return (value1, value2);
            }
        }
    }
    
    public static IEnumerable<(T1, T2, T3)> Generate<T1, T2, T3>(IEnumerable<T1> t1 = null, IEnumerable<T2> t2 = null, IEnumerable<T3> t3 = null)
    {
        foreach (T1 value1 in GenerateType(t1))
        {
            foreach (T2 value2 in GenerateType(t2))
            {
                foreach (T3 value3 in GenerateType(t3))
                {
                    yield return (value1, value2, value3);
                }
            }
        }
    }

    private static IEnumerable<T> GenerateType<T>(IEnumerable<T> t)
    {
        if (t != null)
        {
            return t;
        }

        if (typeof(T).IsEnum)
        {
            return GenerateEnums<T>();
        }
        
        if(typeof(T) == typeof(bool))
        {
            return BoolArray.Cast<T>();
        }

        return [];
    }
    
    private static IEnumerable<T> GenerateEnums<T>()
    {
        return typeof(T).IsDefined(typeof(FlagsAttribute), false) ? GenerateEnumFlags<T>() : Enum.GetValues(typeof(T)).Cast<T>();
    }

    private static IEnumerable<object> GenerateEnums(Type type)
    {
        return type.IsDefined(typeof(FlagsAttribute), false) ? GenerateEnumFlags(type) : Enum.GetValues(type).Cast<object>();
    }

    private static IEnumerable<T> GenerateEnumFlags<T>()
    {
        int[] values = Enum.GetValues(typeof(T)).Cast<T>().Select(v => Convert.ToInt32(v)).Where(v => v != 0).ToArray();
        int maxCombination = (1 << values.Length) - 1;

        List<T> permutations = [];

        for (int i = 1; i <= maxCombination; i++)
        {
            int combinedValue = 0;
            for (int bit = 0; bit < values.Length; bit++)
            {
                if ((i & (1 << bit)) != 0)
                {
                    combinedValue |= values[bit];
                }
            }

            permutations.Add((T)Enum.ToObject(typeof(T), combinedValue));
        }

        return permutations.Distinct();
    }
    
    private static IEnumerable<object> GenerateEnumFlags(Type type)
    {
        int[] values = Enum.GetValues(type).Cast<object>().Select(Convert.ToInt32).Where(v => v != 0).ToArray();
        int maxCombination = (1 << values.Length) - 1;

        List<object> permutations = [];

        for (int i = 1; i <= maxCombination; i++)
        {
            int combinedValue = 0;
            for (int bit = 0; bit < values.Length; bit++)
            {
                if ((i & (1 << bit)) != 0)
                {
                    combinedValue |= values[bit];
                }
            }

            permutations.Add(Enum.ToObject(type, combinedValue));
        }

        return permutations.Distinct();
    }
    
    public static T RandomValue<T>()
    {
        return (T)RandomValue(typeof(T));
    }
    
    private static object RandomValue(Type type)
    {
        if (type == typeof(bool)) return Random.Next(0, 2) == 1;
        if (type == typeof(byte)) return Random.Next(0, 255);
        if (type == typeof(sbyte)) return Random.Next(-128, 127);
        if (type == typeof(char)) return (char)Random.Next(0, 65535);
        if (type == typeof(short)) return Random.Next(short.MinValue, short.MaxValue);
        if (type == typeof(ushort)) return Random.Next(0, ushort.MaxValue);
        if (type == typeof(int)) return Random.Next(int.MinValue, int.MaxValue);
        if (type == typeof(uint)) return Random.Next(0, int.MaxValue);
        if (type == typeof(long)) return Random.NextInt64(long.MinValue, long.MaxValue);
        if (type == typeof(ulong)) return (ulong)Random.NextInt64(0, long.MaxValue);
        if (type == typeof(float)) return Random.NextSingle();
        if (type == typeof(double)) return Random.NextDouble();
        if (type == typeof(decimal)) return Random.NextDouble();
        if (type == typeof(string)) return GetRandomString(3, 15);
        if (type == typeof(Vector2)) return new Vector2(Random.NextSingle(), Random.NextSingle());
        if (type == typeof(Vector3)) return new Vector3(Random.NextSingle(), Random.NextSingle(), Random.NextSingle());
        if (type == typeof(Vector4)) return new Vector4(Random.NextSingle(), Random.NextSingle(), Random.NextSingle(), Random.NextSingle());
        if (type == typeof(UiColor)) return new UiColor(Random.NextSingle(), Random.NextSingle(), Random.NextSingle(), Random.NextSingle());
        if (type == typeof(UiReference)) return new UiReference(GetRandomString(3, 5), GetRandomString(3, 5));
        if (type == typeof(UiPosition)) return new UiPosition(Random.NextSingle(), Random.NextSingle(), Random.NextSingle(), Random.NextSingle());
        if (type == typeof(UiOffset)) return new UiOffset(GetRandomInt(100, 1000), GetRandomInt(100, 1000), GetRandomInt(100, 1000), GetRandomInt(100, 1000));
        if (type == typeof(UiPadding)) return new UiPadding(GetRandomInt(0, 10), GetRandomInt(0, 10), GetRandomInt(0, 10), GetRandomInt(0, 10));
        if (type == typeof(UiRotation)) return new UiRotation(GetRandomInt(0, 360));
        if (type.IsAssignableTo(typeof(Enum))) return GenerateEnum(type);
        if (type.IsNullable) return Random.NextSingle() < 0.25f ? null : RandomValue(type.GetNullableType());
        throw new Exception($"Type {type} is not supported");
    }

    public static int GetRandomInt(int minLength, int maxLength) => new Random().Next(minLength, maxLength);

    public static string GetRandomString(int minLength, int maxLength)
    {
        if (minLength > maxLength || maxLength < 0 || minLength < 0)
        {
            return string.Empty;
        }
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
       
        int length = GetRandomInt(minLength, maxLength);
        int charactersLength = characters.Length;
        StringBuilder sb = new();
        for (int i = 0; i < length; i++)
        {
            int index = Random.Next(charactersLength);
            sb.Append(characters[index]);
        }
        return sb.ToString();
    }

    public static object GenerateEnum(Type type) => GetRandomElement(GenerateEnums(type).ToArray());
    
    public static T GetRandomElement<T>(T[] array) => array[Random.Next(0, array.Length)];

    private static object GetRandomElement(object[] array)
    {
        return array[Random.Next(0, array.Length)];
    }
    
    public static void PopulateObject(object obj)
    {
        foreach (FieldInfo field in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Where(f => !f.IsInitOnly))
        {
            field.SetValue(obj, RandomValue(field.FieldType));
        }

        foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanWrite && p.SetMethod.IsPublic))
        {
            property.SetValue(obj, RandomValue(property.PropertyType));
        }

        if (obj is BaseUiComponent component)
        {
            PopulateObject(component.Component);
        } 
        else if (obj is CoreComponent core)
        {
            int numSub = Random.Next(0, 3);
            for (int i = 0; i < numSub; i++)
            {
                ComponentType type = (ComponentType)Random.Next((int)ComponentTypeExt.SubStart, (int)ComponentTypeExt.SubEnd + 1);
                if (!Enum.IsDefined(type))
                {
                    i--;
                    continue;
                }
                
                ISubComponent sub = core.GetOrAddSubComponentByType(type);
                PopulateObject(sub);
            }
        }
    }
}