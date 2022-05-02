namespace Oxide.Ext.UiFramework.Offsets
{
    public abstract class UiOffset
    {
        public static readonly UiOffset DefaultOffset = new StaticUiOffset(0, 0, 0, 0);

        public abstract Offset ToOffset();
    }
}