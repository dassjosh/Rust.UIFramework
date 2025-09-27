namespace Oxide.Ext.UiFramework.Animation;

public interface IConfigurableAnimationDuration : IAnimationDuration, IRemainingDuration
{
    public float Delay { get; set; }
    public float Duration { get; set; }
    
    public int Repeats { get; set; }
    public float RepeatDelay { get; set; }
}