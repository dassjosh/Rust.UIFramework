using System;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class ComponentAnimationExt
{
    extension(IComponentAnimation<RectTransformComponent> animation)
    {
        public AnimationRef<IFieldAnimation<UiScale>> AnimateScale(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.PositionScaleTracked),
                PositionField.Offset => animation.AnimateField(static a => a.OffsetScaleTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> AnimateTranslate(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.PositionTranslateTracked),
                PositionField.Offset => animation.AnimateField(static a => a.OffsetTranslateTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
        
        public AnimationRef<IFieldAnimation<UiPadding>> AnimatePadding(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.PositionPaddingTracked),
                PositionField.Offset => animation.AnimateField(static a => a.OffsetPaddingTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
    }
}