namespace Oxide.Ext.UiFramework.Pooling;

internal class ObjectPool<T> : BaseObjectPool<BasePoolable> where T : BasePoolable, new()
{
    public ObjectPool()
    {
        SetPolicy(new ObjectPoolPolicy(this));
    }

    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.ObjectPoolSize;

    private sealed class ObjectPoolPolicy(ObjectPool<T> pool) : IPooledObjectPolicy<BasePoolable>
    {
        public BasePoolable Create()
        {
            T obj = new();
            obj.OnInitInternal(pool);
            return obj;
        }

        public void Get(BasePoolable item) => item.LeavePoolInternal();
        public bool Return(BasePoolable item)
        {
            if (item.CanPool)
            {
                item.EnterPoolInternal();
                return true;
            }

            return false;
        }
    }
}