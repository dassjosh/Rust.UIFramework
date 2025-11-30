using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Types;

public class FormattableConvertable<T> : BasePoolable, IUiConvertable<string, T> where T : IFormattable
{
    private string _format;
    private IFormatProvider _provider;
    
    public FormattableConvertable() { }

    public FormattableConvertable(string format, IFormatProvider provider)
    {
        _format = format;
        _provider = provider;
    }
    
    public static FormattableConvertable<T> Create(IUiFrameworkPlugin plugin, string format, IFormatProvider provider) => plugin.PluginPool.Get<FormattableConvertable<T>>().Init(format, provider);
    
    protected FormattableConvertable<T> Init(string format, IFormatProvider provider)
    {
        _format = format;
        _provider = provider;
        return this;
    }
    
    public string Convert(T from) => from.ToString(_format, _provider);

    protected override void EnterPool()
    {
        _format = default;
        _provider = default;
    }
}