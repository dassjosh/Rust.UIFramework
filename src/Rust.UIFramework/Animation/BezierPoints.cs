namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct BezierPoints(float P1, float P2, float P3, float P4)
{
    public static readonly BezierPoints LINEAR = new BezierPoints(0f, 0f, 0f, 0f);
    public static readonly BezierPoints EASE_IN = new BezierPoints(0.42f, 0f, 1f, 1f);
    public static readonly BezierPoints EASE_OUT = new BezierPoints(0f, 0f, 0.58f, 1f);
    public static readonly BezierPoints EASE_IN_OUT = new BezierPoints(0.42f, 0f, 0.58f, 1f);
}