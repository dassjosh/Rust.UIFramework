using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

#if SERVER
using Unity.Collections;
#endif

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusImageHandler : ISingleton, IUiChannelAsyncProcess<RegisterImageRequestHandler>, IUiChannelProcessResult<RegisterImageRequestHandler>, IUiChannelException<RegisterImageRequestHandler>
{
    private readonly UiChannel<RegisterImageRequestHandler> _channel;
    private readonly IUiLogger<BorderRadiusHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<BorderRadiusHandler>();

    private BorderRadiusImageHandler()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, UiChannelOptions.MainThread, this);
    }

    public void Enqueue(RegisterImageRequestHandler request)
    {
        request.SetStep(ProcessStep.Generate);
        _channel.Enqueue(request);
    }

    public async UniTask<ProcessResult> Process(RegisterImageRequestHandler request)
    {
#if SERVER
        NativeArray<Color32> input = default;
        NativeArray<Color32> output = default;
#endif

        try
        {
            BorderRadiusData data = request.GetModifier<BorderRadiusImageModifier>().Data;

            _logger.Debug("Processing Request: ID: {0} ImageId: {1} Type: {2} Thread: {3}", request.Id, request.ImageId, request.Type, Thread.CurrentThread.ManagedThreadId);

#if SERVER
            if (!ImageEncoding.LoadImage(request.Image, Allocator.TempJob, out input, out int width, out int height))
            {
                _logger.Warning("Failed to load image for request.");
                return ProcessResult.Failed;
            }

            int count = width * height;
            output = new NativeArray<Color32>(count, Allocator.TempJob);

            _logger.Debug("Loaded Image: ID: {0} ImageId: {1} Size: {2}x{3} Pixels: {4}", request.Id, request.ImageId, width, height, count);
#else
            ImageEncoding.LoadImage(request.Image, out Color32[] input, out int width, out int height);
            int count = width * height;
            Color32[] output = new Color32[count];
#endif
            UiSize2D size = new(width, height);
            (Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) = data.Radius.Apply(size);

            BorderRadiusJob job = new()
            {
                UseInputImage = true,
                Input = input,
                Output = output,

                Width = width,
                Height = height,

                Tlx = topLeft.x,
                Trx = topRight.x,
                Brx = bottomRight.x,
                Blx = bottomLeft.x,
                Tly = topLeft.y,
                Try = topRight.y,
                Bry = bottomRight.y,
                Bly = bottomLeft.y,

                AntiAlias = data.AntiAlias,
                EdgeWidth = data.EdgeWidth,
                Transparent = data.Transparent,

                EnableBorder = data.EnableBorder,
                BorderColor = data.BorderColor,
                BorderWidth = data.BorderWidth,

                EnableDashedBorder = data.EnableDashedBorder,
                DashLength = data.DashLength,
                GapLength = data.GapLength
            };

            await job.RunAsync(count, 64);
            byte[] bytes = ImageEncoding.EncodeToPng(output, width, height);
            request.SetImage(bytes, true);
            return ProcessResult.Success;
        }
        catch (Exception ex)
        {
            request.Failed(new RegisterException(ex));
            return ProcessResult.Failed;
        }
        finally
        {
#if SERVER
            if (input.IsCreated)
            {
                input.Dispose();
            }

            if (output.IsCreated)
            {
                output.Dispose();
            }
#endif
        }
    }

    public void OnSuccess(RegisterImageRequestHandler request) => Singleton<StoreHandler>.Instance.Enqueue(request);
    public void OnFailed(RegisterImageRequestHandler request) { }
    public void OnException(RegisterImageRequestHandler request, Exception ex) => request.Failed(new RegisterException(ex));
}