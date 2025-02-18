using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class AdvancedProtection(PluginId pluginId, string method, float protectionKeyLifetime) : ICommandProtection
{
    private readonly Dictionary<int, DateTime> _protectionKeys = new();
    private readonly TimeSpan _keyLifetime = TimeSpan.FromSeconds(protectionKeyLifetime);
    
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        writer.WriteNext(GenerateProtectionKey().ToBase64Span());
        return writer;
    }

    public string FinishWriteProtection(ArgWriterIterator writer) => writer.ToString();

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        int value = tokenizer.GetNext().ToIntFromBase64Span();
        if (!_protectionKeys.Remove(value, out DateTime expiration))
        {
            protectedTokens = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
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

        _protectionKeys[value] = DateTime.UtcNow + _keyLifetime;
        return value;
    }
}