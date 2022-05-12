using System.Text;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    public class StringBuilderPool : BasePool<StringBuilder>
    {
        public static readonly IPool<StringBuilder> Instance;
        
        static StringBuilderPool()
        {
            Instance = new StringBuilderPool();
        }

        private StringBuilderPool() : base(64) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Length = 0;
            return true;
        }
    }
}