using System;
using System.Net;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public sealed class RegisterImageRequest
{
    internal readonly PluginId PluginId;
    public readonly string Name;
    public readonly ImageDownloadRequest Download;
    private readonly RegisterImageOptions _options;

    private DownloadEvent<string> _downloadCompletedEvent;
    private DownloadEvent<DownloadFailedEventArgs> _downloadFailedEvent;
    private DownloadEvent<RegisterImageErrorCode> _invalidImageEvent;

    internal RegisterImageRequest(PluginId pluginId, string name, ImageDownloadRequest download, RegisterImageOptions options)
    {
        PluginId = pluginId;
        Name = name;
        Download = download;
        _options = options;
    }

    public void AddOnDownloadCompletedCallback(Action<string> callback)
    {
        _downloadCompletedEvent ??= new DownloadEvent<string>();
        _downloadCompletedEvent.AddCallback(callback);
        
        if (Download.State == DownloadState.Stored)
        {
            callback(Download.ImageId.ToString());
        }
    }

    public void AddOnDownloadFailedCallback(Action<DownloadFailedEventArgs> callback)
    {
        _downloadFailedEvent ??= new DownloadEvent<DownloadFailedEventArgs>();
        _downloadFailedEvent.AddCallback(callback);
        
        if (Download.State == DownloadState.Failed)
        {
            ExecuteOnDownloadFailed();
        }
    }
    
    public void AddOnInvalidImageCallback(Action<RegisterImageErrorCode> callback)
    {
        _invalidImageEvent ??= new DownloadEvent<RegisterImageErrorCode>();
        _invalidImageEvent.AddCallback(callback);
        
        if (Download.State == DownloadState.Completed && Download.ErrorCode != RegisterImageErrorCode.None)
        {
            ExecuteOnInvalidImage();
        }
    }

    internal void ExecuteOnDownloadCompleted()
    {
        _downloadCompletedEvent?.Invoke(PluginId, Name, Download.Url, Download.ImageId.Id.ToString());
        if (_options.EnableClientPrecache)
        {
            Singleton<UiImagePrecache>.Instance.AddPrecachedImage(PluginId, Download.ImageId, Download.Image);
        }
    }

    internal void ExecuteOnDownloadFailed() => _downloadFailedEvent?.Invoke(PluginId, Name, Download.Url, new DownloadFailedEventArgs(Download.StatusCode, Download.Message));
    internal void ExecuteOnInvalidImage() => _invalidImageEvent?.Invoke(PluginId, Name, Download.Url, Download.ErrorCode);

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