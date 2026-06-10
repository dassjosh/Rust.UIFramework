using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class RegisterImageRequestHandler(PluginId pluginId) : IRegisterImageRequestHandler
{
    public HandlerId Id { get; } = new();
    public PluginId PluginCreator { get; } = pluginId;
    public ProcessStep Step { get; private set; }
    public byte[] Image { get; protected set; }
    public ImageId ImageId { get; protected set; }
    public UiImageType Type { get; private set; }

    private CallbackEvent<RegisterSuccessEventArgs> _successEvent;
    private CallbackEvent<IRegisterImageFailureResult> _failedEvent;

    public ConcurrentList<RegisterImageRequest> Requests { get; } = [];

    public RegisterImageRequestHandler(PluginId pluginId, byte[] image) : this(pluginId)
    {
        Image = image;
    }

    public RegisterImageRequestHandler(PluginId pluginId, byte[] image, ImageId id) : this(pluginId, image)
    {
        ImageId = id;
    }

    public void AddRequest(RegisterImageRequest request)
    {
        Requests.TryAdd(request);
    }

    public void SetImageType(UiImageType type)
    {
        Type = type;
    }

    public void SetStep(ProcessStep step)
    {
        if (step > Step)
        {
            Step = step;
        }
    }

    public void AddSuccessCallback(Action<RegisterSuccessEventArgs> callback)
    {
        _successEvent ??= new CallbackEvent<RegisterSuccessEventArgs>();
        _successEvent.AddCallback(callback);
    }

    public void AddFailedCallback(Action<IRegisterImageFailureResult> callback)
    {
        _failedEvent ??= new CallbackEvent<IRegisterImageFailureResult>();
        _failedEvent.AddCallback(callback);
    }

    public virtual void Success(RegisterSuccessEventArgs args)
    {
        ImageId = args.ImageId;
        SetStep(ProcessStep.Completed);
        Singleton<RegisteredImageData>.Instance.OnPluginImageRegistrationCompleted(this);
        _successEvent?.Invoke(this, args);
    }

    public virtual void Failed(IRegisterImageFailureResult args)
    {
        SetStep(ProcessStep.Failed);
        Singleton<RegisteredImageData>.Instance.OnPluginImageRegistrationCompleted(this);
        _failedEvent?.Invoke(this, args);
    }
}