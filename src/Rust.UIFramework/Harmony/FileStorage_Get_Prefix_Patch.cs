using System.Reflection;
using HarmonyLib;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Harmony;

internal static class FileStorage_Get_Prefix_Patch
{
    private static NetworkableId _communityEntityId;
    private static readonly UiImageDatabase DB = OxideLibrary.GetLibrary<UiImageDatabase>(nameof(IImageDatabase));
    
    internal static void Patch(ICommunityEntity entity)
    {
#if SERVER
        _communityEntityId = new NetworkableId(entity.Id);
        MethodInfo target = AccessTools.Method(typeof(FileStorage), nameof(FileStorage.Get), [typeof(uint), typeof(FileStorage.Type), typeof(NetworkableId), typeof(uint)]);
        MethodInfo patch = AccessTools.Method(typeof(FileStorage_Get_Prefix_Patch), nameof(FileStorage_Get_Prefix));
        UiHarmony.Harmony.Patch(target, prefix: patch);
#endif
    }
    
    public static bool FileStorage_Get_Prefix(uint crc, NetworkableId entityID, ref byte[] __result)
    {
        if (_communityEntityId != entityID)
        {
            return true;
        }
        
        byte[] image = DB.Get(new ImageId(crc));
        if (image is { Length: > 0 })
        {
            __result = image;
            return false;
        }

        return true;
    }
}