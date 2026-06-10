using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class DefaultImageProcessor : IUiImageProcessor, ISingleton, IUiChannelProcess<RegisterImageRequestHandler>, IUiChannelProcessResult<RegisterImageRequestHandler>, IUiChannelException<RegisterImageRequestHandler>
{
    private readonly UiChannel<RegisterImageRequestHandler> _channel;
    private readonly IUiLogger<DefaultImageProcessor> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<DefaultImageProcessor>();

    private DefaultImageProcessor()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, new UiChannelOptions(true, 2), this);
    }

    public void Enqueue(RegisterImageRequestHandler request)
    {
        request.SetStep(ProcessStep.Process);
        _channel.Enqueue(request);
        _logger.Debug("Enqueued Request: ID: {0}", request.Id);
    }

    public ProcessResult Process(RegisterImageRequestHandler request)
    {
        _logger.Debug("Processing Request: ID: {0}", request.Id);
        if (request.Image == null || request.Image.Length == 0)
        {
            _logger.Debug("Process Request Failed: ID: {0} - Invalid ByteArray", request.Id);
            request.Failed(new RegisterFailedEventArgs(RegisterImageErrorCode.InvalidByteArray));
            return ProcessResult.Failed;
        }

        if (!UiImageValidation.TryGetImageType(request.Image, out UiImageType imageType))
        {
            _logger.Debug("Process Request Failed: ID: {0} - Invalid Image Type", request.Id);
            request.Failed(new RegisterFailedEventArgs(RegisterImageErrorCode.InvalidImageType));
            return ProcessResult.Failed;
        }

        request.SetImageType(imageType);
        _logger.Debug("Process Request Success: ID: {0} Image Type: {1}", request.Id, imageType);
        return ProcessResult.Success;
    }

    public void OnSuccess(RegisterImageRequestHandler request) => Singleton<StoreHandler>.Instance.Enqueue(request);
    public void OnFailed(RegisterImageRequestHandler request) { }
    public void OnException(RegisterImageRequestHandler request, Exception ex)
    {
        request.Failed(new ExceptionEventArgs(ex));
        _logger.Exception("Process Request Failed: ID: {0}", request.Id, ex);
    }
}