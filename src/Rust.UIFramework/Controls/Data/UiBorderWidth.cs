namespace Oxide.Ext.UiFramework.Controls.Data
{
    public readonly struct UiBorderWidth
    {
        public static readonly UiBorderWidth Default = new(1);
        
        public readonly float Left;
        public readonly float Top;
        public readonly float Right;
        public readonly float Bottom;

        public UiBorderWidth(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
        
        public UiBorderWidth(float width, float height) : this(width, height, width, height) { }
        
        public UiBorderWidth(float width) : this(width, width) { }

        public bool IsEmpty() => Left == 0 || Top == 0 || Right == 0 || Bottom == 0;
    }
}