using Network;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiDestroyRequest : BaseUiRequest
{
    public string Name;
    
    public static UiDestroyRequest Create(string name, SendInfo send) => UiPool.Internal.Get<UiDestroyRequest>().Init(name, send);

    private UiDestroyRequest Init(string name, SendInfo send)
    {
        base.Init(send);
        Name = name;
        return this;
    }
    
    public override ProcessResult Process()
    {
        RpcFunctions.SendDestroyUi(Send, Name);
        return ProcessResult.Success;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Name = null;
    }
}