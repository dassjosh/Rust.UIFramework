using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class KeyFrames
{
    public static readonly FrozenKeyFrameAnimator<UiTranslate> ShakeX = new KeyFrameAnimator<UiTranslate>()
        .AddFrames([0, 100], UiTranslate.DistanceDefault)
        .AddFrames([10, 30, 50, 70, 90], UiTranslate.X(-10.Px()))
        .AddFrames([20, 40, 60, 80], UiTranslate.X(10.Px()))
        .Freeze();
    
    public static readonly FrozenKeyFrameAnimator<UiTranslate> ShakeY = new KeyFrameAnimator<UiTranslate>()
        .AddFrames([0, 100], UiTranslate.DistanceDefault)
        .AddFrames([10, 30, 50, 70, 90], UiTranslate.Y(-10.Px()))
        .AddFrames([20, 40, 60, 80], UiTranslate.Y(10.Px()))
        .Freeze();
    
    public static class Wobble
    {
        public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
            .AddFrames([0, 100], UiTranslate.PercentageDefault)
            .AddFrame(15f, UiTranslate.X(-0.25f.Percent()))
            .AddFrame(30f, UiTranslate.X(0.2f.Percent()))
            .AddFrame(45f, UiTranslate.X(-0.15f.Percent()))
            .AddFrame(60f, UiTranslate.X(0.10f.Percent()))
            .AddFrame(75f, UiTranslate.X(-0.05f.Percent()))
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
            .AddFrames([0, 100], UiTranslate.DistanceDefault)
            .AddFrame(13, UiTranslate.X(-6.Px()))
            .AddFrame(37, UiTranslate.X(5.Px()))
            .AddFrame(63f, UiTranslate.X(-3.Px()))
            .AddFrame(87f, UiTranslate.X(2.Px()))
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
            .AddFrame(100, UiScale.Default)
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .AddFrame(80, 0.7.Opacity())
            .AddFrame(100, UiOpacity.Full)
            .Freeze();

        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, UiTranslate.Y(1200.Px()))
                .AddFrame(80, UiTranslate.Y(0.Px()))
                .Freeze();
        }
        
        public static class Up
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, UiTranslate.Y(-1200.Px()))
                .AddFrame(80, UiTranslate.Y(0.Px()))
                .Freeze();
        }
        
        public static class Left
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, UiTranslate.X(-1200.Px()))
                .AddFrame(80, UiTranslate.X(0.Px()))
                .Freeze();
        }
        
        public static class Right
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(0, UiTranslate.X(1200.Px()))
                .AddFrame(80, UiTranslate.X(0.Px()))
                .Freeze();
        }
    }

    public static class BackOut
    {
        public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
            .AddFrame(0, UiScale.Default)
            .AddFrame(20, 0.7.Scale())
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .AddFrame(0, UiOpacity.Full)
            .AddFrame(20, 0.7.Opacity())
            .Freeze();
        
        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, UiTranslate.Y(0.Px()))
                .AddFrame(100, UiTranslate.Y(700.Px()))
                .Freeze();
        }
        
        public static class Up
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, UiTranslate.Y(0.Px()))
                .AddFrame(100, UiTranslate.Y(-700.Px()))
                .Freeze();
        }
        
        public static class Left
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, UiTranslate.X(0.Px()))
                .AddFrame(100, UiTranslate.X(-700.Px()))
                .Freeze();
        }
        
        public static class Right
        {
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .AddFrame(20, UiTranslate.X(0.Px()))
                .AddFrame(100, UiTranslate.X(700.Px()))
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
            .AddFrame(60, UiScale.Default)
            .Freeze();
        
        public static readonly FrozenKeyFrameAnimator<UiOpacity> Opacity = new KeyFrameAnimator<UiOpacity>()
            .WithTiming(Timing)
            .AddFrame(0, UiOpacity.None)
            .AddFrame(60, UiOpacity.Full)
            .Freeze();

        public static class Down
        {
            public static readonly FrozenKeyFrameAnimator<UiScale> Scale = new KeyFrameAnimator<UiScale>()
                .WithTiming(Timing)
                .AddFrame(0, UiScale.Y(3))
                .AddFrame(60, UiScale.Y(0.9f))
                .AddFrame(75, UiScale.Y(0.95f))
                .AddFrame(90, UiScale.Y(0.985f))
                .AddFrame(100, UiScale.Default)
                .Freeze();
            
            public static readonly FrozenKeyFrameAnimator<UiTranslate> Translate = new KeyFrameAnimator<UiTranslate>()
                .WithTiming(Timing)
                .AddFrame(0, UiTranslate.Y(3000.Px()))
                .AddFrame(60, UiTranslate.Y(25.Px()))
                .AddFrame(75, UiTranslate.Y(-10.Px()))
                .AddFrame(90, UiTranslate.Y(5.Px()))
                .AddFrame(100, UiTranslate.Y(0.Px()))
                .Freeze();
        }
    }

}