namespace Oxide.Ext.UiFramework.Positions;

public abstract class BasePosition
{
    public float XMin;
    public float YMin;
    public float XMax;
    public float YMax;
#if UNIT_TESTS
    internal readonly UiPosition InitialState;
#else
    protected UiPosition InitialState;    
#endif
    
    protected BasePosition(float xMin, float yMin, float xMax, float yMax)
    {
        XMin = xMin;
        YMin = yMin;
        XMax = xMax;
        YMax = yMax;
        InitialState = new UiPosition(XMin, YMin, XMax, YMax);
    }
    
    public virtual void Reset()
    {
        XMin = InitialState.Min.x;
        YMin = InitialState.Min.y;
        XMax = InitialState.Max.x;
        YMax = InitialState.Max.y;
    }

    public virtual void ResetX()
    {
        XMin = InitialState.Min.x;
        XMax = InitialState.Max.x;
    }
    
    public virtual void ResetY()
    {
        YMin = InitialState.Min.y;
        YMax = InitialState.Max.y;
    }

    public override string ToString() => $"{XMin:####} {YMin:####} {XMax:####} {YMax:####}";

    public UiPosition ToPosition() => new(XMin, YMin, XMax, YMax);
    public static implicit operator UiPosition(BasePosition pos) => pos.ToPosition();
}