using System;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public interface IElementAnimation<out T> : IElementAnimation where T : BaseUiComponent
{
    T Element { get; }
    IElementAnimation<T> InitialState(Action<T> initialize);
    AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(ElementFieldSelector<TField, T> selector);
    AnimationRef<IComponentAnimation<TComponent>> AnimateComponent<TComponent>(ComponentSelector<TComponent, T> selector) where TComponent : BaseComponent;
}