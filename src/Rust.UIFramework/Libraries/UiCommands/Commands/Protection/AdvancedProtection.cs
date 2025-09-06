using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class AdvancedProtection(PluginId pluginId, string method, float protectionKeyLifetime, bool multiUse, OnPlayerProtectionFailed onProtectionFailed) : ICommandProtection
{
    private readonly UiMemoryCache<int> _protectionCache = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    
    private static readonly IUiLogger<AdvancedProtection> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<AdvancedProtection>();
    
    public void ProtectCommand(ref UiArgWriter writer)
    {
        writer.Insert(GenerateProtectionKey().ToBase64Span());
    }

    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
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

    private void HandleCallback(BasePlayer player)
    {
        try
        {
            if (onProtectionFailed != null)
            {
                onProtectionFailed(player);
                return;
            }

            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(HandleCallback)} An error occured during callback", ex);
        }
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