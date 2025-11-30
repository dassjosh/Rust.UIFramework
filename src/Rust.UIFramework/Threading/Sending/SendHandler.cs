using Oxide.Ext.UiFramework.Threading.UiChannel;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : BaseUiChannel<IUiRequest>, ISingleton
{
    private SendHandler() { }
    
    protected override void ProcessItem(IUiRequest item)
    {
#if SERVER
        item.SendRequest();
#endif
    }
}