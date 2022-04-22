namespace Oxide.Ext.UiFramework.Positions
{
    public abstract class UiPosition
    {
        public static readonly UiPosition FullPosition = new StaticUiPosition(0, 0, 1, 1);
        public static readonly UiPosition TopLeft = new StaticUiPosition(0, 1, 0, 1);
        public static readonly UiPosition MiddleLeft = new StaticUiPosition(0, .5f, 0, .5f);
        public static readonly UiPosition BottomLeft = new StaticUiPosition(0, 0, 0, 0);
        public static readonly UiPosition TopMiddle = new StaticUiPosition(.5f, 1, .5f, 1);
        public static readonly UiPosition MiddleMiddle = new StaticUiPosition(.5f, .5f, .5f, .5f);
        public static readonly UiPosition BottomMiddle = new StaticUiPosition(.5f, 0, .5f, 0);
        public static readonly UiPosition TopRight = new StaticUiPosition(1, 1, 1, 1);
        public static readonly UiPosition MiddleRight = new StaticUiPosition(1, .5f, 1, .5f);
        public static readonly UiPosition BottomRight = new StaticUiPosition(1, 0, 1, 0);
        
        public static readonly UiPosition Top = new StaticUiPosition(0, 1, 1, 1);
        public static readonly UiPosition Bottom = new StaticUiPosition(0, 0, 1, 0);
        public static readonly UiPosition Left = new StaticUiPosition(0, 0, 0, 1);
        public static readonly UiPosition Right = new StaticUiPosition(1, 0, 1, 1);

        public abstract Position ToPosition();
    }
}