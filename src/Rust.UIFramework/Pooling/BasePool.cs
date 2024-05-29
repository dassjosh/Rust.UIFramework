namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a BasePool in UiFramework
/// </summary>
/// <typeparam name="T">Type being pooled</typeparam>
public abstract class BasePool<T> : IPool<T> where T : class
{
    private readonly T[] _pool;
    private readonly object _lock = new();
    private int _index;

    /// <summary>
    /// Base Pool Constructor
    /// </summary>
    /// <param name="maxSize">Max Size of the pool</param>
    protected BasePool(int maxSize)
    {
        _pool = new T[maxSize];
        UiFrameworkPool.AddPool(this);
    }

    /// <summary>
    /// Returns an element from the pool if it exists else it creates a new one
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        T item = null;

        lock (_lock)
        {
            if (_index < _pool.Length)
            {
                item = _pool[_index];
                _pool[_index] = null;
                _index++;
            }
        }

        item ??= CreateNew();
        OnGetItem(item);
        return item;
    }

    /// <summary>
    /// Creates new type of T
    /// </summary>
    /// <returns>Newly created type of T</returns>
    protected abstract T CreateNew();

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public void Free(T item) => Free(ref item);

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    private void Free(ref T item)
    {
        if (item == null)
        {
            return;
        }

        if (!OnFreeItem(ref item))
        {
            return;
        }

        lock (_lock)
        {
            if (_index != 0)
            {
                _index--;
                _pool[_index] = item;
            }
        }

        item = null;
    }

    /// <summary>
    /// Called when an item is retrieved from the pool
    /// </summary>
    /// <param name="item">Item being retrieved</param>
    protected virtual void OnGetItem(T item) { }

    /// <summary>
    /// Returns if an item can be freed to the pool
    /// </summary>
    /// <param name="item">Item to be freed</param>
    /// <returns>True if can be freed; false otherwise</returns>
    protected virtual bool OnFreeItem(ref T item) => true;

    public void Clear()
    {
        lock (_lock)
        {
            for (int index = 0; index < _pool.Length; index++)
            {
                _pool[index] = null;
                _index = 0;
            }
        }
    }
}