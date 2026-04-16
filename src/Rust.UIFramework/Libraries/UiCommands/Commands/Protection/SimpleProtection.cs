using System;

namespace Oxide.Ext.UiFramework.Libraries;

internal class SimpleProtection(RegisteredCommand command) : BaseCommandProtection<SimpleProtection>(command), ICommandProtection
{
    private readonly string _protectionKey = Convert.ToBase64String(BitConverter.GetBytes(Core.Random.Range(int.MinValue, int.MaxValue)));

    internal string GetProtectionKey() => _protectionKey;

    public override void ProtectCommand(ref UiArgWriter writer)
    {
        writer.Insert(_protectionKey);
    }

    public override bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer)
    {
        if (!tokenizer.GetNext().SequenceEqual(_protectionKey))
        {
            tokenizer = default;
            HandleCallback(player);
            return false;
        }
        
        return true;
    }
}