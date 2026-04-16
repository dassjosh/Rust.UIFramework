using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class KeyFrames
{
    public static readonly FrozenKeyFrameAnimator<UiTranslate> ShakeX = new KeyFrameAnimator<UiTranslate>()
        .AddFrames([0, 100], 0.Px().X())
        .AddFrames([10, 30, 50, 70, 90], -10.Px().X())
        .AddFrames([20, 40, 60, 80], 10.Px().X())
        .Freeze();
    
    public static readonly FrozenKeyFrameAnimator<UiTranslate> ShakeY = new KeyFrameAnimator<UiTranslate>()
        .AddFrames([0, 100], 0.Px().Y())
        .AddFrames([10, 30, 50, 70, 90], -10.Px().Y())
        .AddFrames([20, 40, 60, 80], 10.Px().Y())
        .Freeze();
    
    public static class Wobble
    {
        public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
            .AddFrames([0, 100], 0.Percent().Y())
            .AddFrame(15f, -0.25f.Percent().X())
            .AddFrame(30f, 0.2f.Percent().X())
            .AddFrame(45f, -0.15f.Percent().X())
            .AddFrame(60f, 0.10f.Percent().X())
            .AddFrame(75f, -0.05f.Percent().X())
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiRotation> Rotation = new KeyFrameAnimator<UiRotation>()
            .AddFrames([0, 100], 0.Degrees())
            .AddFrame(15f, -5.Degrees())
            .AddFrame(30f, 3.Degrees())
            .AddFrame(45f, -3.Degrees())
            .AddFrame(60f, 2.Degrees())
            .AddFrame(75f, -1.Degrees())
            .Freeze();
    }

    public static class HeadShake
    {
        public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
            .AddFrames([0, 100], 0.Px().X())
            .AddFrame(13, -6.Px().X())
            .AddFrame(37, 5.Px().X())
            .AddFrame(63f, -3.Px().X())
            .AddFrame(87f, 2.Px().X())
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiRotation> Rotation = new KeyFrameAnimator<UiRotation>()
            .AddFrames([0, 100], 0.Degrees())
            .AddFrame(13, -9.Degrees())
            .AddFrame(37, 7.Degrees())
            .AddFrame(63f, -5.Degrees())
            .AddFrame(87f, 3.Degrees())
            .Freeze();
    }

    public static class BackIn
    {
        public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
            .AddFrame(80, 0.7.Scale())
            .AddFrame(100, UiScale.One)
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .AddFrame(80, 0.7.Opacity())
            .AddFrame(100, UiOpacity.Opaque)
            .Freeze();

        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, 1200.Px().Y())
                .AddFrame(80, 0.Px().Y())
                .Freeze();
        }
        
        public static class Up
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0,-1200.Px().Y())
                .AddFrame(80, 0.Px().Y())
                .Freeze();
        }
        
        public static class Left
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, -1200.Px().X())
                .AddFrame(80, 0.Px().X())
                .Freeze();
        }
        
        public static class Right
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, 1200.Px().X())
                .AddFrame(80, 0.Px().X())
                .Freeze();
        }
    }

    public static class BackOut
    {
        public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
            .AddFrame(0, UiScale.One)
            .AddFrame(20, 0.7.Scale())
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .AddFrame(0, UiOpacity.Opaque)
            .AddFrame(20, 0.7.Opacity())
            .Freeze();
        
        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, 0.Px().Y())
                .AddFrame(100, 700.Px().Y())
                .Freeze();
        }
        
        public static class Up
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, 0.Px().Y())
                .AddFrame(100, -700.Px().Y())
                .Freeze();
        }
        
        public static class Left
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, 0.Px().X())
                .AddFrame(100, -700.Px().X())
                .Freeze();
        }
        
        public static class Right
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, 0.Px().X())
                .AddFrame(100, 700.Px().X())
                .Freeze();
        }
    }

    public static class BounceIn
    {
        public static readonly TimingFunction Timing = new CubicBezier(0.215, 0.61, 0.355, 1);
        
        public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
            .WithTiming(Timing)
            .AddFrame(0, 0.3.Scale())
            .AddFrame(20, 1.1.Scale())
            .AddFrame(40, 0.9.Scale())
            .AddFrame(60, 1.03.Scale())
            .AddFrame(60, 0.97.Scale())
            .AddFrame(60, UiScale.One)
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .WithTiming(Timing)
            .AddFrame(0, UiOpacity.Transparent)
            .AddFrame(60, UiOpacity.Opaque)
            .Freeze();

        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
                .WithTiming(Timing)
                .AddFrame(0, 3.Scale().Y())
                .AddFrame(60, 0.9f.Scale().Y())
                .AddFrame(75, 0.95f.Scale().Y())
                .AddFrame(90, 0.985f.Scale().Y())
                .AddFrame(100, 1.Scale())
                .Freeze();
            
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .WithTiming(Timing)
                .AddFrame(0, 3000.Px().Y())
                .AddFrame(60, 25.Px().Y())
                .AddFrame(75, -10.Px().Y())
                .AddFrame(90, 5.Px().Y())
                .AddFrame(100, 0.Px().Y())
                .Freeze();
        }
    }

}