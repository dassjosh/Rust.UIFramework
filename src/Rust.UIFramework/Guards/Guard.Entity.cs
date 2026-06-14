using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Guards;

public static partial class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IsCommunityEntityReady()
    {
#if SERVER
        if (!CommunityEntity.ServerInstance.IsValid()) throw new CommunityEntityNotReadyException();
#endif
    }
}