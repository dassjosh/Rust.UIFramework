using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.UiFramework.Threading;

internal class OxideCuiElementsRequest : BaseUiRequest, IUiRequest
{
    private List<CuiElement> _elements;
    
    public static OxideCuiElementsRequest Create(List<CuiElement> elements, SendInfo send)
    {
        OxideCuiElementsRequest request = UiFrameworkPool.Get<OxideCuiElementsRequest>();
        request.Init(elements, send);
        return request;
    }
    
    private void Init(List<CuiElement> elements, SendInfo send)
    {
        base.Init(send);
        _elements = elements;
    }
    
    public void SendRequest()
    {
        CommunityEntity.ServerInstance.ClientRPC(RpcTarget.SendInfo("AddUI", Send), CuiHelper.ToJson(_elements));
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _elements = null;
    }
}