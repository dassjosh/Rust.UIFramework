namespace Rust.UiFramework.UnitTests.Global.Generators;

public static class TheoryDataGenerator
{
    public static TheoryData<TRow> Generate<TRow, T>(Func<T, TRow> func, T[] t = null) => new(Permutations.Generate(t).Select(func));
    public static TheoryData<TRow> Generate<TRow, T1, T2>(Func<T1, T2, TRow> func, T1[] t1 = null, T2[] t2 = null) => new(Permutations.Generate(t1, t2).Select(tuple => func(tuple.Item1, tuple.Item2)));
    public static TheoryData<TRow> Generate<TRow, T1, T2, T3>(Func<T1, T2, T3, TRow> func, T1[] t1 = null, T2[] t2 = null, T3[] t3 = null) => new(Permutations.Generate(t1, t2, t3).Select(tuple => func(tuple.Item1, tuple.Item2, tuple.Item3)));
}