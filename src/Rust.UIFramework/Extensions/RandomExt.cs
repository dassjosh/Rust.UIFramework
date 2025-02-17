using System;

namespace Oxide.Ext.UiFramework.Extensions;

public static class RandomExt
{
    private static readonly Random _random = new();
    private static readonly byte[] _buffer = new byte[8];

    public static long NextLong()
    {
        _random.NextBytes(_buffer);
        return BitConverter.ToInt64(_buffer, 0);
    }
}