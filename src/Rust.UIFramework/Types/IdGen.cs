using System.Threading;

namespace Oxide.Ext.UiFramework.Types;

// ReSharper disable once UnusedTypeParameter
internal static class IdGen<T>
{
    private static int _nextId;
    internal static int GetNextId() => Interlocked.Increment(ref _nextId);
}