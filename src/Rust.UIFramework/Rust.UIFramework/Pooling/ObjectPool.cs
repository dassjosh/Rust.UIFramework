namespace Oxide.Ext.UiFramework.Pooling
{
    internal class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
    {
        public static ObjectPool<T> Instance;
        
        public ObjectPool() : base(1024) { }

        protected override void OnGetItem(T item)
        {
            item.LeavePoolInternal();
        }
        
        protected override bool OnFreeItem(ref T item)
        {
            if (item.Disposed)
            {
                return false;
            }
            
            item.EnterPoolInternal();
            return true;
        }
    }
}