namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiChannel<in T>
{
    void Enqueue(IUiChannelObject<T> item);
}