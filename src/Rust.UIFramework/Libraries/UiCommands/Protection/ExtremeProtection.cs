using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ExtremeProtection(PluginId pluginId, string method, float protectionKeyLifetime) : ICommandProtection
{
    private readonly Dictionary<long, ProtectedArgs> _protectedArgs = new();
    private readonly Dictionary<long, string> _protectedCommand = new();
    private readonly TimeSpan _keyLifetime = TimeSpan.FromSeconds(protectionKeyLifetime);

    public ArgWriterIterator StartWriteProtection(ArgWriterIterator writer)
    {
        long protectionKey = GenerateProtectionKey();
        writer.WriteNext(protectionKey.ToBase64Span());
        _protectedCommand[protectionKey] = writer.ToString();
        StringBuilder sb = StringBuilderPool.Instance.Get();
        return new ArgWriterIterator(sb, writer.Writers, protectionKey);
    }

    public string FinishWriteProtection(ArgWriterIterator writer)
    {
        _protectedArgs[writer.ProtectionKey] = new ProtectedArgs(writer.ToString(), DateTime.UtcNow + _keyLifetime);
        return _protectedCommand.Remove(writer.ProtectionKey, out string command) ? command : throw new KeyNotFoundException(nameof(writer.ProtectionKey));
    }

    public bool TryValidateProtection(BasePlayer player, UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        long protectionKey = tokenizer.GetNext().ToLongFromBase64Span();
        if (!_protectedArgs.TryGetValue(protectionKey, out ProtectedArgs args) || args.IsExpired)
        {
            protectedTokens = default;
            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(pluginId, player, method);
            return false;
        }

        protectedTokens = new UiCommandTokenizer(args.Args);
        return true;
    }

    private long GenerateProtectionKey()
    {
        _protectedArgs.RemoveAll(pk => pk.Value.Expire < DateTime.UtcNow);
        long value = RandomExt.NextLong();
        while (_protectedArgs.ContainsKey(value))
        {
            value = RandomExt.NextLong();
        }
        
        return value;
    }
    
    private readonly record struct ProtectedArgs(string Args, DateTime Expire)
    {
        public bool IsExpired => Expire < DateTime.UtcNow;
    }
}