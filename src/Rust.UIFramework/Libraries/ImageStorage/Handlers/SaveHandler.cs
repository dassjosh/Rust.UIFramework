using System;
using Oxide.Ext.UiFramework.Data;
using Oxide.Ext.UiFramework.Libraries.ImagePrecache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class SaveHandler : ISingleton, IUiChannelProcess<RegisterImageRequestHandler>, IUiChannelException<RegisterImageRequestHandler>
{
    private readonly ImageStorageData _data = ImageStorageData.Instance;
    private readonly UiChannel<RegisterImageRequestHandler> _channel;
    private readonly IUiLogger<SaveHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<SaveHandler>();

    private SaveHandler()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, new UiChannelOptions(true, 2), this);
    }

    public void Enqueue(RegisterImageRequestHandler request)
    {
        request.SetStep(ProcessStep.Save);
        _channel.Enqueue(request);
        _logger.Debug("Enqueued Request: ID: {0}", request.Id);
    }

    public ProcessResult Process(RegisterImageRequestHandler request)
    {
        _logger.Debug("Processing Request: ID: {0} ImageId: {1}", request.Id, request.ImageId);
        ImageId id = request.ImageId;
        if (request is IDownloadImageRequestHandler download)
        {
            if (request.ModifiedImage)
            {
                _logger.Debug("Save Modified Url: ID: {0} ImageId: {1}, Url: {2}", request.Id, download.DownloadedImageId, download.Url);
                _data.AddUrlImage(download.Url, download.DownloadedImageId);
            }
            else
            {
                _logger.Debug("Save Url: ID: {0} ImageId: {1}, Url: {2}", request.Id, id, download.Url);
                _data.AddUrlImage(download.Url, id);
            }
        }

        if (request is IBorderRadiusRequestHandler borderRadius)
        {
            _data.AddBorderRadiusImage(borderRadius.Name, id);
        }

        foreach (RegisterImageRequest image in request.Requests)
        {
            _logger.Debug("Save Image: ID: {0} ImageId: {1}, PluginId: {2}, Name: {3}", request.Id, request.ImageId, image.PluginId, image.Name);
            _data.AddPluginImage(image.PluginId, image.Name, id);
            if (image.Options.EnableClientPrecache)
            {
                Singleton<UiImagePrecache>.Instance.AddPrecachedImage(image.PluginId, image.ImageId, image.Image);
            }
            request.Requests.Remove(image);
        }

        request.Success(new RegisterSuccessEventArgs(id));
        return ProcessResult.Success;
    }

    public void OnException(RegisterImageRequestHandler request, Exception ex)
    {
        request.Failed(new ExceptionEventArgs(ex));
        _logger.Exception("Process Request Failed: ID: {0}", request.Id, ex);
    }
}