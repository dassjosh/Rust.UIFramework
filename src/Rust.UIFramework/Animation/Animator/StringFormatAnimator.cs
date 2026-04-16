using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class StringFormatAnimator : BasePoolable, IAnimator<string>
{
    private string _format;
    private IFormatProvider _provider;
    private List<AnimatorIndex> _animators;
    private UiPooledArray<object> _values;

    public StringFormatAnimator() { }

    public StringFormatAnimator(string format, IFormatProvider provider, IEnumerable<object> animators)
    {
        Init(format, provider, animators);
    }

    public static StringFormatAnimator Create(IUiFrameworkPlugin plugin, string format, IFormatProvider provider, IEnumerable<object> animators)
        => plugin.PluginPool.Get<StringFormatAnimator>().Init(format, provider, animators);

    protected StringFormatAnimator Init(string format, IFormatProvider provider, IEnumerable<object> animators)
    {
        _format = format ?? throw new ArgumentNullException(nameof(format));
        _provider = provider;
        List<object> tempAnimators = PluginPool.GetList<object>();
        tempAnimators.AddRange(animators);
        _values = PluginPool.GetArray<object>(tempAnimators.Count);
        int index = 0;
        foreach (object obj in tempAnimators)
        {
            if (obj is IAnimator<object> animator)
            {
                _animators.Add(new AnimatorIndex(animator, index));
            }
            else
            {
                _values[index] = obj;
            }

            index++;
        }
       
        PluginPool.FreeList(tempAnimators);
        return this;
    }

    public string Get(float progress)
    {
        for (int index = 0; index < _animators.Count; index++)
        {
            AnimatorIndex animator = _animators[index];
            _values[animator.Index] = animator.Animator.Get(progress);
        }
        
        return string.Format(_provider, _format, _values.Array);
    }

    protected override void LeavePool()
    {
        _animators = PluginPool.GetList<AnimatorIndex>();
    }

    protected override void EnterPool()
    {
        _format = null;
        _provider = null;
        PluginPool.FreeList(_animators);
        _animators.TryFreeValues();
        _values.TryDispose();
    }
    
    private readonly record struct AnimatorIndex(IAnimator<object> Animator, int Index);
}