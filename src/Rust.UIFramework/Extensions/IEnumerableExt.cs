using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Extensions;

public static class IEnumerableExt
{
    extension<T>(IEnumerable<T> enumerable)
    {
        public List<T> ToListPooled(IUiFrameworkPlugin plugin)
        {
            List<T> list = plugin.PluginPool.GetList<T>();
            list.AddRange(enumerable);
            return list;
        }
    }
}