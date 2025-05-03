namespace Oxide.Ext.UiFramework.Animation;

public class LoopProgressor : IAnimationProgressor
{
    public static readonly LoopProgressor Default = new();
    
    private LoopProgressor() { }
    
    public float GetProgress(float elapsedPercentage)
    {
        if (elapsedPercentage <= 0.5f)
        {
            return elapsedPercentage * 2;
        }

        return 1f - (elapsedPercentage - 0.5f) * 2;
    }
}