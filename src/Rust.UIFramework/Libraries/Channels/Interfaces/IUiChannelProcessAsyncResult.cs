using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelProcessAsyncResult<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    UniTask OnSuccess(T item);
    UniTask OnFailed(T item);
}

public interface IUiChannelProcessAsyncResult
{
    UniTask OnSuccess();
    UniTask OnFailed();
}