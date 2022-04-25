using System;
using System.Linq;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Extensions
{
    public static class EnumExt<T>
    {
        private static readonly Hash<T, string> CachedStrings = new Hash<T, string>();

        static EnumExt()
        {
            foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
            {
                CachedStrings[value] = value.ToString();
            }
        }
        
        public static string ToString(T value)
        {
            return CachedStrings[value];
        }
    }
}