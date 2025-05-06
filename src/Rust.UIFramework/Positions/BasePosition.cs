namespace Oxide.Ext.UiFramework.Positions;

public abstract class BasePosition
{
    public float XMin;
    public float YMin;
    public float XMax;
    public float YMax;
    protected readonly UiPosition InitialState;

    protected BasePosition(float xMin, float yMin, float xMax, float yMax)
    {
        XMin = xMin;
        YMin = yMin;
        XMax = xMax;
        YMax = yMax;
        InitialState = new UiPosition(XMin, YMin, XMax, YMax);
    }

    public UiPosition ToPosition()
    {
        return new UiPosition(XMin, YMin, XMax, YMax);
    }
        
    public void Reset()
    {
        XMin = InitialState.Min.x;
        YMin = InitialState.Min.y;
        XMax = InitialState.Max.x;
        YMax = InitialState.Max.y;
    }

    public override string ToString()
    {
        return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
    }
        
    public static implicit operator UiPosition(BasePosition pos) => pos.ToPosition();
}