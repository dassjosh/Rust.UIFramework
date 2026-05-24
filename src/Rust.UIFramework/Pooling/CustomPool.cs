namespace Oxide.Ext.UiFramework.Pooling;

public abstract class CustomPool<TPooled, TPool> : BaseObjectPool<TPooled, TPool>
    where TPooled : class
    where TPool : CustomPool<TPooled, TPool>, new()
{
    protected CustomPool() { }
    protected CustomPool(IPooledObjectPolicy<TPooled> policy) : base(policy) { }
}