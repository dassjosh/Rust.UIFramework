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

    extension<T>(ConditionalResult<T> builder) where T : IConditional
    {
        public ConditionalResult<T> ElseIf(bool statement, Action<T> action)
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

        public ConditionalResult<T> Else(Action<T> action)
        {
            if (builder.Result)
            {
                return builder;
            }

            action(builder.Value);

            return builder with { Result = true };
        }

        public T EndIf()
        {
            return builder.Value;
        }
    }
}

public readonly record struct ConditionalResult<T>(T Value, bool Result)
{
    public static implicit operator T(ConditionalResult<T> result) => result.Value;
}