using System;
using System.Linq;
using Oxide.Plugins;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions
{
    public static class EnumExt<T>
    {
        private static readonly Hash<T, string> CachedStrings = new Hash<T, string>();

        static EnumExt()
        {
            foreach (T anchor in Enum.GetValues(typeof(T)).Cast<T>())
            {
                CachedStrings[anchor] = anchor.ToString();
            }
        }
        
        public static string ToString(T value)
        {
            return CachedStrings[value];
        }
    }
}