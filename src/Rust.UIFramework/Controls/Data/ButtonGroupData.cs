namespace Oxide.Ext.UiFramework.Controls.Data;

public readonly struct ButtonGroupData(string displayName, string command, bool isActive = false)
{
    public readonly string DisplayName = displayName;
    public readonly string Command = command;
    public readonly bool IsActive = isActive;
}