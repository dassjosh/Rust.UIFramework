using System;

namespace Oxide.Ext.UiFramework.Threading;

public interface IUiRequest : IDisposable
{
    public void SendUi();
}