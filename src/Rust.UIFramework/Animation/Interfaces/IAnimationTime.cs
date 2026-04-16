namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationTime
{
    float CurrentTime { get; }
    float DeltaTime { get; }
    int CurrentFrame { get; }
    float UpdateRate { get; }
    float FramesPerSecond => 1f / (UpdateRate / 1000f);
    bool AnimationsEnabled { get; }
}