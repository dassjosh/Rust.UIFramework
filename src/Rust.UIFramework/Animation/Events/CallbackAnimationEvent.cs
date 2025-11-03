using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class CallbackAnimationEvent : BasePoolable, IAnimationEvent
{
    private AnimationEventType _type;
    private Action<IAnimation> _callback;
    
    public static CallbackAnimationEvent Create(IUiFrameworkPlugin plugin, AnimationEventType type, Action<IAnimation> callback)
    {
        CallbackAnimationEvent @event = plugin.PluginPool.Get<CallbackAnimationEvent>();
        @event._type = type;
        @event._callback = callback;
        return @event;
    }
    
    public bool IsForEvent(AnimationEventType type) => _type == type;

    public void OnAnimationEvent(IAnimation animation, AnimationEventType type)
    {
        try
        {
            _callback?.Invoke(animation);
        }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Exception("An error occured in {0} animation event callback", _type, ex);
        }
    }

    protected override void EnterPool()
    {
        _type = default;
        _callback = null;
    }
}