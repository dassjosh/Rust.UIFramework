using Oxide.Ext.UiFramework.Config;

namespace Oxide.Ext.UiFramework.Animation;

public static class AnimationTime
{
    public static float CurrentTime { get; private set; }
    public static float DeltaTime { get; private set; }
    public static bool AnimationsEnabled => Config.Enabled;
    
    private static readonly UiAnimationConfig Config = UiFrameworkConfig.Instance.Animations;
    
    internal static void UpdateTime(float currentTime, bool wasPaused)
    {
        DeltaTime = wasPaused ? float.Epsilon : currentTime - CurrentTime;
        CurrentTime = currentTime;
    }
}