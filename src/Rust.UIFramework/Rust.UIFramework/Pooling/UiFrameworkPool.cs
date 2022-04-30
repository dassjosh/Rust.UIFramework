using System.Collections.Generic;
using System.Text;
using Oxide.Plugins;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.Pooling
{
    public static class UiFrameworkPool
    {
        public static T Get<T>() where T : class, new()
        {
            return Pool.Get<T>() ?? new T();
        }

        public static void Free<T>(ref T entity) where T : class
        {
            Pool.Free(ref entity);
        }
        
        public static List<T> GetList<T>()
        {
            return Pool.GetList<T>() ?? new List<T>();
        }
        
        public static void FreeList<T>(ref List<T> list)
        {
            Pool.FreeList(ref list);
        }

        public static Hash<TKey, TValue> GetHash<TKey, TValue>()
        {
            return Pool.Get<Hash<TKey, TValue>>() ?? new Hash<TKey, TValue>();
        }
        
        public static void FreeHash<TKey, TValue>(ref Hash<TKey, TValue> hash)
        {
            hash.Clear();
            Pool.Free(ref hash);
        }

        public static StringBuilder GetStringBuilder()
        {
            return Pool.Get<StringBuilder>() ?? new StringBuilder();
        }
        
        public static void FreeStringBuilder(ref StringBuilder sb)
        {
            sb.Clear();
            Pool.Free(ref sb);
        }
    }
}