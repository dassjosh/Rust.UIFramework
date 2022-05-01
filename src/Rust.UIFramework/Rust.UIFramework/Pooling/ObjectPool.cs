namespace Oxide.Ext.UiFramework.Pooling
{
    public class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
    {
        public static IPool<T> Instance;
        
        static ObjectPool()
        {
            Instance = new ObjectPool<T>();
        }

        private ObjectPool() : base(1024) { }

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

        public override void Clear()
        {
            base.Clear();
            Instance = null;
        }
    }
}