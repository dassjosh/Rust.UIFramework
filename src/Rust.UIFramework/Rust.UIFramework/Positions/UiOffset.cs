namespace UI.Framework.Rust.Positions
{
    public abstract class UiOffset
    {
        public static readonly UiOffset DefaultOffset = new StaticUiOffset(0, 0, 0, 0);
        public static readonly Offset Default = new Offset(0, 0, 0, 0);

        public abstract Offset ToOffset();
    }
}