using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class PermissionHandler(RegisteredCommand command, string[] permissions, PermissionMode mode, string errorMessage) : IPermissionHandler
{
    private static readonly IUiLogger<PermissionHandler> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<PermissionHandler>();
    
    public bool HasPermission(BasePlayer player) => CheckPlayerPermissions(player);

    public void OnNoPermission(BasePlayer player)
    {
        try
        {
            if (command.OnPlayerNoPermission != null)
            {
                command.OnPlayerNoPermission(player, errorMessage);
                return;
            }

            Singleton<UiCommands>.Instance.OnPlayerNoPermission(command.PluginId, player, command.Delegate.Method, errorMessage);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(OnNoPermission)} An error occured during callback", ex);
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