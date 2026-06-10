using System;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IUiChannelException<in T> : IUiChannelHandler<T> where T : IBaseUiChannelObject
{
    void OnException(T item, Exception ex);
}

public interface IUiChannelException
{
    void OnException(Exception ex);
}