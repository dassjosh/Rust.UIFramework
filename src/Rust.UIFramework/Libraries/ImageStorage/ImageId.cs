namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct ImageId(string Id)
{
    public bool IsValid => !string.IsNullOrEmpty(Id);
}