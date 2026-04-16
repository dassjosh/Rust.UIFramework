using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationTime : IAnimationTime, ISingleton
{
    public float CurrentTime { get; private set; }
    public float DeltaTime { get; private set; }
    public int CurrentFrame { get; private set; }
    public float UpdateRate => _config.UpdateRate;
    public bool AnimationsEnabled => _config.Enabled;
    
    private readonly UiAnimationConfig _config = UiFrameworkConfig.Instance.Animations;

    private AnimationTime() { }
    
    internal void UpdateTime(float currentTime, bool wasPaused)
    {
        DeltaTime = wasPaused ? float.Epsilon : currentTime - CurrentTime;
        CurrentTime = currentTime;
        ++CurrentFrame;
    }
}