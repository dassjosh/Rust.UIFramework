namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationRepeat
{
    int Repeats { get; set; }
    float RepeatDelay { get; set; }
    bool OnRepeat();
}