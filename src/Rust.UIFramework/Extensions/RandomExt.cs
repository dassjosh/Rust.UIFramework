using System;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class RandomExt
{
    private static readonly Random Random = new();
    private static readonly byte[] Buffer = new byte[8];

    internal static long NextLong()
    {
        Random.NextBytes(Buffer);
        return BitConverter.ToInt64(Buffer, 0);
    }
}