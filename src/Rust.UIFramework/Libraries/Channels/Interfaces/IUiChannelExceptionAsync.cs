using System;
using Cysharp.Threading.Tasks;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelExceptionAsync<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    UniTask OnException(T item, Exception ex);
}

public interface IUiChannelExceptionAsync
{
    UniTask OnException(Exception ex);
}