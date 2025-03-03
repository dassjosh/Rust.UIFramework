using System;
using System.Text;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ExtremeProtection(PluginId pluginId, string method, float protectionKeyLifetime, string protectingStringPrefix, bool multiUse) : ICommandProtection
{
    private readonly UiMemoryCache<long, string> _protectedArgs = new(TimeSpan.FromSeconds(protectionKeyLifetime));

    public string ProtectCommand(ArgWriterIterator writer)
    {
        long protectionKey = GenerateProtectionKey();
        _protectedArgs[protectionKey] = writer.ToString();
        StringBuilder sb = StringBuilderPool.Instance.Get();
        sb.Append(protectingStringPrefix);
        sb.Append(' ');
        sb.Append(protectionKey.ToBase64Span());
        return sb.ToStringAndFree();
    }

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        long protectionKey = tokenizer.GetLast().ToLongFromBase64();
        if (!_protectedArgs.TryGetValue(protectionKey, out string args))
        {
            protectedTokens = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
            return false;
        }

        if (!multiUse)
        {
            _protectedArgs.Remove(protectionKey);
        }

        protectedTokens = new UiCommandTokenizer(args);
        protectedTokens.SkipNext(2);  //Skip Command Prefix & Command ID
        return true;
    }

    private long GenerateProtectionKey()
    {
        long value = RandomExt.NextLong();
        while (!_protectedArgs.ContainsKey(value))
        {
            value = RandomExt.NextLong();
        }
        
        return value;
    }
}