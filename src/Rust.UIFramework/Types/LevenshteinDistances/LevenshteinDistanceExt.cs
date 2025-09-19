using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Types;

public static class LevenshteinDistanceExt
{
    private static readonly ConcurrentDictionary<LevenshteinDistanceKey, LevenshteinDistance> Interpolators = new();
    
    public static string Lerp(string start, string end, float value)
    {
        LevenshteinDistanceKey key = new(start, end);
        LevenshteinDistance interpolator = Interpolators.GetOrAdd(key, CreateNew);
        return interpolator.GetFrame(value);
    }

    private static LevenshteinDistance CreateNew(LevenshteinDistanceKey ldk) => new(ldk.Start, ldk.End);

    private readonly record struct LevenshteinDistanceKey(string Start, string End);
}