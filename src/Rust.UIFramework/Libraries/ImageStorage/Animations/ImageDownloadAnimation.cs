using System;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

public class ImageDownloadAnimation : BaseAnimation
{ 
    private ImageId _id;
    private string _timeoutImage;
    private string _failedImage;
    private ITriggeredDuration _duration;
    private DownloadState _state = DownloadState.InProgress;

    private enum DownloadState { InProgress, Failed, Success, Timeout }
    
    internal static ImageDownloadAnimation Create(IAnimationBuilder builder, ITriggeredDuration duration, in UiReference reference, ImageDownloadOptions options)
    {
        ImageDownloadAnimation animation = builder.PluginPool.Get<ImageDownloadAnimation>();
        animation.Init(builder, duration, reference, options);
        return animation;
    }

    private void Init(IAnimationBuilder builder, ITriggeredDuration duration, in UiReference reference, ImageDownloadOptions options)
    {
        base.Init(builder.Plugin, reference, duration);
        _duration = duration;
        _timeoutImage = options.AutomaticUpdate.TimeoutImageNameOrUrl;
        _failedImage = options.FailedImageNameOrUrl;
    }

    internal void OnImageDownloadedSuccessfully(ImageId id)
    {
        _id = id;
        _state = DownloadState.Success;
        _duration.Trigger();
    }
    
    public void OnImageDownloadFailed()
    {
        _state = DownloadState.Failed;
        _duration.Trigger();
    }
    
    public override void WriteAnimation(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        if (_state == DownloadState.InProgress && _duration.HasTimedOut)
        {
            _state = DownloadState.Timeout;
        }
        
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.RawImage.Type);
        
        string image = _state switch
        {
            DownloadState.InProgress or DownloadState.Failed => _failedImage,
            DownloadState.Success => _id.ToString(),
            DownloadState.Timeout => _timeoutImage ?? _failedImage,
        };
        SetImage(writer, image);
        
        writer.WriteEndObject();
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    private void SetImage(JsonFrameworkWriter writer, string image)
    {
        if (image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            image = Singleton<UiImageStorage>.Instance.Get(Plugin, image);
            if (image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                writer.AddFieldRaw(JsonDefaults.Image.UrlName, image);
            }
            else
            {
                writer.AddFieldRaw(JsonDefaults.Image.PngName, image);
            }
        }
        else
        {
            writer.AddFieldRaw(JsonDefaults.Image.PngName, image);
        }
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _state = DownloadState.InProgress;
        _timeoutImage = null;
        _failedImage = null;
        if (_duration is BasePoolable poolable)
        {
            poolable.TryDispose();
        }
    }
}