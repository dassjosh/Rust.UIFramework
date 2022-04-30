using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a BasePool in Discord
    /// </summary>
    /// <typeparam name="T">Type being pooled</typeparam>
    public abstract class BasePool<T> : IPool<T> where T : class, new()
    {
        private readonly Queue<T> _pool;
        private readonly int _maxSize;

        /// <summary>
        /// Base Pool Constructor
        /// </summary>
        /// <param name="maxSize">Max Size of the pool</param>
        protected BasePool(int maxSize)
        {
            _maxSize = maxSize;
            _pool = new Queue<T>(maxSize);
            for (int i = 0; i < maxSize; i++)
            {
                _pool.Enqueue(new T());
            }
        }
        
        /// <summary>
        /// Returns an element from the pool if it exists else it creates a new one
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T item = _pool.Count != 0 ? _pool.Dequeue() : new T();
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
            
            if (_pool.Count >= _maxSize)
            {
                return;
            }
                
            _pool.Enqueue(item);

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
            _pool.Clear();
        }
    }
}