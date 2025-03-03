using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

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

    public string ProtectCommand(ArgWriterIterator writer)
    {
        writer.Write(_protectionKey);
        return writer.ToString();
    }

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        if (!tokenizer.GetLast().SequenceEqual(_protectionKey))
        {
            protectedTokens = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(_pluginId, player, _method);
            return false;
        }

        protectedTokens = tokenizer;
        return true;
    }
}