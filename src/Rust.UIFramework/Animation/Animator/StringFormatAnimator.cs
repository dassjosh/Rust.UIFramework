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
    private readonly List<object> _animators = [];
    private UiPooledArray<object> _values;

    public StringFormatAnimator() { }

    public StringFormatAnimator(string format, IFormatProvider provider, IEnumerable<object> animators)
    {
        _format = format;
        _provider = provider;
        _animators.AddRange(animators);
        _values = new UiPooledArray<object>(_animators.Count);
    }

    public static StringFormatAnimator Create(IUiFrameworkPlugin plugin, string format, IFormatProvider provider, IEnumerable<object> animators)
        => plugin.PluginPool.Get<StringFormatAnimator>().Init(format, provider, animators);

    protected StringFormatAnimator Init(string format, IFormatProvider provider, IEnumerable<object> animators)
    {
        _format = format ?? throw new ArgumentNullException(nameof(format));
        _provider = provider;
        _animators.AddRange(animators);
        _values = PluginPool.GetArray<object>(_animators.Count);
        return this;
    }

    public string Get(float progress)
    {
        for (int index = 0; index < _animators.Count; index++)
        {
            object obj = _animators[index];
            if (obj is IAnimator<object> animator)
            {
                _values[index] = animator.Get(progress);
            }
            else
            {
                _values[index] = obj;
            }
        }
        
        return string.Format(_provider, _format, _values);
    }

    protected override void EnterPool()
    {
        _format = null;
        _provider = null;
        _animators.TryFreeValues();
        _values.TryDispose();
    }
}