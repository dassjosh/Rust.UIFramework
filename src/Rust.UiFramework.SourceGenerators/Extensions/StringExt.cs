namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class StringExt
{
    public static bool ContainsAny(this string str, char[] chars)
    {
        return str.IndexOfAny(chars) >= 0;
    }
}