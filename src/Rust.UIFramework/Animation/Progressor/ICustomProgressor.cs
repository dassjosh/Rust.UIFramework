namespace Oxide.Ext.UiFramework.Animation;

/// <summary>
/// Providers a custom animator to animate a UiElement
/// </summary>
public interface ICustomProgressor
{
    /// <summary>
    /// Returns the current progress for the animation based on the elapsed percentage
    /// Values should be between 0 and 1 but can go outside the range IE for Bezier Curves.
    /// </summary>
    /// <param name="elapsedPercentage"></param>
    /// <returns></returns>
    public float GetProgress(float elapsedPercentage);
}