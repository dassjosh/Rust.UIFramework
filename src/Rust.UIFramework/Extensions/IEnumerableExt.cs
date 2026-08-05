using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Extensions;

public static class IEnumerableExt
{
    extension<T>(IEnumerable<T> enumerable)
    {
        public List<T> ToListPooled(IUiFrameworkPlugin plugin) => enumerable.ToListPooled(plugin.PluginPool);

        internal List<T> ToListPooled(UiPluginPool pluginPool)
        {
            List<T> list = pluginPool.GetList<T>();
            if (enumerable is not null)
            {
                list.AddRange(enumerable);
            }
            return list;
        }
    }
}