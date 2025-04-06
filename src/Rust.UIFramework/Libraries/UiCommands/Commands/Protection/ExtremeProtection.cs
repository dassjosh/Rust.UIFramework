using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ExtremeProtection(PluginId pluginId, string method, float protectionKeyLifetime, bool multiUse) : ICommandProtection
{
    private readonly UiMemoryCache<long, string> _protectedArgs = new(TimeSpan.FromSeconds(protectionKeyLifetime));

    public void ProtectCommand(string command, ref UiArgWriter writer)
    {
        long protectionKey = GenerateProtectionKey();
        _protectedArgs[protectionKey] = writer.ToString();
        writer = new UiArgWriter(StringBuilderPool.Instance.Get());
        writer.Append(command);
        writer.AppendSpace();
        writer.Append(protectionKey.ToBase64Span());
    }

    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        long protectionKey = tokenizer.GetNext().ToLongFromBase64();
        if (!_protectedArgs.TryGetValue(protectionKey, out string args))
        {
            tokenizer = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
            return false;
        }

        if (!multiUse)
        {
            _protectedArgs.Remove(protectionKey);
        }

        tokenizer = new UiCommandTokenizer(args);
        return true;
    }

    private long GenerateProtectionKey()
    {
        long value = RandomExt.NextLong();
        while (_protectedArgs.ContainsKey(value))
        {
            value = RandomExt.NextLong();
        }
        
        return value;
    }
}