using Network;
using Oxide.Ext.UiFramework.Config;

namespace Oxide.Ext.UiFramework.Threading;

internal static class ThreadingHelper
{
    private static readonly bool NetworkMultiThreading = BaseNetwork.Multithreading;
    public static readonly bool AnimationsMultiThreaded = NetworkMultiThreading && UiFrameworkConfig.Instance.Threading.EnableAnimationThread;
    public static readonly bool UiMultiThreaded = NetworkMultiThreading && UiFrameworkConfig.Instance.Threading.EnableUiSendingThread;
    public static readonly bool ImageDownloadMultiThreaded = UiFrameworkConfig.Instance.Threading.EnableImageDownloadThread;
}