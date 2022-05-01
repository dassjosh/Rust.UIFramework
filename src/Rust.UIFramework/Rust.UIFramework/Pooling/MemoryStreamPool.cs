using System.IO;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    public class MemoryStreamPool : BasePool<MemoryStream>
    {
        public static IPool<MemoryStream> Instance;
        
        static MemoryStreamPool()
        {
            Instance = new MemoryStreamPool();
        }

        private MemoryStreamPool() : base(256) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref MemoryStream item)
        {
            item.Position = 0;
            return true;
        }
        
        public override void Clear()
        {
            base.Clear();
            Instance = null;
        }
    }
}