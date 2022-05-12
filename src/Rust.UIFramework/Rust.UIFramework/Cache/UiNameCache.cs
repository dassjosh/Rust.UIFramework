using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Cache
{
    public static class UiNameCache
    {
        private static readonly Dictionary<string, List<string>> NameCache = new Dictionary<string, List<string>>();

        public static string GetName(string baseName, int index)
        {
            List<string> names;
            if (!NameCache.TryGetValue(baseName, out names))
            {
                names = new List<string>();
                NameCache[baseName] = names;
            }

            if (index >= names.Count)
            {
                for (int i = names.Count; i <= index; i++)
                {
                    names.Add(string.Concat(baseName, "_", index.ToString()));
                }
            }

            return names[index];
        }
        
        public static void OnUnload()
        {
            NameCache.Clear();
        }
    }
}