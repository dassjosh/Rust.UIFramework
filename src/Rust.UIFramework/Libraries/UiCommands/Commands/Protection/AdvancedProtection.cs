using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;

internal class AdvancedProtection(RegisteredCommand command, float protectionKeyLifetime, bool multiUse) : BaseCommandProtection<AdvancedProtection>(command), ICommandProtection
{
    private readonly UiMemoryCache<int> _protectionCache = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    
    public override void ProtectCommand(ref UiArgWriter writer)
    {
        writer.Insert(GenerateProtectionKey().ToBase64Span());
    }

    public override bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        int value = tokenizer.GetNext().ToIntFromBase64();
        if (!_protectionCache.ContainsKey(value))
        {
            tokenizer = default;
            HandleCallback(player);
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