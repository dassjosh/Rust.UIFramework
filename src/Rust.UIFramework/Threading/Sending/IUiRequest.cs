namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiRequest : IUiChannelObject<IUiRequest>
{
    public void SendRequest();
}