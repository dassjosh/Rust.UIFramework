namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiRequest : IChannelObject<IUiRequest>
{
    public void SendRequest();
}