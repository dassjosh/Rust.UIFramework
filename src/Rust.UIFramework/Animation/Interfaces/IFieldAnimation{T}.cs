using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public interface IFieldAnimation<T> : IAnimation
{
    Tracked<T> Field { get; }
    T Value => Field.Value;
    new IAnimationInterpolator<T> Interpolator { get; set;  }
}