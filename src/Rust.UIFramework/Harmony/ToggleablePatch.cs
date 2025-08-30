using System;
using System.Reflection;
using HarmonyLib;

namespace Oxide.Ext.UiFramework.Harmony;

internal class ToggleablePatch(MethodInfo target, HarmonyPatchType type, HarmonyMethod patchMethod)
{
    private MethodInfo _patchedMethod;

    public void Patch()
    {
        if (_patchedMethod == null)
        {
            _patchedMethod = type switch
            {
                HarmonyPatchType.Prefix => UiHarmony.Harmony.Patch(target, prefix: patchMethod),
                HarmonyPatchType.Postfix => UiHarmony.Harmony.Patch(target, postfix: patchMethod),
                HarmonyPatchType.Transpiler => UiHarmony.Harmony.Patch(target, transpiler: patchMethod),
                HarmonyPatchType.Finalizer => UiHarmony.Harmony.Patch(target, finalizer: patchMethod),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }
    
    public void Unpatch()
    {
        if (_patchedMethod != null)
        {
            UiHarmony.Harmony.Unpatch(target, _patchedMethod);
            _patchedMethod = null;
        }
    }
}