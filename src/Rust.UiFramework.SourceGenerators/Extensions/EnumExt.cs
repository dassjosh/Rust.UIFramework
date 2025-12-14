using System;

namespace Rust.UiFramework.SourceGenerators.Extensions;

public static class EnumExt
{
    public static string ToParameterValue<T>(this T @enum) where T : Enum
    {
        return $"{@enum.GetType().FullName}.{@enum}";
    }
}