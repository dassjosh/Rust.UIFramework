using System;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class TaskExt
{
    internal static async void RunSafely(Task task, Action<Exception> onError, BasePoolable poolable)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            onError(ex);
        }
        finally
        {
            poolable?.TryDispose();
        }
    }
    
    internal static async void RunSafely(ValueTask task, Action<Exception> onError, BasePoolable poolable)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            onError(ex);
        }
        finally
        {
            poolable?.TryDispose();
        }
    }
}