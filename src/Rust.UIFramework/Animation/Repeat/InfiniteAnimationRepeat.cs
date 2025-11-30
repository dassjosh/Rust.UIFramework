namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteAnimationRepeat : IAnimationRepeat
{
    public static readonly InfiniteAnimationRepeat Instance = new();
    
    int IAnimationRepeat.Repeats { get => -1; set {} }
    float IAnimationRepeat.RepeatDelay { get => 0; set {} }

    private InfiniteAnimationRepeat() { }
    
    public bool OnRepeat() => true;
}