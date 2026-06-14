using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class RegisterImageRequestHandler(PluginId pluginId) : IRegisterImageRequestHandler
{
    public HandlerId Id { get; } = new();
    public PluginId PluginCreator { get; } = pluginId;
    public ProcessStep Step { get; private set; }
    public byte[] Image { get; private set; }
    public bool ModifiedImage { get; private set; }
    public ImageId ImageId { get; protected set; }
    public UiImageType Type { get; private set; }

    public List<IImageModifier> Modifiers { get; protected set; }

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

    public void SetImage(byte[] image, bool modified = false)
    {
        Image = image;
        ModifiedImage = modified;
    }

    public void SetImageType(UiImageType type)
    {
        Type = type;
    }

    public void SetImageId(ImageId id)
    {
        ImageId = id;
    }

    public void SetStep(ProcessStep step)
    {
        if (step > Step)
        {
            Step = step;
        }
    }

    public void AddModifier(IImageModifier modifier)
    {
        Modifiers ??= [];
        Modifiers.Add(modifier);
    }

    public void AddModifiers(IEnumerable<IImageModifier> modifiers)
    {
        Modifiers ??= [];
        Modifiers.AddRange(modifiers);
    }

    public T GetModifier<T>()
    {
        if (Modifiers == null)
        {
            return default;
        }

        foreach (IImageModifier modifier in Modifiers)
        {
            if (modifier is T result)
            {
                return result;
            }
        }

        return default;
    }

    public bool Redirect()
    {
        if (Modifiers == null)
        {
            return false;
        }

        foreach (IImageModifier modifier in Modifiers)
        {
            if(modifier.Redirect(Step))
            {
                return true;
            }
        }

        return false;
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