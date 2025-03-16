namespace Oxide.Ext.UiFramework.Layouts;

public readonly struct LayoutPadding(float horizontal, float vertical)
{
    public readonly float Horizontal = horizontal;
    public readonly float Vertical = vertical;

    public LayoutPadding(float padding) : this(padding, padding) { }
}