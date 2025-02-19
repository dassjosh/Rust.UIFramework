using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Oxide.Ext.UiFramework.Builders;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.HarmonyPatches;

internal static class CuiHelper_AddUi_Patch
{
    private static readonly MethodInfo TargetMethod = typeof(CuiHelper).GetMethod(nameof(CuiHelper.AddUi), [typeof(BasePlayer), typeof(List<CuiElement>)]);
    private static MethodInfo PatchMethod;
    
    internal static void Patch(Harmony harmony)
    {
        if (UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod)
        {
            PatchMethod = harmony.Patch(TargetMethod, 
                prefix: new HarmonyMethod(typeof(CuiHelper_AddUi_Patch), nameof(CuiHelper_AddUi_Patch.CuiHelper_AddUi_Prefix)));
        }
    }

    internal static void Unpatch(Harmony harmony)
    {
        if (PatchMethod != null)
        {
            harmony.Unpatch(TargetMethod, PatchMethod);
        }
    }
    
    private static bool CuiHelper_AddUi_Prefix(BasePlayer player, List<CuiElement> elements)
    {
        OxideCuiElementsRequest request = OxideCuiElementsRequest.Create(elements, SendInfoBuilder.Get(player));
        SendHandler.Enqueue(request);
        return false;
    }
}