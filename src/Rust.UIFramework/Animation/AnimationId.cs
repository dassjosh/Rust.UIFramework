using System.Threading;

namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct AnimationId(long Id)
{
    public bool IsValid => Id != 0;

    private static long _nextAnimationId;
    
    internal static AnimationId GetNextId() => new(Interlocked.Increment(ref _nextAnimationId));
}