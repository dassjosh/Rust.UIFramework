using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Threading;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelAsyncProcess<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    UniTask<ProcessResult> Process(T item);
}

public interface IUiChannelAsyncProcess
{
    UniTask<ProcessResult> Process();
}