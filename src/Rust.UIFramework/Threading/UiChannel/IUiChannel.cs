namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiChannel<T> where T : IUiChannelObject<T>
{
    void Enqueue(IUiChannelObject<T> item);
}