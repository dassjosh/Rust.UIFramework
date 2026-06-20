using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BasePlayerHandler : IArgHandler<BasePlayer>, ISingleton
{
    private BasePlayerHandler() { }
    
    public BasePlayer Read(in UiStringView view)
    {
        if(view.AsSpan() is UiCommands.NullArg) return default;
        ulong playerId = ulong.Parse(view);
        BasePlayer player = BasePlayer.FindAwakeOrSleepingByID(playerId);
        return player ? player : BasePlayer.FindBot(playerId);
    }

    public void Write(UiArgWriter writer, BasePlayer arg) => writer.Append(arg?.UserIDString ?? UiCommands.NullArg);
}