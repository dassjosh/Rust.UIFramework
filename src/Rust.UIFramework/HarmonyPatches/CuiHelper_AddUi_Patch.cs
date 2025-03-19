using System.Collections.Generic;
using HarmonyLib;
using Oxide.Core;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.HarmonyPatches;

internal static class CuiHelper_AddUi_Patch
{
    private static readonly ToggleablePatch[] Patches =
    [
        new(typeof(CuiHelper).GetMethod(nameof(CuiHelper.AddUi), [typeof(BasePlayer), typeof(List<CuiElement>)]), 
            HarmonyPatchType.Prefix, 
            new HarmonyMethod(typeof(CuiHelper_AddUi_Patch), nameof(CuiHelper_AddUi_Prefix_Elements))),
        
        new(typeof(CuiHelper).GetMethod(nameof(CuiHelper.AddUi), [typeof(BasePlayer), typeof(string)]), 
            HarmonyPatchType.Prefix, 
            new HarmonyMethod(typeof(CuiHelper_AddUi_Patch), nameof(CuiHelper_AddUi_Prefix_Json)))
    ];
    
    internal static void Patch()
    {
        if (!UiFrameworkConfig.Instance.Harmony.PatchAddUiMethod)
        {
            foreach (ToggleablePatch patch in Patches)
            {
                patch.Patch();
            }
        }
    }

    private static void Unpatch()
    {
        foreach (ToggleablePatch patch in Patches)
        {
            patch.Unpatch();
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
        if (player?.net == null || Interface.CallHook("CanUseUI", player, elements) != null)
        {
            return false;
        }
        OxideCuiElementsRequest request = OxideCuiElementsRequest.Create(elements, SendInfoBuilder.Get(player));
        Singleton<SendHandler>.Instance.Enqueue(request);
        return false;
    }
    
    private static bool CuiHelper_AddUi_Prefix_Json(BasePlayer player, string json)
    {
        if (player?.net == null || Interface.CallHook("CanUseUI", player, json) != null)
        {
            return false;
        }
        OxideCuiJsonRequest request = OxideCuiJsonRequest.Create(json, SendInfoBuilder.Get(player));
        Singleton<SendHandler>.Instance.Enqueue(request);
        return false;
    }
}