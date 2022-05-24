namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a BasePool in UiFramework
    /// </summary>
    /// <typeparam name="T">Type being pooled</typeparam>
    public abstract class BasePool<T> : IPool<T> where T : class, new()
    {
        private readonly T[] _pool;
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
            if (_index < _pool.Length)
            {
                item = _pool[_index];
                _pool[_index] = null;
                _index++;
            }

            if (item == null)
            {
                item = new T();
            }

            OnGetItem(item);
            return item;
        }

        /// <summary>
        /// Frees an item back to the pool
        /// </summary>
        /// <param name="item">Item being freed</param>
        public void Free(ref T item)
        {
            if (item == null)
            {
                return;
            }

            if (!OnFreeItem(ref item))
            {
                return;
            }

            if (_index != 0)
            {
                _index--;
                _pool[_index] = item;
            }

            item = null;
        }

        /// <summary>
        /// Called when an item is retrieved from the pool
        /// </summary>
        /// <param name="item">Item being retrieved</param>
        protected virtual void OnGetItem(T item)
        {
            
        }
        
        /// <summary>
        /// Returns if an item can be freed to the pool
        /// </summary>
        /// <param name="item">Item to be freed</param>
        /// <returns>True if can be freed; false otherwise</returns>
        protected virtual bool OnFreeItem(ref T item)
        {
            return true;
        }

        public void Clear()
        {
            for (int index = 0; index < _pool.Length; index++)
            {
                _pool[index] = null;
                _index = 0;
            }
        }
    }
}