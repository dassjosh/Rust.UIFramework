using Oxide.Ext.UiFramework.Animation;

namespace Rust.UiFramework.UnitTests.Animations;

public class UnitTestAnimationTime : IAnimationTime
{
    public float CurrentTime { get; set; }
    public float DeltaTime { get; set; }
    public int CurrentFrame { get; set; }
    public float UpdateRate => 25f;
    public bool AnimationsEnabled { get; set; }
    
    public void AddSeconds(float seconds)
    {
        CurrentTime += seconds;
        DeltaTime = seconds;
        CurrentFrame += (int) (seconds * UpdateRate);
    }
}