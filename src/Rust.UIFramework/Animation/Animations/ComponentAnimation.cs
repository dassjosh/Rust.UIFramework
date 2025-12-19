using System;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class ComponentAnimation<T> : BaseAnimation, IComponentAnimation<T> where T : BaseComponent
{
    public T Component { get; private set; }
    private T _sourceComponent;

    public static ComponentAnimation<T> Create(IUiFrameworkPlugin plugin, T component, T sourceComponent) => plugin.PluginPool.Get<ComponentAnimation<T>>().Init(plugin, component, sourceComponent);
    
    protected ComponentAnimation<T> Init(IUiFrameworkPlugin plugin, T component, T sourceComponent)
    {
        base.Init(plugin);
        Component = component;
        _sourceComponent = sourceComponent;
        return this;
    }
    
    public IComponentAnimation<T> InitialState(Action<T> initialize)
    {
        initialize(Component);
        return this;
    }
    
    public AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(ComponentFieldSelector<TField, T> selector)
    {
        Tracked<TField> field = selector(Component);
        if (_sourceComponent != null)
        {
            field.Value = selector(_sourceComponent).Value;
        }
        field.ResetHasChanged();

        FieldAnimation<TField> animated = FieldAnimation<TField>.Create(Plugin, field);
        AddChildAnimation(animated);
        return new AnimationRef<IFieldAnimation<TField>>(animated);
    }

    protected override void EnterPool()
    {
        Component = null;
        _sourceComponent = null;
    }
}