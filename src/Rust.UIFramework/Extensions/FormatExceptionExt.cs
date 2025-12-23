using System;
using System.Diagnostics.CodeAnalysis;

namespace Oxide.Ext.UiFramework.Extensions;

public static class FormatExceptionExt
{
    extension(FormatException)
    {
        [DoesNotReturn]
        public static void ThrowFailedParse<T>(ReadOnlySpan<char> input) => throw new FormatException($"Unable to parse '{input.ToString()}' as {typeof(T).Name}");
        
        public static FormatException FailedParse<T>(ReadOnlySpan<char> input) => new($"Unable to parse '{input.ToString()}' as {typeof(T).Name}");
    }
}