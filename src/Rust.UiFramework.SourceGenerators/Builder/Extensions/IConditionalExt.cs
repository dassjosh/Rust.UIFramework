using System;
using System.Collections.Generic;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

internal static class IConditionalExt
{
    extension<T>(T builder) where T : IConditional
    {
        public ConditionalResult<T> If(bool statement, Action<T> onTrue)
        {
            if (statement)
            {
                onTrue(builder);
            }

            return new ConditionalResult<T>(builder, statement);
        }
        
        public SwitchState<T, TKey> Switch<TKey>(TKey key)
        {
            return new SwitchState<T, TKey>(builder, key, false);
        }
    }

    extension<T>(ConditionalResult<T> builder) where T : IConditional
    {
        public ConditionalResult<T> ElseIf(bool statement, Action<T> onTrue)
        {
            if (builder.Result)
            {
                return builder;
            }
        
            if (statement)
            {
                onTrue(builder.Value);
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

        public T EndIf() => builder.Value;
    }
    
    extension<T, TKey>(SwitchState<T, TKey> state) where T : IConditional
    {
        public SwitchState<T, TKey> Case(TKey caseValue, Action<T> action)
        {
            if (state.CaseExecuted)
            {
                return state;
            }
            
            if (EqualityComparer<TKey>.Default.Equals(state.Key, caseValue))
            {
                action(state.Value);
                return state with { CaseExecuted = true };
            }
            
            return state;
        }

        public SwitchState<T, TKey> Default(Action<T> action)
        {
            if (state.CaseExecuted)
            {
                return state;
            }

            action(state.Value);
            return state with { CaseExecuted = true };
        }

        public T EndSwitch() => state.Value;
    }
}

public readonly record struct ConditionalResult<T>(T Value, bool Result) where T : IConditional
{
    public static implicit operator T(ConditionalResult<T> result) => result.Value;
}

public readonly record struct SwitchState<T, TKey>(T Value, TKey Key, bool CaseExecuted) where T : IConditional;