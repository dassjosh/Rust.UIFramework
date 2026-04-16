namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct PartialArgs(byte Index, string Args)
{
    public PartialArgs(ArgWriterIterator writer) : this((byte)writer.Index, writer.ToString()) { }
}