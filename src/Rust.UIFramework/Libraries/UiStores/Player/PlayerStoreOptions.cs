namespace Oxide.Ext.UiFramework.Libraries;

public class PlayerStoreOptions
{
    internal static readonly PlayerStoreOptions Default = new();

    public bool RemoveOnDisconnect { get; set; } = true;
}