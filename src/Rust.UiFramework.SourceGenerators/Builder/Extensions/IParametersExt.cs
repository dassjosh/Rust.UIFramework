using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Interfaces;

namespace Rust.UiFramework.SourceGenerators.Builder.Extensions;

public static class IParametersExt
{
    extension<T>(T builder) where T : IParameters
    {
        public T AddParameter(string parameter) => builder.AddParameter(p => p.Value(parameter));

        public T AddParameter(Action<ParameterBuilder> parameter)
        {
            ParameterBuilder parm = new();
            parameter(parm);
            builder.Parameters ??= [];
            builder.Parameters.Add(parm);
            return builder;
        }
    
        public T AddParameters<TValue>(IEnumerable<TValue> parameters, Action<TValue, ParameterBuilder> parameter)
        {
            builder.Parameters ??= [];
            foreach (TValue data in parameters)
            {
                ParameterBuilder parm = new();
                parameter(data, parm);
                builder.Parameters.Add(parm);
            }
            return builder;
        }
        
        public T AddParameters<TValue>(IEnumerable<TValue> parameters, Action<TValue, ParameterBuilder, int> parameter)
        {
            builder.Parameters ??= [];
            int index = 0;
            foreach (TValue data in parameters)
            {
                ParameterBuilder parm = new();
                parameter(data, parm, index++);
                builder.Parameters.Add(parm);
            }
            return builder;
        }
        
        public IEnumerable<ParameterBuilder> Parameters => builder.Parameters ?? Enumerable.Empty<ParameterBuilder>();

        public void BuildParameters(StringBuilder sb, int indent)
        {
            if (builder.Parameters is not null)
            {
                sb.Append('(');
                sb.Append(string.Join(", ", builder.Parameters.Select(p => p.Build(indent))));
                sb.Append(')');
            }
        }
    }
}