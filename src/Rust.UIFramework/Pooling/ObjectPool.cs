namespace Oxide.Ext.UiFramework.Pooling;

internal class ObjectPool<T> : BasePool<BasePoolable, ObjectPool<T>> where T : BasePoolable, new()
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ObjectPoolSize;
    protected override BasePoolable CreateNew()
    {
        T obj = new();
        obj.OnInitInternal(this);
        return obj;
    }

    protected override void OnGetItem(BasePoolable item)
    {
        item.LeavePoolInternal();
    }
        
    protected override bool OnFreeItem(BasePoolable item)
    {
        if (item.CanPool)
        {
            item.EnterPoolInternal();
            return true;
        }

        return false;
    }
}