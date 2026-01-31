namespace Oxide.Ext.UiFramework.Threading;

internal interface IUiChannelObject<T> where T : IUiChannelObject<T>
{
    void Enqueue();
    void OnCompleted();
}