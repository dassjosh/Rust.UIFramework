using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Types;

public static class LevenshteinDistanceExt
{
    private static readonly ConcurrentDictionary<LevenshteinDistanceKey, LevenshteinDistance> Interpolators = new();

    extension(LevenshteinDistance)
    {
        public static string Lerp(string start, string end, float value)
        {
            LevenshteinDistanceKey key = new(start, end);
            LevenshteinDistance interpolator = Interpolators.GetOrAdd(key, CreateNew);
            return interpolator.GetFrame(value);
        }
    }

#pragma warning disable EPS05
    private static LevenshteinDistance CreateNew(LevenshteinDistanceKey ldk) => new(ldk.Start, ldk.End);
#pragma warning restore EPS05

    private readonly record struct LevenshteinDistanceKey(string Start, string End);
}