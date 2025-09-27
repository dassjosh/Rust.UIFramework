namespace Oxide.Ext.UiFramework.Animation;

public class PingPongProgressor : IAnimationProgressor
{
    public static readonly PingPongProgressor Default = new();
    
    private PingPongProgressor() { }
    
    public float GetProgress(float elapsedPercentage)
    {
        if (elapsedPercentage <= 0.5f)
        {
            return elapsedPercentage * 2;
        }

        return 1f - (elapsedPercentage - 0.5f) * 2;
    }
}