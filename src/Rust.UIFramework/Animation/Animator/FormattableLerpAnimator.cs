using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Animation;

public class FormattableLerpAnimator<T> : LerpAnimator<T>, IAnimator<string> where T : unmanaged, IFormattable
{
    public string Format;
    public IFormatProvider FormatProvider;
    
    public FormattableLerpAnimator() { }
    
    public FormattableLerpAnimator(T start, T end, string format = null, IFormatProvider formatProvider = null) : this(start, end, UiLerp.GetDefaultOrError<T>(), format, formatProvider) { }

    public FormattableLerpAnimator(T start, T end, UiLerp<T> lerp, string format = null, IFormatProvider formatProvider = null) : base(start, end, lerp)
    {
        Format = format;
        FormatProvider = formatProvider;
    }
    
    public static FormattableLerpAnimator<T> Create(IUiFrameworkPlugin plugin, T start, T end, UiLerp<T> lerp, string format, IFormatProvider formatProvider)
        => plugin.PluginPool.Get<FormattableLerpAnimator<T>>().Init(start, end, lerp, format, formatProvider);

    protected FormattableLerpAnimator<T> Init(T start, T end, UiLerp<T> lerp, string format, IFormatProvider formatProvider)
    {
        base.Init(start, end, lerp);
        Format = format;
        FormatProvider = formatProvider;
        return this;
    }

    public new string Get(float progress) => base.Get(progress).ToString(Format, FormatProvider);

    protected override void EnterPool()
    {
        base.EnterPool();
        Format = null;
        FormatProvider = null;
    }
}