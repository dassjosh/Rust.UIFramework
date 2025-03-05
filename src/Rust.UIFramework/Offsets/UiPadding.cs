namespace Oxide.Ext.UiFramework.Offsets;

public readonly struct UiPadding(float left, float bottom, float right, float top)
{
    public static readonly UiPadding None = new(0);
    
    public readonly float Left = left;
    public readonly float Top = top;
    public readonly float Right = right;
    public readonly float Bottom = bottom;

    public UiPadding(float horizontal, float vertical) : this(horizontal, vertical, horizontal, vertical) {}

    public UiPadding(float padding) : this(padding, padding, padding, padding) {}
    
    public static implicit operator UiOffset(UiPadding padding) => padding.ToOffset();
    public UiOffset ToOffset() => new(Left, Bottom, -Right, -Top);
}