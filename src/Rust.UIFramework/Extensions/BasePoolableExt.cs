using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

public static class BasePoolableExt
{
    public static void TryReturnToPool(this object obj)
    {
        if (obj is BasePoolable poolable)
        {
            poolable.TryDispose();
        }
    }
}