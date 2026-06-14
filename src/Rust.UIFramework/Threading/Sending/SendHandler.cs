using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : ISingleton
{
    internal readonly UiChannel<IUiRequest> Channel = Singleton<UiChannels>.Instance.Create<IUiRequest>(UiFrameworkPlugin.Instance, new UiChannelOptions(ThreadingHelper.UiMultiThreaded, 1));

    private SendHandler() { }

#if UNIT_TESTS
    public void WaitUntilFinished() => Channel.WaitUntilFinished();
#endif
}