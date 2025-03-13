using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BasePlayerHandler : IArgHandler<BasePlayer>, ISingleton
{
    private BasePlayerHandler() { }
    
    public BasePlayer Read(ReadOnlySpan<char> arg)
    {
        if(arg is UiCommands.NullArg) return default;
        ulong playerId = ulong.Parse(arg);
        BasePlayer player = BasePlayer.FindAwakeOrSleepingByID(playerId);
        return player ? player : BasePlayer.FindBot(playerId);
    }

    public void Write(UiArgWriter writer, BasePlayer arg) => writer.Append(arg?.UserIDString ?? UiCommands.NullArg);
}