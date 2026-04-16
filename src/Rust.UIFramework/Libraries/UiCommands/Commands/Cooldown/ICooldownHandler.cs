namespace Oxide.Ext.UiFramework.Libraries;

public interface ICooldownHandler
{
    bool IsOnCooldown(BasePlayer player);
    float GetRemainingSeconds(BasePlayer player);
    void OnCooldown(BasePlayer player, float remaining);
}