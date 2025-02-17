using System;
using System.Threading;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class CastExt
{
    internal static TDestination Cast<TSource, TDestination>(this TSource source)
    {
        CastImpl<TSource, TDestination>.Value.Value = source;
        return CastImpl<TDestination, TSource>.Value.Value;
    }
        
    private static class CastImpl<TSource, TDestination>
    {
        public static readonly ThreadLocal<TSource> Value = new();

        static CastImpl()
        {
            if (typeof(TSource) != typeof(TDestination)) throw new InvalidCastException($"{typeof(TSource)} != {typeof(TDestination)}");
        }
    }
}