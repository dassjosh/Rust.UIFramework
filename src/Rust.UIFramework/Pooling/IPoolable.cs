using System;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Pooling;

public interface IPoolable : IDisposable
{
    UiPluginPool PluginPool { get; }
    bool IsPooled { get; }

    void TryDispose();
}