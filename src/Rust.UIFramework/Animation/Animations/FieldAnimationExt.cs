using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

//TODO: Move?
public static class FieldAnimationExt
{
    public static IFieldAnimation<UiPosition> Lerp(this IFieldAnimation<UiPosition> field, in UiPosition start, in UiPosition end) 
        => field.WithAnimator(LerpAnimator<UiPosition>.Create(field.Plugin, start, end, UiPosition.LerpUnclamped));

    public static IFieldAnimation<UiPosition> Lerp(this IFieldAnimation<UiPosition> field, in UiPosition end) => field.Lerp(field.Value, end);
    
    public static IFieldAnimation<UiOffset> Lerp(this IFieldAnimation<UiOffset> field, in UiOffset start, in UiOffset end) 
        => field.WithAnimator(LerpAnimator<UiOffset>.Create(field.Plugin, start, end, UiOffset.LerpUnclamped));

    public static IFieldAnimation<UiOffset> Lerp(this IFieldAnimation<UiOffset> field, in UiOffset end) => field.Lerp(field.Value, end);
    
    public static IFieldAnimation<UiRotation> Lerp(this IFieldAnimation<UiRotation> field, in UiRotation start, in UiRotation end) 
        => field.WithAnimator(LerpAnimator<UiRotation>.Create(field.Plugin, start, end, UiRotation.LerpUnclamped));

    public static IFieldAnimation<UiRotation> Lerp(this IFieldAnimation<UiRotation> field, in UiRotation end) => field.Lerp(field.Value, end);
    
    public static IFieldAnimation<UiColor> Lerp(this IFieldAnimation<UiColor> field, UiColor start, UiColor end) 
        => field.WithAnimator(LerpAnimator<UiColor>.Create(field.Plugin, start, end, UiColor.LerpUnclamped));

    public static IFieldAnimation<UiColor> Lerp(this IFieldAnimation<UiColor> field, UiColor end) => field.Lerp(field.Value, end);
    
    public static IFieldAnimation<string> Lerp(this IFieldAnimation<string> field, string start, string end) 
        => field.WithAnimator(LerpAnimator<string>.Create(field.Plugin, start, end, LevenshteinDistanceExt.Lerp));

    public static IFieldAnimation<string> Lerp(this IFieldAnimation<string> field, string end) => field.Lerp(field.Value, end);
    
    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T start, T end, UiLerp<T> lerp) 
        => field.WithAnimator(LerpAnimator<T>.Create(field.Plugin, start, end, lerp));

    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T end, UiLerp<T> lerp) => field.Lerp(field.Value, end, lerp);

    public static IFieldAnimation<T> WithAnimator<T>(this IFieldAnimation<T> field, IAnimator<T> animator)
    {
        field.Interpolator.Animator.TryReturnToPool();
        field.Interpolator.Animator = animator;
        return field;
    }
}