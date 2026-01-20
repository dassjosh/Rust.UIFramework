using System;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public static class ElementAnimationExt
{
    extension<T>(in AnimationRef<IElementAnimation<T>> animation) where T : BaseUiComponent
    {
        public AnimationRef<IFieldAnimation<bool>> AnimateActive()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(static a => a.ActiveTracked);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<bool>> AnimateEnabled()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(static a => a.Component.EnabledTracked);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<UiPosition>> AnimatePosition()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(static a => a.RectTransform.PositionTracked);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<UiOffset>> AnimateOffset()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(static a => a.RectTransform.OffsetTracked);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<UiRotation>> AnimateRotation()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(static a => a.RectTransform.RotationTracked);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiScale>> AnimateScale(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.RectTransform.PositionScaleTracked),
                PositionField.Offset => animation.AnimateField(static a => a.RectTransform.OffsetScaleTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> AnimateTranslate(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.RectTransform.PositionTranslateTracked),
                PositionField.Offset => animation.AnimateField(static a => a.RectTransform.OffsetTranslateTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
        
        public AnimationRef<IFieldAnimation<UiPadding>> AnimatePadding(PositionField target)
        {
            return target switch
            {
                PositionField.Position => animation.AnimateField(static a => a.RectTransform.PositionPaddingTracked),
                PositionField.Offset => animation.AnimateField(static a => a.RectTransform.OffsetPaddingTracked),
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
        
        public AnimationRef<IFieldAnimation<UiPosition>> Slide(in UiPosition start, in UiPosition end)
        {
            if (animation.IsValid)
            {
                return animation.AnimatePosition().Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiOffset>> Slide(in UiOffset start, in UiOffset end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateOffset().Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiScale>> Scale(in UiScale start, in UiScale end, PositionField target)
        {
            if (animation.IsValid)
            {
                return animation.AnimateScale(target).Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiRotation>> Spin(in UiRotation start, in UiRotation end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateRotation().Lerp(start, end);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<UiRotation>> Spin() => animation.Spin(UiRotation.Zero, UiRotation.Full);
        
        public AnimationRef<IFieldAnimation<UiScale>> PulsePosition(UiScale pulsedSize, PositionField target)
        {
            if (animation.IsValid)
            {
                return animation.Scale(UiScale.One, pulsedSize, target).Linear().PingPong();
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeX()
        {
            if (animation.IsValid)
            {
                return animation.AnimateTranslate(PositionField.Offset).ShakeX();
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeY()
        {
            if (animation.IsValid)
            {
                return animation.AnimateTranslate(PositionField.Offset).ShakeY();
            }
            return default;
        }
        
        public UiTuple<AnimationRef<IFieldAnimation<UiTranslate>>, AnimationRef<IFieldAnimation<UiRotation>>> Wobble(PositionField target)
        {
            if (animation.IsValid)
            {
                return UiTuple.Create(animation.AnimateTranslate(target).Wobble(),
                    animation.AnimateRotation().Wobble());
            }
            return default;
        }
        
        public UiTuple<AnimationRef<IFieldAnimation<UiTranslate>>, AnimationRef<IFieldAnimation<UiRotation>>> HeadShake()
        {
            if (animation.IsValid)
            {
                return UiTuple.Create(
                    animation.AnimateTranslate(PositionField.Offset).HeadShake(),
                    animation.AnimateRotation().HeadShake()
                    );
            }
            return default;
        }
    }
}