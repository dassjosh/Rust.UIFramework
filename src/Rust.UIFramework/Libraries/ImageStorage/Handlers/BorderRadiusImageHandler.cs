using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusImageHandler : ISingleton, IUiChannelProcess<RegisterImageRequestHandler>, IUiChannelProcessResult<RegisterImageRequestHandler>, IUiChannelException<RegisterImageRequestHandler>
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

    public ProcessResult Process(RegisterImageRequestHandler request)
    {
        NativeArray<Color32> input = default;
        NativeArray<Color32> output = default;
        Texture2D tex = null;
        try
        {
            BorderRadiusImageData data = request.GetModifier<BorderRadiusImageModifier>().Data;

            Texture2D image = new(2, 2);
            if(!image.LoadImage(request.Image))
            {
                _logger.Warning("Failed to load image for request.");
                return ProcessResult.Failed;
            }

            UiDimensions2D size = new(image.width, image.height);

            int count = size.WidthInt * size.HeightInt;
            input = new NativeArray<Color32>(count, Allocator.Temp);
            output = new NativeArray<Color32>(count, Allocator.Temp);
            image.SetPixelData(input, 0);

            (float tlx, float trx, float brx, float blx, float tly, float @try, float bry, float bly) = data.Radius.Apply(size);

            BorderRadiusImageJob job = new()
            {
                Width = size.WidthInt,
                Height = size.HeightInt,

                Tlx = tlx,
                Trx = trx,
                Brx = brx,
                Blx = blx,
                Tly = tly,
                Try = @try,
                Bry = bry,
                Bly = bly,

                AntiAlias = data.AntiAlias,
                EdgeWidth = data.EdgeWidth,

                Image = input,
                Pixels = output,

                Replacement = data.ReplacementColor
            };

            JobHandle handle = job.Schedule(count, 64);
            handle.Complete();

            tex = new Texture2D(size.WidthInt, size.HeightInt, TextureFormat.RGBA32, false);
            tex.SetPixelData(output, 0);
            tex.Apply();

            byte[] bytes = tex.EncodeToPNG();
            request.SetImage(bytes, true);
            return ProcessResult.Success;
        }
        catch (Exception ex)
        {
            request.Failed(new ExceptionEventArgs(ex));
            return ProcessResult.Failed;
        }
        finally
        {
            if(input.IsCreated)
            {
                input.Dispose();
            }

            if(output.IsCreated)
            {
                output.Dispose();
            }

            if(tex)
            {
                Object.Destroy(tex);
            }
        }
    }

    public void OnSuccess(RegisterImageRequestHandler request) => Singleton<DefaultImageProcessor>.Instance.Enqueue(request);
    public void OnFailed(RegisterImageRequestHandler request) { }
    public void OnException(RegisterImageRequestHandler request, Exception ex) => request.Failed(new ExceptionEventArgs(ex));
}