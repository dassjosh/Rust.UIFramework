using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Padding;

public readonly record struct UiPadding(float Left, float Bottom, float Right, float Top)
{
    public static readonly UiPadding None = new(0);
    
    public bool IsSingleValue => Left == Bottom && Left == Right && Left == Top;

    public UiPadding(float horizontal, float vertical) : this(horizontal, vertical, horizontal, vertical) {}

    public UiPadding(float padding) : this(padding, padding, padding, padding) {}
    
    public static implicit operator UiOffset(UiPadding padding) => padding.ToOffset();
    public static implicit operator UiPosition(UiPadding padding) => padding.ToPosition();
    
    public UiOffset ToOffset() => new(Left, Bottom, -Right, -Top);
    public UiPosition ToPosition() => new(Left, Bottom, -Right, -Top);
    
    public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
}