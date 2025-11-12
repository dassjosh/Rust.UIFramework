using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public interface IElementAnimation<out T> : IElementAnimation where T : BaseUiComponent
{
    T Element { get; }
    AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(FieldSelector<TField, T> selector);
}