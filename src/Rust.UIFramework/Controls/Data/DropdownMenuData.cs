namespace Oxide.Ext.UiFramework.Controls;

public readonly struct DropdownMenuData(string displayName, string commandArgs, bool isActive = false)
{
    public readonly string DisplayName = displayName;
    public readonly string CommandArgs = commandArgs;
    public readonly bool IsActive = isActive;
}