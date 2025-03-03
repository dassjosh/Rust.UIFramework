namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandProtection
{
    public string ProtectCommand(ArgWriterIterator writer);
    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens);
}