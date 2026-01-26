using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Threading;

internal class OxideCuiElementsRequest : BaseUiRequest
{
    private List<CuiElement> _elements;
    
    public static OxideCuiElementsRequest Create(List<CuiElement> elements, SendInfo send) => UiPool.Internal.Get<OxideCuiElementsRequest>().Init(elements, send);

    private OxideCuiElementsRequest Init(List<CuiElement> elements, SendInfo send)
    {
        base.Init(send);
        _elements = elements;
        return this;
    }
    
    public override void SendRequest()
    {
        CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("AddUI", Send), CuiHelper.ToJson(_elements));
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _elements = null;
    }
}