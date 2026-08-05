using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Helpers;

namespace Oxide.Ext.UiFramework.Builder;

public class CachedPieMenu(byte[] menu) : BasePieMenuBuilder
{
    public override void SendUi(SendInfo send)
    {
        RpcFunctions.SendPieMenu(send, menu);
    }
}