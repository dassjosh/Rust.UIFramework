using System;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a poolable object
    /// </summary>
    public abstract class BasePoolable : IDisposable
    {
        internal bool Disposed;

        /// <summary>
        /// Returns if the object should be pooled.
        /// This field is set to true when leaving the pool.
        /// If the object instantiated using new() outside the pool it will be false
        /// </summary>
        private bool _shouldPool;
        private IPool<BasePoolable> _pool;

        internal void OnInit(IPool<BasePoolable> pool)
        {
            _pool = pool;
        }

        internal void EnterPoolInternal()
        {
            EnterPool();
            _shouldPool = false;
            Disposed = true;
        }

        internal void LeavePoolInternal()
        {
            _shouldPool = true;
            Disposed = false;
            LeavePool();
        }

        /// <summary>
        /// Called when the object is returned to the pool.
        /// Can be overriden in child classes to cleanup used data
        /// </summary>
        protected virtual void EnterPool()
        {
            
        }
        
        /// <summary>
        /// Called when the object leaves the pool.
        /// Can be overriden in child classes to set the initial object state
        /// </summary>
        protected virtual void LeavePool()
        {
            
        }

        public void Dispose()
        {
            if (!_shouldPool)
            {
                return;
            }

            if (Disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            
            _pool.Free(this);
        }
    }
}