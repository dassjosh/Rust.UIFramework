namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandProtection
{
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer);
    public string FinishWriteProtection(ArgWriterIterator writer);
    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens);
}