using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ConcurrentListPool<T> : BasePool<ConcurrentList<T>, ConcurrentListPool<T>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ListPoolSize;
    protected override ConcurrentList<T> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(ConcurrentList<T> item)
    {
        item.Clear();
        return true;
    }
}