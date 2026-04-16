using System;
using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.Animation;

public interface IComponentAnimation<out T> : IAnimation where T : BaseComponent
{
    T Component { get; }
    IComponentAnimation<T> InitialState(Action<T> initialize);
    AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(ComponentFieldSelector<TField, T> selector);
    AnimationRef<IComponentAnimation<TComponent>> AnimateComponent<TComponent>(ChildComponentSelector<TComponent, T> selector) where TComponent : BaseComponent;
}