using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class NumberCache<T>
    {
        private static readonly Dictionary<T, string> Cache = new Dictionary<T, string>();

        public static string Get(T value)
        {
            string text;
            if (!Cache.TryGetValue(value, out text))
            {
                text = value.ToString();
                Cache[value] = text;
            }

            return text;
        }
    }
}