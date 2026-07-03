using Network;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class OxideCuiJsonRequest : BaseUiRequest
{
    private string _json;
    
    public static OxideCuiJsonRequest Create(string json, SendInfo send) => UiPool.Internal.Get<OxideCuiJsonRequest>().Init(json, send);

    private OxideCuiJsonRequest Init(string json, SendInfo send)
    {
        base.Init(send);
        _json = json;
        return this;
    }
    
    public override ProcessResult Process()
    {
        RpcFunctions.SendAddUi(Send, _json);
        return ProcessResult.Success;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _json = null;
    }
}