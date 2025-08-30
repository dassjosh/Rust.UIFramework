using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class SimpleProtection : ICommandProtection
{
    private readonly PluginId _pluginId;
    private readonly string _method;
    private readonly string _protectionKey;
    
    public SimpleProtection(PluginId pluginId, string method)
    {
        _pluginId = pluginId;
        _method = method;
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
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(_pluginId, player, _method);
            return false;
        }
        
        return true;
    }
}