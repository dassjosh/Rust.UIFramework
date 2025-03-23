using System.Collections.Generic;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ListExt
{
    public static void ReturnToPool<T>(this List<T> list) where T : BasePoolable
    {
        if (list == null)
        {
            return;
        }
        
        for (int index = 0; index < list.Count; index++)
        {
            list[index].Dispose();
        }

        UiFrameworkPool.FreeList(list);
    }
}