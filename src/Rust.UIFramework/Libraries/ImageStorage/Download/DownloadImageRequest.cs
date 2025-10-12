using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public sealed class DownloadImageRequest
{
    internal readonly PluginId PluginId;
    public readonly string Name;
    public readonly UrlDownloadState UrlState;

    private event Action<string> OnDownloadCompleted;
    private event Action OnDownloadFailed;

    internal DownloadImageRequest(PluginId pluginId, string name, UrlDownloadState urlState)
    {
        PluginId = pluginId;
        Name = name;
        UrlState = urlState;
    }

    public void AddOnDownloadCompletedCallback(Action<string> callback)
    {
        if (UrlState.State == DownloadState.Completed)
        {
            callback(UrlState.ImageId.ToString());
        }
        else
        {
            OnDownloadCompleted += callback;
        }
    }

    public void AddOnDownloadFailedCallback(Action callback)
    {
        if (UrlState.State == DownloadState.Failed && UrlState.IsOutOfAttempts)
        {
            callback();
        }
        else
        {
            OnDownloadFailed += callback;
        }
    }

    internal void ExecuteOnDownloadCompleted(string imageId)
    {
        try
        {
            OnDownloadCompleted?.Invoke(imageId);
        }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Exception("An error occured during OnDownloadCompleted callback. Plugin: {0} Name: {1} Url: {2}", PluginId, Name, UrlState.Url, ex);
        }
    }

    internal void ExecuteOnDownloadFailed()
    {
        try
        {
            OnDownloadFailed?.Invoke();
        }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Exception("An error occured during OnDownloadCompleted callback. Plugin: {0} Name: {1} Url: {2}", PluginId, Name, UrlState.Url, ex);
        }
    }
}