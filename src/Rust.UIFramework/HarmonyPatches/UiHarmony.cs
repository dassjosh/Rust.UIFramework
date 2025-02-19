using HarmonyLib;

namespace Oxide.Ext.UiFramework.HarmonyPatches;

internal static class UiHarmony
{
    internal static readonly Harmony Harmony = new(UiFrameworkExtension.Instance.Name); 
    internal static void Initialize()
    {
        CuiHelper_AddUi_Patch.Patch();
    }
}