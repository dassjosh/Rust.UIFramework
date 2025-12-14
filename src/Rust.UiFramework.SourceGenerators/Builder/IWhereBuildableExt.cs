using System;
using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

public static class IWhereBuildableExt
{
    extension<T>(T builder) where T : IWhereBuildable
    {
        public T Where(Action<WhereBuilder> callback)
        {
            WhereBuilder where = new();
            builder.Where ??= [];
            builder.Where.Add(where);
            callback(where);
            return builder;
        }

        public void BuildWhere(StringBuilder sb, int indent)
        {
            if (builder.Where is { Count: > 0 })
            {
                foreach (WhereBuilder where in builder.Where)
                {
                    sb.Append(where.Build(indent));
                }
            }
        }
    }
}