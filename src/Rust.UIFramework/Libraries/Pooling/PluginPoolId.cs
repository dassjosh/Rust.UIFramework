using System.Threading;

namespace Oxide.Ext.UiFramework.Libraries;

internal readonly struct PluginPoolId
{
    public readonly int Id;
    public bool IsValid => Id != 0;

    private PluginPoolId(int id)
    {
        Id = id;
    }

    private static int _nextId;
    internal static PluginPoolId GetNextId() => new(Interlocked.Increment(ref _nextId));
}