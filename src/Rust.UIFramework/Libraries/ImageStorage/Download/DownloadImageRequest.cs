using System;
using System.Net;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public record DownloadFailedEventArgs(HttpStatusCode Code, string Message);

public sealed class DownloadImageRequest
{
    internal readonly PluginId PluginId;
    public readonly string Name;
    public readonly UrlDownloadState UrlState;
    private readonly RegisterImageOptions _options;

    private DownloadEvent<string> _downloadCompletedEvent;
    private DownloadEvent<DownloadFailedEventArgs> _downloadFailedEvent;
    private DownloadEvent<RegisterImageErrorCode> _invalidImageEvent;

    internal DownloadImageRequest(PluginId pluginId, string name, UrlDownloadState urlState, RegisterImageOptions options)
    {
        PluginId = pluginId;
        Name = name;
        UrlState = urlState;
        _options = options;
    }

    public void AddOnDownloadCompletedCallback(Action<string> callback)
    {
        _downloadCompletedEvent ??= new DownloadEvent<string>();
        _downloadCompletedEvent.AddCallback(callback);
        
        if (UrlState.State == DownloadState.Stored)
        {
            callback(UrlState.ImageId.ToString());
        }
    }

    public void AddOnDownloadFailedCallback(Action<DownloadFailedEventArgs> callback)
    {
        _downloadFailedEvent ??= new DownloadEvent<DownloadFailedEventArgs>();
        _downloadFailedEvent.AddCallback(callback);
        
        if (UrlState.State == DownloadState.Failed)
        {
            ExecuteOnDownloadFailed();
        }
    }
    
    public void AddOnInvalidImageCallback(Action<RegisterImageErrorCode> callback)
    {
        _invalidImageEvent ??= new DownloadEvent<RegisterImageErrorCode>();
        _invalidImageEvent.AddCallback(callback);
        
        if (UrlState.State == DownloadState.Completed && UrlState.ErrorCode != RegisterImageErrorCode.None)
        {
            ExecuteOnInvalidImage();
        }
    }

    internal void ExecuteOnDownloadCompleted()
    {
        _downloadCompletedEvent?.Invoke(PluginId, Name, UrlState.Url, UrlState.ImageId.Id.ToString());
        if (_options.EnableClientPrecache)
        {
            Singleton<UiImagePrecache>.Instance.AddPrecachedImage(PluginId, UrlState.ImageId, UrlState.Image);
        }
    }

    internal void ExecuteOnDownloadFailed() => _downloadFailedEvent?.Invoke(PluginId, Name, UrlState.Url, new DownloadFailedEventArgs(UrlState.StatusCode, UrlState.Message));
    internal void ExecuteOnInvalidImage() => _invalidImageEvent?.Invoke(PluginId, Name, UrlState.Url, UrlState.ErrorCode);

    private sealed class DownloadEvent<T>
    {
        private event Action<T> Event;

        public void AddCallback(Action<T> callback) => Event += callback;
        public void Invoke(PluginId id, string name, string url, T arg)
        {
            try
            {
                Event?.Invoke(arg);
            }
            catch (Exception ex)
            {
                UiFrameworkExtension.GlobalLogger.Exception("An error occured during event callback for type: {0}. Plugin: {1} Name: {2} Url: {3}", typeof(T).GetRealTypeName(), id, name, url, ex);
            }
            finally
            {
                Event = null;
            }
        }
    }
}