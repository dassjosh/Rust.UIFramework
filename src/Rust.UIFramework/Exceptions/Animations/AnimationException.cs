using Oxide.Ext.UiFramework.Animation;

namespace Oxide.Ext.UiFramework.Exceptions;

public class AnimationException(string message) : BaseUiFrameworkException(message)
{
    public static void ThrowIfMissingSend(ISendableAnimation animation)
    {
        if (animation.Send.connection == null && animation.Send.connections == null)
        {
            throw new AnimationException($"Invalid Animation State. Animation is missing Send. ID: {animation.Id} Plugin: {animation.Plugin.Name}");
        }
    }
}