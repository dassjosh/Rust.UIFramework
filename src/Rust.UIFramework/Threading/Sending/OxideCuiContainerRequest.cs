using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Threading;

internal class OxideCuiContainerRequest : BaseUiRequest
{
    private CuiElementContainer _container;
    private string _destroyUiName;
    
    public static OxideCuiContainerRequest Create(CuiElementContainer container, SendInfo send, string destroyUiName) => UiPool.Internal.Get<OxideCuiContainerRequest>().Init(container, send, destroyUiName);

    private OxideCuiContainerRequest Init(CuiElementContainer container, SendInfo send, string destroyUiName)
    {
        base.Init(send);
        _container = container;
        _destroyUiName = destroyUiName;
        return this;
    }

    public override ProcessResult Process()
    {
        if (!string.IsNullOrEmpty(_destroyUiName))
        {
            CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("DestroyUI", Send));
        }
        CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("AddUI", Send), _container.ToJson());
        return ProcessResult.Success;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _container = null;
        _destroyUiName = null;
    }
}