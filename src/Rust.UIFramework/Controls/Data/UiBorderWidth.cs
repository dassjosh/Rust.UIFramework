namespace Oxide.Ext.UiFramework.Controls.Data;

public readonly struct UiBorderWidth(float left, float top, float right, float bottom)
{
    public static readonly UiBorderWidth Default = new(1);
        
    public readonly float Left = left;
    public readonly float Top = top;
    public readonly float Right = right;
    public readonly float Bottom = bottom;

    public UiBorderWidth(float width, float height) : this(width, height, width, height) { }
        
    public UiBorderWidth(float width) : this(width, width) { }

    public bool IsEmpty() => Left == 0 || Top == 0 || Right == 0 || Bottom == 0;
}