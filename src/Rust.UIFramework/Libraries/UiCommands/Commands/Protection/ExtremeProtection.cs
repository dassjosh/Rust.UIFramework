using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ExtremeProtection(PluginId pluginId, string method, float protectionKeyLifetime) : ICommandProtection
{
    private readonly UiMemoryCache<long, string> _protectedArgs = new(TimeSpan.FromSeconds(protectionKeyLifetime));
    private readonly Dictionary<long, string> _protectedCommand = new();

    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        long protectionKey = GenerateProtectionKey();
        writer.Write(protectionKey.ToBase64Span());
        _protectedCommand[protectionKey] = writer.ToString();
        return new ArgWriterIterator(writer, protectionKey);
    }

    public string FinishWriteProtection(ArgWriterIterator writer)
    {
        _protectedArgs[writer.ProtectionKey] = writer.ToString();
        return _protectedCommand.Remove(writer.ProtectionKey, out string command) ? command : throw new KeyNotFoundException(nameof(writer.ProtectionKey));
    }

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        long protectionKey = tokenizer.GetNext().ToLongFromBase64();
        if (!_protectedArgs.TryRemove(protectionKey, out string args))
        {
            protectedTokens = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
            return false;
        }

        protectedTokens = new UiCommandTokenizer(args);
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