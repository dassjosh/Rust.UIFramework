using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Libraries;

internal sealed class CallbackEvent<T>
{
    private event Action<T> Event;

    public void AddCallback(Action<T> callback)
    {
        Guard.IsNotNull(callback);
        Event += callback;
    }

    public void Invoke(RegisterImageRequestHandler handler, T arg)
    {
        try
        {
            Event?.Invoke(arg);
        }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Exception("An error occured during event callback for type: {0}. ID: {1}", typeof(T).GetRealTypeName(), handler.Id, ex);
        }
        finally
        {
            Event = null;
        }
    }
}