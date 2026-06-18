using System;
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

internal class BorderRadiusHandler : ISingleton, IUiChannelAsyncProcess<BorderRadiusRequestHandler>, IUiChannelProcessResult<BorderRadiusRequestHandler>, IUiChannelException<BorderRadiusRequestHandler>
{
    private readonly UiChannel<BorderRadiusRequestHandler> _channel;
    private readonly IUiLogger<BorderRadiusHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<BorderRadiusHandler>();

    private BorderRadiusHandler()
    {
        _channel = Singleton<UiChannels>.Instance.Create(UiFrameworkPlugin.Instance, UiChannelOptions.MainThread, this);
    }

    public void Enqueue(BorderRadiusRequestHandler request)
    {
        request.SetStep(ProcessStep.Generate);
        _channel.Enqueue(request);
    }

    public async UniTask<ProcessResult> Process(BorderRadiusRequestHandler request)
    {
        #if SERVER
        NativeArray<Color32> output = default;
        #endif

        try
        {
            BorderRadiusData data = request.Data;
            UiSize2D size = data.Size;
            UiBorderRadius radius = data.Radius;
            int width = size.WidthInt;
            int height = size.HeightInt;

            int count = width * height;
#if SERVER
            output = new NativeArray<Color32>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
#else
            Color32[] output = new Color32[count];
#endif

            (Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) = radius.Apply(size);

            //_logger.Debug("Generating border radius image. Size: {0} Radius: {1} AntiAlias: {2} EdgeWidth: {3}", size, radius, antiAlias, edgeWidth);

            BorderRadiusJob job = new()
            {
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
                Fill = data.Fill,
                Transparent = data.Transparent,
                Output = output,

                EnableBorder = data.EnableBorder,
                BorderColor = data.BorderColor,
                BorderWidth = data.BorderWidth,

                EnableDashedBorder = data.EnableDashedBorder,
                DashLength = data.DashLength,
                GapLength = data.GapLength
            };

            await job.RunAsync(count, 64);
            byte[] bytes = ImageEncoding.EncodeToPng(output, width, height);
            request.SetImage(bytes);
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
            if(output.IsCreated)
            {
                output.Dispose();
            }
#endif
        }
    }

    public void OnSuccess(BorderRadiusRequestHandler request) => Singleton<DefaultImageProcessor>.Instance.Enqueue(request);
    public void OnFailed(BorderRadiusRequestHandler request) { }
    public void OnException(BorderRadiusRequestHandler request, Exception ex) => request.Failed(new RegisterException(ex));
}