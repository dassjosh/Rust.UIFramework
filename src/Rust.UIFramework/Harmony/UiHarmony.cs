namespace Oxide.Ext.UiFramework.Harmony;

internal static class UiHarmony
{
    internal static readonly HarmonyLib.Harmony Harmony = new(UiFrameworkExtension.Instance.Name); 
    internal static void Initialize()
    {
        CuiHelper_AddUi_Patch.Patch();
    }
}