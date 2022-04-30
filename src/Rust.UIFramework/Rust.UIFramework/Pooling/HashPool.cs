using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a pool for Hash&lt;TKey, TValue&gt;
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
    {
        public static HashPool<TKey, TValue> Instance;
        
        internal HashPool() : base(256) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}