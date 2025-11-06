namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteAnimationRepeat : IAnimationRepeat
{
    public static readonly InfiniteAnimationRepeat Default = new();
    
    int IAnimationRepeat.Repeats { get => -1; set {} }
    float IAnimationRepeat.RepeatDelay { get => 0; set {} }
    
    public bool OnRepeat() => true;
}