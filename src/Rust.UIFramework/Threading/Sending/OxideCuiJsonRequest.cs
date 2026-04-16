using Network;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class OxideCuiJsonRequest : BaseUiRequest, IUiRequest
{
    private string _json;
    
    public static OxideCuiJsonRequest Create(string json, SendInfo send) => UiPool.Internal.Get<OxideCuiJsonRequest>().Init(json, send);

    private OxideCuiJsonRequest Init(string json, SendInfo send)
    {
        base.Init(send);
        _json = json;
        return this;
    }
    
    public void SendRequest()
    {
        CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("AddUI", Send), _json);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _json = null;
    }
}