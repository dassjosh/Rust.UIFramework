using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ExtremeProtection(PluginId pluginId, string method, float protectionKeyLifetime, bool multiUse, OnPlayerProtectionFailed onProtectionFailed) : ICommandProtection
{
    private readonly UiMemoryCache<long, string> _protectedArgs = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    private static readonly IUiLogger<ExtremeProtection> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ExtremeProtection>();

    public void ProtectCommand(ref UiArgWriter writer)
    {
        long protectionKey = GenerateProtectionKey();
        _protectedArgs[protectionKey] = writer.ToString();
        writer = new UiArgWriter(UiPool.Internal.GetStringBuilder());
        writer.Append(protectionKey.ToBase64Span());
    }

    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
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