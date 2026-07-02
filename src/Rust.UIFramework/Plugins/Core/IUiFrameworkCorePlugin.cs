namespace Oxide.Ext.UiFramework.Plugins;

public interface IUiFrameworkCorePlugin : IUiFrameworkPlugin
{
    HarmonyLib.Harmony Harmony { get; }
}