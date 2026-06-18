using System;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types.Results;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IBaseRegisterImageRequest
{
    PluginId PluginId { get; }
    string Name { get; }
    IRegisterImageOptions Options { get; }
    ProcessStep Step { get; }
    void OnSuccess(Action<RegisterSuccessEventArgs> callback);
    void OnFailed(Action<IRegisterImageException> callback);
    UniTask<Result<ImageId>> AsUniTask();
}