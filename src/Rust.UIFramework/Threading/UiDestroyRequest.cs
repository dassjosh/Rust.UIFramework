using Network;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

public class UiDestroyRequest : BaseUiRequest, IUiRequest
{
    public string Name;
    
    public static UiDestroyRequest Create(string name, SendInfo send)
    {
        UiDestroyRequest request = UiPool.Internal.Get<UiDestroyRequest>();
        request.Init(name, send);
        return request;
    }
    
    private void Init(string name, SendInfo send)
    {
        base.Init(send);
        Name = name;
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