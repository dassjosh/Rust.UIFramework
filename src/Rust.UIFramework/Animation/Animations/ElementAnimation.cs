using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class ElementAnimation<T> : SendableAnimation, IElementAnimation<T> where T : BaseUiComponent, new()
{
    public T Element { get; } = new();
    private T _sourceElement;
    public bool Tracked { get; set; } = true;
    public override bool HasChanged => base.HasChanged || Element.HasChanged();
    public string Reference => Element.Name;

    public static ElementAnimation<T> Create(IUiFrameworkPlugin plugin, T sourceElement) => plugin.PluginPool.Get<ElementAnimation<T>>().Init(plugin, sourceElement);
    public static ElementAnimation<T> Create(IUiFrameworkPlugin plugin, string name) => plugin.PluginPool.Get<ElementAnimation<T>>().Init(plugin, name);

    protected ElementAnimation<T> Init(IUiFrameworkPlugin plugin, T sourceElement)
    {
        _sourceElement = sourceElement;
        return Init(plugin, sourceElement.Name);
    }
    
    protected ElementAnimation<T> Init(IUiFrameworkPlugin plugin, string name)
    {
        base.Init(plugin);
        Element.Name = name;
        Element.Update = UpdateMode.Update;
        Element.OverridePluginPool(plugin.PluginPool);
        return this;
    }
    
    public ElementAnimation<T> InitialState(Action<T> initialize)
    {
        initialize(Element);
        return this;
    }
    
    public AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(FieldSelector<TField, T> selector)
    {
        Tracked<TField> field = selector(Element);
        if (_sourceElement != null)
        {
            field.Value = selector(_sourceElement).Value;
        }
        field.ResetHasChanged();

        FieldAnimation<TField> animated = FieldAnimation<TField>.Create(Plugin, field);
        AddChildAnimation(animated);
        return new AnimationRef<IFieldAnimation<TField>>(animated);
    }

    public override void Serialize(JsonFrameworkWriter writer)
    {
        Element.WriteElement(writer);
        Element.ResetHasChanged();
        base.Serialize(writer);
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Element.Reset();
        _sourceElement = null;
        Tracked = true;
    }
}