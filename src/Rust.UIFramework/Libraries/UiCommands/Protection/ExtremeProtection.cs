using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ExtremeProtection : ICommandProtection
{
    private readonly Dictionary<long, ProtectedArgs> _protectedArgs = new();
    private readonly Dictionary<long, string> _protectedCommand = new();

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
        _protectedArgs[writer.ProtectionKey] = new ProtectedArgs(writer.ToString(), DateTime.UtcNow.AddMinutes(30));
        return _protectedCommand.Remove(writer.ProtectionKey, out string command) ? command : throw new KeyNotFoundException(nameof(writer.ProtectionKey));
    }

    public bool TryValidateProtection(UiCommandTokenizer tokenizer, out UiCommandTokenizer protectedTokens)
    {
        long protectionKey = tokenizer.GetNext().ToLongFromBase64Span();
        if (!_protectedArgs.TryGetValue(protectionKey, out ProtectedArgs args) || args.IsExpired)
        {
            protectedTokens = default;
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