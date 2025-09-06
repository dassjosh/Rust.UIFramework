using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class SimpleProtection : ICommandProtection
{
    private readonly PluginId _pluginId;
    private readonly string _method;
    private readonly string _protectionKey;
    private readonly OnPlayerProtectionFailed _onProtectionFailed;
    
    public SimpleProtection(PluginId pluginId, string method, OnPlayerProtectionFailed onProtectionFailed)
    {
        _pluginId = pluginId;
        _method = method;
        _onProtectionFailed = onProtectionFailed;
        int value = Core.Random.Range(int.MinValue, int.MaxValue);
        _protectionKey = Convert.ToBase64String(BitConverter.GetBytes(value));
    }
    
    internal string GetProtectionKey() => _protectionKey;

    public void ProtectCommand(ref UiArgWriter writer)
    {
        writer.Insert(_protectionKey);
    }

    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        if (!tokenizer.GetNext().SequenceEqual(_protectionKey))
        {
            tokenizer = default;

            if (_onProtectionFailed != null)
            {
                _onProtectionFailed(player);
                return false;
            }
            
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(_pluginId, player, _method);
            return false;
        }
        
        return true;
    }
}