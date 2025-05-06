using Network;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Threading;

public class OxideCuiRequest : BaseUiRequest, IUiRequest
{
    private CuiElementContainer _container;
    private string _destroyUiName;
    
    public static OxideCuiRequest Create(CuiElementContainer container, SendInfo send, string destroyUiName)
    {
        OxideCuiRequest request = UiFrameworkPool.Get<OxideCuiRequest>();
        request.Init(container, send, destroyUiName);
        return request;
    }
    
    private void Init(CuiElementContainer container, SendInfo send, string destroyUiName)
    {
        base.Init(send);
        _container = container;
        _destroyUiName = destroyUiName;
    }
    
    public void SendUi()
    {
        if (!string.IsNullOrEmpty(_destroyUiName))
        {
            CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("DestroyUI", Send));
        }
        CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("AddUI", Send), _container.ToJson());
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _container = null;
        _destroyUiName = null;
    }
}