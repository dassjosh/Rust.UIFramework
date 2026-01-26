using System;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseInstantUiChannel<T> : BaseUiChannel<T> where T : IChannelObject<T>
{
    public override void Enqueue(IUiChannelObject<T> item)
    {
        ProcessItemInternal(item);
    }
    
    private void ProcessItemInternal(IUiChannelObject<T> request)
    {
        try
        {
            ProcessItem(request.Item);
        }
        catch (Exception ex)
        {
            Logger.Exception("An error occured in channel", ex);
        }
        finally
        {
            request.EnqueueNext();
        }
    }
    
    protected abstract void ProcessItem(T item);
}