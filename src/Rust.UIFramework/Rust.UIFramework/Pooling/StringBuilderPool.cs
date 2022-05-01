using System.Text;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    public class StringBuilderPool : BasePool<StringBuilder>
    {
        public static IPool<StringBuilder> Instance;
        
        static StringBuilderPool()
        {
            Instance = new StringBuilderPool();
        }

        private StringBuilderPool() : base(256) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Length = 0;
            return true;
        }
        
        public override void Clear()
        {
            base.Clear();
            Instance = null;
        }
    }
}