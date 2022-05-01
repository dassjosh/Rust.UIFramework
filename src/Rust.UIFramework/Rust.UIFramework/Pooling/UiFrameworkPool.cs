using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling
{
    public static class UiFrameworkPool
    {
        private static readonly Hash<Type, IPool> Pools = new Hash<Type, IPool>();

        /// <summary>
        /// Returns a pooled object of type T
        /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <returns>Pooled object of type T</returns>
        public static T Get<T>() where T : BasePoolable, new()
        {
            return ObjectPool<T>.Instance.Get();
        }

        /// <summary>
        /// Returns a <see cref="BasePoolable"/> back into the pool
        /// </summary>
        /// <param name="value">Object to free</param>
        /// <typeparam name="T">Type of object being freed</typeparam>
        public static void Free<T>(ref T value) where T : BasePoolable, new()
        {
            ObjectPool<T>.Instance.Free(ref value);
        }

        /// <summary>
        /// Returns a <see cref="BasePoolable"/> back into the pool
        /// </summary>
        /// <param name="value">Object to free</param>
        /// <typeparam name="T">Type of object being freed</typeparam>
        internal static void Free<T>(T value) where T : BasePoolable, new()
        {
            ObjectPool<T>.Instance.Free(ref value);
        }

        /// <summary>
        /// Returns a pooled <see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">Type for the list</typeparam>
        /// <returns>Pooled List</returns>
        public static List<T> GetList<T>()
        {
            return ListPool<T>.Instance.Get();
        }

        /// <summary>
        /// Returns a pooled <see cref="Hash{TKey, TValue}"/>
        /// </summary>
        /// <typeparam name="TKey">Type for the key</typeparam>
        /// <typeparam name="TValue">Type for the value</typeparam>
        /// <returns>Pooled Hash</returns>
        public static Hash<TKey, TValue> GetHash<TKey, TValue>()
        {
            return HashPool<TKey, TValue>.Instance.Get();
        }

        /// <summary>
        /// Returns a pooled <see cref="StringBuilder"/>
        /// </summary>
        /// <returns>Pooled <see cref="StringBuilder"/></returns>
        public static StringBuilder GetStringBuilder()
        {
            return StringBuilderPool.Instance.Get();
        }
        
        /// <summary>
        /// Returns a pooled <see cref="MemoryStream"/>
        /// </summary>
        /// <returns>Pooled <see cref="MemoryStream"/></returns>
        public static MemoryStream GetMemoryStream()
        {
            return MemoryStreamPool.Instance.Get();
        }

        /// <summary>
        /// Free's a pooled <see cref="List{T}"/>
        /// </summary>
        /// <param name="list">List to be freed</param>
        /// <typeparam name="T">Type of the list</typeparam>
        public static void FreeList<T>(ref List<T> list)
        {
            ListPool<T>.Instance.Free(ref list);
        }

        /// <summary>
        /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
        /// </summary>
        /// <param name="hash">Hash to be freed</param>
        /// <typeparam name="TKey">Type for key</typeparam>
        /// <typeparam name="TValue">Type for value</typeparam>
        public static void FreeHash<TKey, TValue>(ref Hash<TKey, TValue> hash)
        {
            HashPool<TKey, TValue>.Instance.Free(ref hash);
        }

        /// <summary>
        /// Frees a <see cref="StringBuilder"/> back to the pool
        /// </summary>
        /// <param name="sb">StringBuilder being freed</param>
        public static void FreeStringBuilder(ref StringBuilder sb)
        {
            StringBuilderPool.Instance.Free(ref sb);
        }
        
        /// <summary>
        /// Frees a <see cref="MemoryStream"/> back to the pool
        /// </summary>
        /// <param name="stream">MemoryStream being freed</param>
        public static void FreeMemoryStream(ref MemoryStream stream)
        {
            MemoryStreamPool.Instance.Free(ref stream);
        }

        /// <summary>
        /// Frees a <see cref="StringBuilder"/> back to the pool returning the <see cref="string"/>
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> being freed</param>
        public static string ToStringAndFreeStringBuilder(ref StringBuilder sb)
        {
            string result = sb.ToString();
            StringBuilderPool.Instance.Free(ref sb);
            return result;
        }

        public static void AddPool<TType>(BasePool<TType> pool) where TType : class, new()
        {
            Pools[typeof(TType)] = pool;
        }
        
        public static void OnUnload()
        {
            foreach (IPool pool in Pools.Values)
            {
                pool.Clear();
            }
            
            Pools.Clear();
        }
    }
}