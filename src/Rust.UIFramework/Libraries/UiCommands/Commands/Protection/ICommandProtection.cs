namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandProtection
{
    public string ProtectCommand(ArgWriterIterator writer);
    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer);
}