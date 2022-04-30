using System;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a poolable object
    /// </summary>
    public class BasePoolable : IDisposable
    {
        internal bool Disposed;

        /// <summary>
        /// Returns if the object should be pooled.
        /// This field is set to true when leaving the pool.
        /// If the object instantiated using new() outside the pool it will be false
        /// </summary>
        private bool _shouldPool;

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

        /// <summary>
        /// Frees a pooled object that is part of a field on this object
        /// </summary>
        /// <param name="obj">Object to free</param>
        /// <typeparam name="T">Type of object being freed</typeparam>
        protected void Free<T>(ref T obj) where T : BasePoolable, new()
        {
            if (obj != null && obj._shouldPool)
            {
                UiFrameworkPool.Free(ref obj);
            }
        }
        
        /// <summary>
        /// Frees a pooled list that is part of a field on this object
        /// </summary>
        /// <param name="obj">List to be freed</param>
        /// <typeparam name="T">Type of the list</typeparam>
        protected void FreeList<T>(ref List<T> obj)
        {
            UiFrameworkPool.FreeList(ref obj);
        }

        /// <summary>
        /// Disposes the object when used in a using statement
        /// </summary>
        public void Dispose()
        {
            if (_shouldPool)
            {
                UiFrameworkPool.Free(this);
            }
        }
    }
}