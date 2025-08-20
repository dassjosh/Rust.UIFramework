namespace Rust.UiFramework.UnitTests.Global.Generators;

public static class Permutations
{
    private static readonly bool[] BoolArray = [true, false];
    
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
            return typeof(T).IsDefined(typeof(FlagsAttribute), false) ? GenerateEnumFlags<T>() : Enum.GetValues(typeof(T)).Cast<T>();
        }
        
        if(typeof(T) == typeof(bool))
        {
            return BoolArray.Cast<T>();
        }

        return [];
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
}