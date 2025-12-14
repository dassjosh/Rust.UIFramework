namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class StringExt
{
    extension(string value)
    {
        public string ToCamelCase()
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
        
            return char.ToLower(value[0]) + value[1..];
        }


        
        public bool ContainsAny(char[] chars)
        {
            return value.IndexOfAny(chars) >= 0;
        }
    }
}