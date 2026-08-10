using System.Threading;
using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Extensions;

public static class UniTaskExt
{
    public static async UniTask SwitchToThreadPool()
    {
        if (Thread.CurrentThread.ManagedThreadId == ThreadExt.MainThreadId)
        {
            await UniTask.SwitchToThreadPool();
        }
    }

    public static async UniTask SwitchToMainThread()
    {
        if (Thread.CurrentThread.ManagedThreadId != ThreadExt.MainThreadId)
        {
            await UniTask.SwitchToMainThread();
        }
    }
}