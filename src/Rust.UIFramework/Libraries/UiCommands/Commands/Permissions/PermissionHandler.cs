using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class PermissionHandler(PluginId pluginId, string method, string[] permissions, PermissionMode mode, string errorMessage, OnPlayerNoPermission noPermission) : IPermissionHandler
{
    private static readonly IUiLogger<PermissionHandler> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<PermissionHandler>();
    
    public bool HasPermission(BasePlayer player)
    {
        if (CheckPlayerPermissions(player))
        {
            return true;
        }

        HandleCallback(player);
        return false;
    }

    private void HandleCallback(BasePlayer player)
    {
        try
        {
            if (noPermission != null)
            {
                noPermission(player, errorMessage);
                return;
            }

            Singleton<UiCommands>.Instance.OnPlayerNoPermission(pluginId, player, method, errorMessage);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(HandleCallback)} An error occured during callback", ex);
        }
    }

    private bool CheckPlayerPermissions(BasePlayer player)
    {
        for (int index = 0; index < permissions.Length; index++)
        {
            string permission = permissions[index];
            bool hasPerm = OxideLibrary.Permission.UserHasPermission(player.UserIDString, permission);
            switch (hasPerm)
            {
                case true when mode == PermissionMode.RequireAny:
                    return true;
                case false when mode == PermissionMode.RequireAll:
                    return false;
            }
        }

        return mode != PermissionMode.RequireAny;
    }
}