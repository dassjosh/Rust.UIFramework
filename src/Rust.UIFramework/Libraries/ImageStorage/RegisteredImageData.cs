using System.Collections.Concurrent;
using System.Linq;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class RegisteredImageData : ISingleton
{
    private readonly ConcurrentDictionary<PluginId, ConcurrentList<RegisterImageRequest>> _pluginImages = new();
    private readonly ConcurrentDictionary<string, DownloadImageRequestHandler> _urlRequests = new();
    private readonly ConcurrentHashSet<PluginId> _completedPlugins = [];

    private RegisteredImageData() { }

    private void AddPluginImageRequest(PluginId id, RegisterImageRequest request)
    {
        ConcurrentList<RegisterImageRequest> requests = _pluginImages.GetOrAdd(id, static _ => []);
        requests.TryAdd(request);
    }

    /// <summary>
    /// Adds a new download request to the queue if it doesn't already exist or hasn't failed too many times
    /// </summary>
    /// <param name="pluginId">The plugin ID associated with the request</param>
    /// <param name="name">The name for the downloaded image</param>
    /// <param name="url">The URL to download the image from</param>
    /// <param name="options"></param>
    /// <returns>True if the request was added, false if it already existed or failed too many times</returns>
    internal DownloadImageRequest AddRequest(PluginId pluginId, string name, string url, RegisterImageOptions options)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(name);
        Guard.IsNotNullOrEmpty(url);
        Guard.IsNotNull(options);

        if (!_urlRequests.TryGetValue(url, out DownloadImageRequestHandler handler))
        {
            handler = new DownloadImageRequestHandler(pluginId, url);
            _urlRequests.TryAdd(url, handler);
        }

        DownloadImageRequest request = new(pluginId, name, options, handler);
        AddPluginImageRequest(pluginId, request);
        if (handler.State.IsCompleted || handler.State.IsOutOfAttempts)
        {
            return request;
        }

        handler.AddRequest(request);
        if (handler.Step == ProcessStep.Init)
        {
            Singleton<ImageDownloadHandler>.Instance.Enqueue(handler);
        }

        return request;
    }

    internal RegisterImageRequest AddRequest(PluginId pluginId, string name, byte[] image, RegisterImageOptions options)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(name);
        Guard.IsNotNullOrEmpty(image);
        Guard.IsNotNull(options);

        RegisterImageRequestHandler handler = new(pluginId, image);
        RegisterImageRequest request = new(pluginId, name, options, handler);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        Singleton<DefaultImageProcessor>.Instance.Enqueue(handler);
        return request;
    }

    internal DownloadImageRequest AddExistingImageRequest(PluginId pluginId, string name, string url, ImageId imageId, RegisterImageOptions options)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(name);
        Guard.IsNotNullOrEmpty(url);
        Guard.IsValid(imageId);
        Guard.IsNotNull(options);

        DownloadImageRequestHandler handler = new(pluginId, url, imageId);
        DownloadImageRequest request = new(pluginId, name, options, handler);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        handler.Success(new RegisterSuccessEventArgs(imageId));
        Singleton<SaveHandler>.Instance.Enqueue(handler);
        return request;
    }

    internal RegisterImageRequest AddExistingImageRequest(PluginId pluginId, string name, byte[] image, ImageId imageId, RegisterImageOptions options)
    {
        Guard.IsValid(pluginId);
        Guard.IsNotNullOrEmpty(name);
        Guard.IsValid(imageId);
        Guard.IsNotNull(options);

        RegisterImageRequestHandler handler = new(pluginId, image, imageId);
        RegisterImageRequest request = new(pluginId, name, options, handler);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        handler.Success(new RegisterSuccessEventArgs(imageId));
        Singleton<SaveHandler>.Instance.Enqueue(handler);
        return request;
    }

    internal BorderRadiusRequest AddRequest(PluginId pluginId, BorderRadiusData data, RegisterImageOptions options)
    {
        string name = data.ToName();
        BorderRadiusRequestHandler handler = new(pluginId, name, data);
        BorderRadiusRequest request = new(pluginId, name, handler, options);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        Singleton<BorderRadiusHandler>.Instance.Enqueue(handler);
        return request;
    }

    internal RegisterImageRequest AddRequest(PluginId pluginId, byte[] image, BorderRadiusData data, RegisterImageOptions options)
    {
        string name = data.ToName();
        RegisterImageRequestHandler handler = new(pluginId, image);
        RegisterImageRequest request = new(pluginId, name, options, handler);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        handler.AddModifier(new BorderRadiusImageModifier(handler, data));
        Singleton<BorderRadiusImageHandler>.Instance.Enqueue(handler);
        return request;
    }

    internal DownloadImageRequest AddRequest(PluginId pluginId, string url, BorderRadiusData data, RegisterImageOptions options)
    {
        DownloadImageRequest request = AddRequest(pluginId, data.ToName(), url, options);
        request.WithBorderRadius(data);
        return request;
    }

    internal RegisterImageRequest CreateFailed(PluginId pluginId, string name, RegisterImageOptions options)
    {
        RegisterImageRequestHandler handler = new(pluginId, null);
        RegisterImageRequest request = new(pluginId, name, options, handler);
        handler.AddRequest(request);
        AddPluginImageRequest(pluginId, request);
        handler.Failed(new RegisteredFailedException(RegisterImageErrorCode.ImageNotFound));
        return request;
    }

    public void OnPluginImageRegistrationCompleted(RegisterImageRequestHandler handler)
    {
        foreach (RegisterImageRequest request in handler.Requests)
        {
            PluginId id = request.PluginId;
            id.CallHook(handler.Step == ProcessStep.Completed ? UiFrameworkHooks.OnUiPluginImageRegistered : UiFrameworkHooks.OnUiPluginImageRegistrationFailed, request);
            ConcurrentList<RegisterImageRequest> requests = _pluginImages.GetOrAdd(id, static _ => []);
            if (requests.All(r => r.Step is ProcessStep.Completed or ProcessStep.Failed) && _completedPlugins.Add(id))
            {
                id.CallHook(UiFrameworkHooks.OnUiPluginImagesCompleted,
                    requests.Where(r => r.Step is ProcessStep.Completed).ToArray<IRegisterImageRequest>(),
                    requests.Where(r => r.Step is ProcessStep.Failed).ToArray<IRegisterImageRequest>());
            }
        }
    }

    internal bool IsDownloading(string url) => _urlRequests.TryGetValue(url, out DownloadImageRequestHandler handler) && handler.State.IsDownloading;
}