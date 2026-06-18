using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types.Results;

namespace Oxide.Ext.UiFramework.Libraries;

internal class RegisterImageRequest(PluginId pluginId, string name, IRegisterImageOptions options, RegisterImageRequestHandler handler) : IRegisterImageRequest
{
    public PluginId PluginId { get; } = pluginId;
    public string Name { get; } = name;
    public IRegisterImageOptions Options { get; } = options;
    public byte[] Image => Handler.Image;
    public ImageId ImageId => Handler.ImageId;
    public UiImageType Type => Handler.Type;
    public ProcessStep Step => Handler.Step;
    public RegisterImageRequestHandler Handler { get; } = handler;

    public void OnSuccess(Action<RegisterSuccessEventArgs> callback)
    {
        Handler.AddSuccessCallback(callback);
    }

    public void OnFailed(Action<IRegisterImageException> callback)
    {
        Handler.AddFailedCallback(callback);
    }

    public UniTask<Result<ImageId>> AsUniTask() => Handler.AsUniTask();
}