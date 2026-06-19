using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

#if SERVER
using System;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
#endif

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationTrackerChannel : ISingleton
{
    private readonly UiChannel<UiTrackerRequest> _channel = Singleton<UiChannels>.Instance.Create<UiTrackerRequest>(UiFrameworkPlugin.Instance, new UiChannelOptions(true, 2));

    private AnimationTrackerChannel() { }

    public void Enqueue(UiTrackerRequest item)
    {
        _channel.Enqueue(item);
    }

#if UNIT_TESTS || BENCHMARKS
    public void WaitUntilFinished() => _channel.WaitUntilFinished();
#endif
}