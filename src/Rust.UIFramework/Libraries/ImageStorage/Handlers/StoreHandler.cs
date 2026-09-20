using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class StoreHandler : ISingleton, IUiChannelProcess<RegisterImageRequestHandler>, IUiChannelProcessResult<RegisterImageRequestHandler>, IUiChannelException<RegisterImageRequestHandler>
{
    private readonly UiChannel<RegisterImageRequestHandler> _channel;
    private readonly UiImageDatabaseHandler _handler = Singleton<UiImageDatabaseHandler>.Instance;
    private readonly IUiLogger<StoreHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<StoreHandler>();

    private StoreHandler()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, UiChannelOptions.MainThread, this);
    }

    public void Enqueue(RegisterImageRequestHandler request)
    {
        request.SetStep(ProcessStep.Store);
        _channel.Enqueue(request);
        _logger.Verbose("Enqueued Request: ID: {0} Plugin: {1}", request.Id, request.PluginCreator);
    }

    public ProcessResult Process(RegisterImageRequestHandler request)
    {
        _logger.Verbose("Processing Request: ID: {0} Plugin: {1}", request.Id, request.PluginCreator);
        ImageId id = StoreImage(request.Image);
        if (!id.IsValid)
        {
            _logger.Verbose("Failed to store image. ID: {0} Plugin: {1}", request.Id, request.PluginCreator);
            request.Failed(new RegisteredFailedException(RegisterImageErrorCode.DbStorageFailed));
            return ProcessResult.Failed;
        }

        if (request is DownloadImageRequestHandler download && request.ModifiedImage)
        {
            ImageId downloadId = StoreImage(download.DownloadedImage);
            if (downloadId.IsValid)
            {
                download.SetDownloadImageId(downloadId);
            }
        }

        request.SetImageId(id);
        _logger.Verbose("Image stored successfully. ID: {0} Plugin: {1} Image ID: {2}", request.Id, request.PluginCreator, id);
        return ProcessResult.Success;
    }

    private ImageId StoreImage(byte[] image)
    {
        Guard.IsNotNullOrEmpty(image);
        return _handler.Store(image);
    }

    public void OnSuccess(RegisterImageRequestHandler request)
    {
        Singleton<SaveHandler>.Instance.Enqueue(request);
    }

    public void OnFailed(RegisterImageRequestHandler request) { }
    public void OnException(RegisterImageRequestHandler request, Exception ex)
    {
        request.Failed(new RegisterException(ex));
        _logger.Exception("Process Request Failed: ID: {0}", request.Id, ex);
    }
}