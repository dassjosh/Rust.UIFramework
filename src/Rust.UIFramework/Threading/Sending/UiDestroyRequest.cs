using Network;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

public class UiDestroyRequest : BaseUiRequest, IUiRequest
{
    public string Name;
    
    public static UiDestroyRequest Create(string name, SendInfo send) => UiPool.Internal.Get<UiDestroyRequest>().Init(name, send);

    private UiDestroyRequest Init(string name, SendInfo send)
    {
        base.Init(send);
        Name = name;
        return this;
    }
    
    public void SendRequest()
    {
        RpcFunctions.SendDestroyUi(Send, Name);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Name = null;
    }
}