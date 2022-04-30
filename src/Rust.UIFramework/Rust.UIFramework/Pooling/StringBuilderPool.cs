using System.Text;

namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Pool for StringBuilders
    /// </summary>
    public class StringBuilderPool : BasePool<StringBuilder>
    {
        internal StringBuilderPool() : base(256) { }
        
        ///<inheritdoc/>
        protected override bool OnFreeItem(ref StringBuilder item)
        {
            item.Length = 0;
            return true;
        }
    }
}