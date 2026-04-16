using Oxide.Ext.UiFramework.Threading.UiChannel;

namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiRequest : IChannelObject
{
    public void SendRequest();
}