namespace Oxide.Ext.UiFramework.Threading;

internal interface IChannelObject<in T>
{
    public IUiChannel<T> GetChannel(int index);
}