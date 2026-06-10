using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IRegisterImageRequestHandler : IBaseUiChannelObject
{
    HandlerId Id { get; }
    byte[] Image { get; }
    ImageId ImageId { get; }
    UiImageType Type { get; }
    PluginId PluginCreator { get; }
    ProcessStep Step { get; }
    ConcurrentList<RegisterImageRequest> Requests { get; }

    void AddRequest(RegisterImageRequest request);
}