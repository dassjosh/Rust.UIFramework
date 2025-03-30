namespace Oxide.Ext.UiFramework.Controls.Data;

public readonly struct ButtonGroupData(string displayName, string commandArgs, bool isActive = false)
{
    public readonly string DisplayName = displayName;
    public readonly string CommandArgs = commandArgs;
    public readonly bool IsActive = isActive;
}