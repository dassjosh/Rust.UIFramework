namespace Oxide.Ext.UiFramework.Threading.UiChannel;

internal interface IUiChannel
{
    void Enqueue(IUiChannelObject item);
}