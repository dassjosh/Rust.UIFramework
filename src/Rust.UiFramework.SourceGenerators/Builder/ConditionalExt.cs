using System;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class ConditionalExt
{
    public static ConditionalResult<T> If<T>(this T builder, bool statement, Action<T> action) where T : IConditional
    {
        if (statement)
        {
            action(builder);
        }

        return new ConditionalResult<T>(builder, statement);
    }

    public static ConditionalResult<T> ElseIf<T>(this ConditionalResult<T> builder, bool statement, Action<T> action) where T : IConditional
    {
        if (builder.Result)
        {
            return builder;
        }
        
        if (statement)
        {
            action(builder.Value);
        }

        return builder with { Result = statement };
    }
    
    public static ConditionalResult<T> Else<T>(this ConditionalResult<T> builder, Action<T> action) where T : IConditional
    {
        if (builder.Result)
        {
            return builder;
        }

        action(builder.Value);

        return builder with { Result = true };
    }

    public static T EndIf<T>(this ConditionalResult<T> builder) where T : IConditional
    {
        return builder.Value;
    }
}

public readonly record struct ConditionalResult<T>(T Value, bool Result)
{
    public static implicit operator T(ConditionalResult<T> result) => result.Value;
}