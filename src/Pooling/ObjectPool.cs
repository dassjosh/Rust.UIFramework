namespace Oxide.Ext.UiFramework.Pooling
{
    public class ObjectPool<T> : BasePool<BasePoolable> where T : BasePoolable, new()
    {
        public static readonly IPool<BasePoolable> Instance = new ObjectPool<T>();

        private ObjectPool() : base(1024) { }

        protected override BasePoolable CreateNew()
        {
            T obj = new T();
            obj.OnInit(this);
            return obj;
        }

        protected override void OnGetItem(BasePoolable item)
        {
            item.LeavePoolInternal();
        }
        
        protected override bool OnFreeItem(ref BasePoolable item)
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