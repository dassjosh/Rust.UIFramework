namespace UI.Framework.Rust.Positions
{
    public abstract class UiPosition
    {
        public static readonly UiPosition FullPosition = new StaticUiPosition(0, 0, 1, 1);
        public static readonly UiPosition Center = new StaticUiPosition(.5f, .5f, .5f, .5f);

        public abstract Position ToPosition();
    }
}