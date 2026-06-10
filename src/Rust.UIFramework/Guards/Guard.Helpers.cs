namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    private static string Message(string message) => $"UiFramework Extension Guard ({UiFrameworkExtension.Instance.Version}): {message}";
}