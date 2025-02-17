using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class AdvancedProtection : ICommandProtection
{
    private readonly Dictionary<int, DateTime> _protectionKeys = new();
    
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        writer.WriteNext(GenerateProtectionKey().ToBase64Span());
        return writer;
    }

    public string FinishWriteProtection(ArgWriterIterator writer) => writer.ToString();

    public bool TryValidateProtection(UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        int value = tokenizer.GetNext().ToIntFromBase64Span();
        if (!_protectionKeys.Remove(value, out DateTime expiration))
        {
            protectedTokens = default;
            return false;
        }
        
        protectedTokens = tokenizer;
        return expiration >= DateTime.UtcNow;
    }
    
    private int GenerateProtectionKey()
    {
        _protectionKeys.RemoveAll(pk => pk.Value < DateTime.UtcNow);
        int value = Core.Random.Range(int.MinValue, int.MaxValue);
        while (_protectionKeys.ContainsKey(value))
        {
            value = Core.Random.Range(int.MinValue, int.MaxValue);
        }

        _protectionKeys[value] = DateTime.UtcNow.AddHours(1);
        return value;
    }
}