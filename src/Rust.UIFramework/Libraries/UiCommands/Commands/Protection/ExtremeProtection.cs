using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ExtremeProtection(RegisteredCommand command, float protectionKeyLifetime, bool multiUse) : BaseCommandProtection<ExtremeProtection>(command), ICommandProtection
{
    private readonly UiMemoryCache<long, string> _protectedArgs = new(TimeSpan.FromSeconds(protectionKeyLifetime));

    public override void ProtectCommand(ref UiArgWriter writer)
    {
        long protectionKey = GenerateProtectionKey();
        _protectedArgs[protectionKey] = writer.ToString();
        writer = new UiArgWriter();
        writer.Append(protectionKey.ToBase64Span());
    }

    public override bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        long protectionKey = tokenizer.GetNext().ToLongFromBase64();
        if (!_protectedArgs.TryGetValue(protectionKey, out string args))
        {
            tokenizer = default;
            HandleCallback(player);
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