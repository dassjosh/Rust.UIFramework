using Network;
using Oxide.Ext.UiFramework.Config;

namespace Oxide.Ext.UiFramework.Threading;

internal static class ThreadingHelper
{
    private static readonly bool ServerMultiThreaded = BaseNetwork.Multithreading;
    public static readonly bool AnimationsMultiThreaded = ServerMultiThreaded && UiFrameworkConfig.Instance.Threading.EnableAnimationThread;
    public static readonly bool UiMultiThreaded = ServerMultiThreaded && UiFrameworkConfig.Instance.Threading.EnableAnimationThread;
}