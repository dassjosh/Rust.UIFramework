using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusHandler : ISingleton, IUiChannelProcess<BorderRadiusRequestHandler>, IUiChannelProcessResult<BorderRadiusRequestHandler>, IUiChannelException<BorderRadiusRequestHandler>
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

    public ProcessResult Process(BorderRadiusRequestHandler request)
    {
        NativeArray<Color32> pixels = default;
        Texture2D tex = null;
        try
        {
            UiDimensions2D size = request.Data.Size;
            UiBorderRadius radius = request.Data.Radius;
            bool antiAlias = request.Data.AntiAlias;
            float edgeWidth = request.Data.EdgeWidth;
            int width = size.WidthInt;
            int height = size.HeightInt;

            int count = width * height;
            pixels = new NativeArray<Color32>(count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);

            (float tlx, float trx, float brx, float blx, float tly, float @try, float bry, float bly) = radius.Apply(size);

            _logger.Debug("Generating border radius image. Size: {0} Radius: {1} AntiAlias: {2} EdgeWidth: {3}", size, radius, antiAlias, edgeWidth);

            BorderRadiusJob job = new()
            {
                Width = width,
                Height = height,

                Tlx = tlx,
                Trx = trx,
                Brx = brx,
                Blx = blx,
                Tly = tly,
                Try = @try,
                Bry = bry,
                Bly = bly,

                AntiAlias = antiAlias,
                EdgeWidth = edgeWidth,
                Fill = new Color32(255, 255, 255, 255),
                Transparent = new Color32(0, 0, 0, 0),
                Pixels = pixels
            };

            JobHandle handle = job.Schedule(count, 64);
            handle.Complete();

            tex = new Texture2D(width, height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };

            tex.SetPixelData(pixels, 0);
            tex.Apply(false, false);

            byte[] bytes = tex.EncodeToPNG();
            request.SetImage(bytes);
            return ProcessResult.Success;
        }
        catch (Exception ex)
        {
            request.Failed(new ExceptionEventArgs(ex));
            return ProcessResult.Failed;
        }
        finally
        {
            if(pixels.IsCreated)
            {
                pixels.Dispose();
            }

            if(tex)
            {
                Object.Destroy(tex);
            }
        }
    }

    public void OnSuccess(BorderRadiusRequestHandler request) => Singleton<DefaultImageProcessor>.Instance.Enqueue(request);
    public void OnFailed(BorderRadiusRequestHandler request) { }
    public void OnException(BorderRadiusRequestHandler request, Exception ex) => request.Failed(new ExceptionEventArgs(ex));
}