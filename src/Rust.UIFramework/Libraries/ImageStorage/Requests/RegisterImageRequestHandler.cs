using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Types.Results;

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

    public Result<ImageId> Result { get; private set; }

    private CallbackEvent<RegisterSuccessEventArgs> _successEvent;
    private CallbackEvent<IRegisterImageException> _failedEvent;

    private UniTaskCompletionSource<Result<ImageId>> _task;

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
        if (Result is { IsSuccess: true })
        {
            callback(new RegisterSuccessEventArgs(ImageId));
            return;
        }

        _successEvent ??= new CallbackEvent<RegisterSuccessEventArgs>();
        _successEvent.AddCallback(callback);
    }

    public void AddFailedCallback(Action<IRegisterImageException> callback)
    {
        if (Result is { IsFailure: true })
        {
            callback(Result.Exception as IRegisterImageException);
            return;
        }

        _failedEvent ??= new CallbackEvent<IRegisterImageException>();
        _failedEvent.AddCallback(callback);
    }

    public UniTask<Result<ImageId>> AsUniTask()
    {
        _task ??= new UniTaskCompletionSource<Result<ImageId>>();
        if (Result != null)
        {
            _task.TrySetResult(Result);
        }
        return _task.Task;
    }

    public virtual void Success(RegisterSuccessEventArgs args)
    {
        ImageId = args.ImageId;
        SetStep(ProcessStep.Completed);
        Singleton<RegisteredImageData>.Instance.OnPluginImageRegistrationCompleted(this);
        Result = Result<ImageId>.Success(args.ImageId);
        _successEvent?.Invoke(this, args);
        _task?.TrySetResult(args.ImageId);
    }

    public virtual void Failed(BaseImageStorageException exception)
    {
        SetStep(ProcessStep.Failed);
        Singleton<RegisteredImageData>.Instance.OnPluginImageRegistrationCompleted(this);
        Result = Result<ImageId>.Failure(exception);
        _failedEvent?.Invoke(this, exception);
        _task?.TrySetResult(Result);
    }
}