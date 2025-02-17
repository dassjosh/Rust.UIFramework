using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class SimpleProtection : ICommandProtection
{
    private readonly string _protectionKey;
    
    public SimpleProtection()
    {
        int value = Core.Random.Range(int.MinValue, int.MaxValue);
        _protectionKey = Convert.ToBase64String(BitConverter.GetBytes(value));
    }
    
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        writer.WriteNext(_protectionKey);
        return writer;
    }

    public string FinishWriteProtection(ArgWriterIterator writer) => writer.ToString();

    public bool TryValidateProtection(UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        if (tokenizer.GetNext().SequenceEqual(_protectionKey))
        {
            protectedTokens = default;
            return false;
        }

        protectedTokens = tokenizer;
        return true;
    }
}