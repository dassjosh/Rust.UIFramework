using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class DestroyUisRequest : BaseUiRequest
{
    public List<string> Names;

    public static DestroyUisRequest Create(IEnumerable<string> names, SendInfo send) => UiPool.Internal.Get<DestroyUisRequest>().Init(names, send);

    private DestroyUisRequest Init(IEnumerable<string> names, SendInfo send)
    {
        base.Init(send);
        Names = names.ToListPooled(PluginPool);
        return this;
    }

    public override ProcessResult Process()
    {
        RpcFunctions.SendDestroyUis(Send, Names);
        Singleton<AnimationTracker>.Instance.RemoveUiForSend(Send, Names);
        return ProcessResult.Success;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        PluginPool.FreeList(Names);
        Names = null;
    }
}