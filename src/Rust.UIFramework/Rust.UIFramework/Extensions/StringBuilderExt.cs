using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions
{
    //Define:ExtensionMethods
    public static class StringBuilderExt
    {
        /// <summary>
        /// Frees a <see cref="StringBuilder"/> back to the pool returning the created <see cref="string"/>
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> with string and being freed</param>
        public static string ToStringAndFree(this StringBuilder sb)
        {
            string result = sb.ToString();
            UiFrameworkPool.FreeStringBuilder(sb);
            return result;
        }
    }
}