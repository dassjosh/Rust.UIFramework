namespace Oxide.Ext.UiFramework.Libraries;

public interface IPermissionHandler
{
    bool HasPermission(BasePlayer player);
    void OnNoPermission(BasePlayer player);
}