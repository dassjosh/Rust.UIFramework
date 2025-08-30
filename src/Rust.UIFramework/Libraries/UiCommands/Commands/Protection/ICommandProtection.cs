namespace Oxide.Ext.UiFramework.Libraries;

internal interface ICommandProtection
{
    public void ProtectCommand(ref UiArgWriter writer);
    public bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer);
}