using System;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Callbacks;

/// <summary>
/// Represents a base callback to be used when needing a lambda callback so no delegate or class is generated
/// This class is pooled to prevent allocations
/// </summary>
public abstract class BaseAsyncCallback : BasePoolable
{
    /// <summary>
    /// The callback to be called by the delegate
    /// </summary>
    private readonly Action _callback;
        
    /// <summary>
    /// Constructor
    /// </summary>
    protected BaseAsyncCallback()
    {
        _callback = CallbackInternal;
    }

    /// <summary>
    /// Overridden in the child class to handle the callback
    /// </summary>
    protected abstract ValueTask HandleCallback();

    /// <summary>
    /// Returns Exception message if an error occurs 
    /// </summary>
    /// <returns></returns>
    protected abstract string GetExceptionMessage();
        
    /// <summary>
    /// Runs the callback using async
    /// </summary>
    protected void Run()
    {
#pragma warning disable EPC13
        Task.Run(_callback);
#pragma warning restore EPC13
    }

    private async void CallbackInternal()
    {
        try
        {
            await HandleCallback().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Interface.Oxide.LogException($"{GetType().GetRealTypeName()}.CallbackInternal had exception. Callback Data: {GetExceptionMessage()}", ex);
        }
        finally
        {
            Dispose();
        }
    }
}