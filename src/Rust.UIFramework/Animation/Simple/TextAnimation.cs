using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public delegate string TextFormatter(float progress, string text);

public class TextAnimation : SimpleAnimation<string>
{
    private Utf8String _elementType;
    private TextFormatter _formatter;
    
    public static TextAnimation Create(IAnimationBuilder builder, in AnimationReference reference, ISimpleAnimator<string> animator, IAnimationDuration duration, TextFormatter formatter) 
        => builder.PluginPool.Get<TextAnimation>().Init(builder.Plugin, reference, animator, duration, formatter);

    private TextAnimation Init(IUiFrameworkPlugin plugin, in AnimationReference reference, ISimpleAnimator<string> animator, IAnimationDuration duration, TextFormatter formatter)
    {
        base.Init(plugin, reference.Reference, animator, duration);
        _elementType = reference.Type;
        _formatter = formatter;
        return this;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, string value, float progress)
    {
        if (_formatter != null)
        {
            value = _formatter(progress, value);
        }
        
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, _elementType);
        writer.AddFieldRaw(JsonDefaults.Text.TextName, value);
        writer.WriteEndObject();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _elementType = default;
    }
}