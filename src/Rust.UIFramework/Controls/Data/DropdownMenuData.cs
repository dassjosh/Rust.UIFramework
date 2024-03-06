namespace Oxide.Ext.UiFramework.Controls.Data
{
    public readonly struct DropdownMenuData
    {
        public readonly string DisplayName;
        public readonly string CommandArgs;
        public readonly bool IsActive;

        public DropdownMenuData(string displayName, string commandArgs, bool isActive = false)
        {
            DisplayName = displayName;
            CommandArgs = commandArgs;
            IsActive = isActive;
        }
    }
}