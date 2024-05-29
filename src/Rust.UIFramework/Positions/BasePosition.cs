namespace Oxide.Ext.UiFramework.Positions;

public abstract class BasePosition
{
    public float XMin;
    public float YMin;
    public float XMax;
    public float YMax;
    private readonly UiPosition _initialState;

    protected BasePosition(float xMin, float yMin, float xMax, float yMax)
    {
        XMin = xMin;
        YMin = yMin;
        XMax = xMax;
        YMax = yMax;
        _initialState = new UiPosition(XMin, YMin, XMax, YMax);
    }

    public UiPosition ToPosition()
    {
        return new UiPosition(XMin, YMin, XMax, YMax);
    }
        
    public void Reset()
    {
        XMin = _initialState.Min.x;
        YMin = _initialState.Min.y;
        XMax = _initialState.Max.x;
        YMax = _initialState.Max.y;
    }

    public override string ToString()
    {
        return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
    }
        
    public static implicit operator UiPosition(BasePosition pos) => pos.ToPosition();
}