using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class PermissionHandler(PluginId pluginId, string permission) : IPermissionHandler
{
    public bool HasPermission(BasePlayer player)
    {
        if(OxideLibrary.Permission.UserHasPermission(player.UserIDString, permission))
        {
            return true;
        }

        Singleton<UiCommands>.Instance.OnPlayerNoPermission(pluginId, player, permission);
        return false;
    }
}