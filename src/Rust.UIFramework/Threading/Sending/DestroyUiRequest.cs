using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class DestroyUiRequest : BaseUiRequest
{
    public string Name;
    
    public static DestroyUiRequest Create(string name, SendInfo send) => UiPool.Internal.Get<DestroyUiRequest>().Init(name, send);

    private DestroyUiRequest Init(string name, SendInfo send)
    {
        base.Init(send);
        Name = name;
        return this;
    }
    
    public override ProcessResult Process()
    {
        RpcFunctions.SendDestroyUi(Send, Name);
        Singleton<AnimationTracker>.Instance.RemoveUiForSend(Send, Name);
        return ProcessResult.Success;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Name = null;
    }
}