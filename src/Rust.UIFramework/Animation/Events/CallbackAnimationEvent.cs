using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class CallbackAnimationEvent<T> : BasePoolable, IAnimationEvent where T : class, IAnimation
{
    private AnimationEventType _type;
    private Action<T> _callback;
    
    public static CallbackAnimationEvent<T> Create(IUiFrameworkPlugin plugin, AnimationEventType type, Action<T> callback)
    {
        CallbackAnimationEvent<T> @event = plugin.PluginPool.Get<CallbackAnimationEvent<T>>();
        @event._type = type;
        @event._callback = callback;
        return @event;
    }
    
    public bool IsForEvent(AnimationEventType type) => _type == type;

    public void OnAnimationEvent(in AnimationRef<IAnimation> animation, AnimationEventType type)
    {
        if (!animation.IsValid)
        {
            return;
        }
        
        try
        {
            _callback?.Invoke((T)animation.Animation);
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