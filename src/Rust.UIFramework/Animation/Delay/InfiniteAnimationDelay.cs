namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteAnimationDelay : IAnimationDelay
{
    public bool IsDelayed => true;

    public static readonly InfiniteAnimationDelay Instance = new();
    
    private InfiniteAnimationDelay() { }
    
    public void OnStarted() {}
    public void OnTick() { }
}