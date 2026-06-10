using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelAsyncComplete<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    UniTask OnCompleted(T item);
}

public interface IUiChannelAsyncComplete
{
    UniTask OnCompleted();
}