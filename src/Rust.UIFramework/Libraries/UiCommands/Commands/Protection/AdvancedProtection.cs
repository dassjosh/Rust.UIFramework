using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class AdvancedProtection(PluginId pluginId, string method, float protectionKeyLifetime, bool multiUse) : ICommandProtection
{
    private readonly UiMemoryCache<int> _protectionCache = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    
    public string ProtectCommand(ArgWriterIterator writer)
    {
        writer.Write(GenerateProtectionKey().ToBase64Span());
        return writer.ToString();
    }

    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        int value = tokenizer.GetLast().ToIntFromBase64();
        if (!_protectionCache.ContainsKey(value))
        {
            tokenizer = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
            return false;
        }

        if (!multiUse)
        {
            _protectionCache.Remove(value);
        }
        
        return true;
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