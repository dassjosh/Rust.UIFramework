namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelProcessResult<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    void OnSuccess(T item);
    void OnFailed(T item);
}

public interface IUiChannelProcessResult
{
    void OnSuccess();
    void OnFailed();
}