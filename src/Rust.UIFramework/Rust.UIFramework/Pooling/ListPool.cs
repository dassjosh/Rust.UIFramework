using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Represents a pool for list&lt;T&gt;
    /// </summary>
    /// <typeparam name="T">Type that will be in the list</typeparam>
    public class ListPool<T> : BasePool<List<T>>
    {
        public static IPool<List<T>> Instance;
        
        static ListPool()
        {
            Instance = new ListPool<T>();
        }

        private ListPool() : base(256) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref List<T> item)
        {
            item.Clear();
            return true;
        }
        
        public override void Clear()
        {
            base.Clear();
            Instance = null;
        }
    }
}