using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class AdvancedProtection(PluginId pluginId, string method, float protectionKeyLifetime) : ICommandProtection
{
    private readonly UiMemoryCache<int> _protectionCache = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    
    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        writer.Write(GenerateProtectionKey().ToBase64Span());
        return writer;
    }

    public string FinishWriteProtection(ArgWriterIterator writer) => writer.ToString();

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        int value = tokenizer.GetNext().ToIntFromBase64();
        if (_protectionCache.TryRemove(value))
        {
            protectedTokens = tokenizer;
            return true;
        }

        protectedTokens = default;
        Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
        return false;
    }
    
    private int GenerateProtectionKey()
    {
        int value = Core.Random.Range(int.MinValue, int.MaxValue);
        while (!_protectionCache.TryAdd(value))
        {
            value = Core.Random.Range(int.MinValue, int.MaxValue);
        }
        
        return value;
    }
}