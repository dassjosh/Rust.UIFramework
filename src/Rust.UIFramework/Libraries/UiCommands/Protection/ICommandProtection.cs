namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandProtection
{
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer);
    public string FinishWriteProtection(ArgWriterIterator writer);
    public bool TryValidateProtection(UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens);
}