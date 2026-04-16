using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Threading.UiChannel;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : BaseUiChannel<IUiRequest>, ISingleton
{
    private SendHandler() : base(UiFrameworkConfig.Instance.Threading.EnableUiSendingThread) { }
    
    protected override void ProcessItem(IUiRequest item)
    {
#if SERVER
        item.SendRequest();
#endif
    }
}