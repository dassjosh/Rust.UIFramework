using Oxide.Ext.UiFramework.Threading;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelProcess<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    ProcessResult Process(T item);
}

public interface IUiChannelProcess
{
    ProcessResult Process();
}