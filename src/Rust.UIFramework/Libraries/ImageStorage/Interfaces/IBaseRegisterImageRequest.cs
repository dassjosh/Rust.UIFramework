using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IBaseRegisterImageRequest
{
    PluginId PluginId { get; }
    string Name { get; }
    IRegisterImageOptions Options { get; }
    ProcessStep Step { get; }
    void OnSuccess(Action<RegisterSuccessEventArgs> callback);
    void OnFailed(Action<IRegisterImageFailureResult> callback);
}