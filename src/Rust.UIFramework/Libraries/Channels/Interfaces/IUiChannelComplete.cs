namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelComplete<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    void OnCompleted(T item);
}

public interface IUiChannelComplete
{
    void OnCompleted();
}