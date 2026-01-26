namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiChannelObject
{
    void EnqueueNext();
}

internal interface IUiChannelObject<out T> : IUiChannelObject
{
    T Item { get; }
}