using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsMainThread()
    {
        if(Thread.CurrentThread.ManagedThreadId != 1) throw new InvalidOperationException("This method must be called on the main thread.");
    }
}