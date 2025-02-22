using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.HarmonyPatches;

internal static class CuiHelper_AddUi_Patch
{
    private static readonly MethodInfo[] TargetMethods = [
        typeof(CuiHelper).GetMethod(nameof(CuiHelper.AddUi), [typeof(BasePlayer), typeof(List<CuiElement>)]), 
        typeof(CuiHelper).GetMethod(nameof(CuiHelper.AddUi), [typeof(BasePlayer), typeof(string)])
    ];
    private static readonly MethodInfo[] PatchMethods = new MethodInfo[2];
    
    internal static void Patch()
    {
        if (!UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod && PatchMethods.All(pm => pm == null))
        {
            PatchMethods[0] = UiHarmony.Harmony.Patch(TargetMethods[0], prefix: new HarmonyMethod(typeof(CuiHelper_AddUi_Patch), nameof(CuiHelper_AddUi_Prefix_Elements)));
            PatchMethods[1] = UiHarmony.Harmony.Patch(TargetMethods[1], prefix: new HarmonyMethod(typeof(CuiHelper_AddUi_Patch), nameof(CuiHelper_AddUi_Prefix_Json)));
        }
    }

    internal static void Unpatch()
    {
        for (int i = 0; i < TargetMethods.Length; i++)
        {
            UiHarmony.Harmony.Unpatch(TargetMethods[i], PatchMethods[i]);
            PatchMethods[i] = null;
        }
    }

    internal static void ToggleState(bool enabled)
    {
        if (enabled)
        {
            Patch();
        }
        else
        {
            Unpatch();
        }
    }
    
    private static bool CuiHelper_AddUi_Prefix_Elements(BasePlayer player, List<CuiElement> elements)
    {
        OxideCuiElementsRequest request = OxideCuiElementsRequest.Create(elements, SendInfoBuilder.Get(player));
        SendHandler.Enqueue(request);
        return false;
    }
    
    private static bool CuiHelper_AddUi_Prefix_Json(BasePlayer player, string json)
    {
        OxideCuiJsonRequest request = OxideCuiJsonRequest.Create(json, SendInfoBuilder.Get(player));
        SendHandler.Enqueue(request);
        return false;
    }
}