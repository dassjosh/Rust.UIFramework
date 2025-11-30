using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public static class ElementAnimationExt
{
    extension<T>(in AnimationRef<IElementAnimation<T>> animation) where T : BaseUiComponent
    {
        public AnimationRef<IFieldAnimation<UiPosition>> Slide(in UiPosition start, in UiPosition end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().Position)
                    .Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiOffset>> Slide(in UiOffset start, in UiOffset end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().Offset)
                    .Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiScale>> ScalePosition(in UiScale start, in UiScale end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().PositionScale)
                    .Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiScale>> ScaleOffset(in UiScale start, in UiScale end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().OffsetScale)
                    .Lerp(start, end);
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiRotation>> Spin(in UiRotation start, in UiRotation end)
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().Rotation)
                    .Lerp(start, end);
            }
            return default;
        }

        public AnimationRef<IFieldAnimation<UiRotation>> Spin() => animation.Spin(UiRotation.Zero, UiRotation.Full);
        
        public AnimationRef<IFieldAnimation<UiScale>> PulsePosition(UiScale pulsedSize)
        {
            if (animation.IsValid)
            {
                return animation.ScalePosition(UiScale.Default, pulsedSize).Linear().PingPong();
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiScale>> PulseOffset(UiScale pulsedSize)
        {
            if (animation.IsValid)
            {
                return animation.ScaleOffset(UiScale.Default, pulsedSize).Ease().InOut().PingPong();
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeX()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().OffsetTranslate).ShakeX();
            }
            return default;
        }
        
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeY()
        {
            if (animation.IsValid)
            {
                return animation.AnimateField(a => a.RectTransform.AsTrackable().OffsetTranslate).ShakeY();
            }
            return default;
        }
        
        public UiTuple<AnimationRef<IFieldAnimation<UiTranslate>>, AnimationRef<IFieldAnimation<UiRotation>>> WobblePosition()
        {
            if (animation.IsValid)
            {
                return UiTuple.Create(animation.AnimateField(a => a.RectTransform.AsTrackable().PositionTranslate).Wobble(),
                    animation.AnimateField(a => a.RectTransform.AsTrackable().Rotation).Wobble());
            }
            return default;
        }
        
        public UiTuple<AnimationRef<IFieldAnimation<UiTranslate>>, AnimationRef<IFieldAnimation<UiRotation>>> WobbleOffset()
        {
            if (animation.IsValid)
            {
                return UiTuple.Create(
                    animation.AnimateField(a => a.RectTransform.AsTrackable().OffsetTranslate).Wobble(),
                    animation.AnimateField(a => a.RectTransform.AsTrackable().Rotation).Wobble()
                    );
            }
            return default;
        }
        
        public UiTuple<AnimationRef<IFieldAnimation<UiTranslate>>, AnimationRef<IFieldAnimation<UiRotation>>> HeadShake()
        {
            if (animation.IsValid)
            {
                return UiTuple.Create(
                    animation.AnimateField(a => a.RectTransform.AsTrackable().OffsetTranslate).HeadShake(),
                    animation.AnimateField(a => a.RectTransform.AsTrackable().Rotation).HeadShake()
                    );
            }
            return default;
        }
    }
}