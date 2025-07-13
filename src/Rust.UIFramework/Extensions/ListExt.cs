using System.Collections.Generic;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ListExt
{
    public static void FreeValues<T>(this List<T> list) where T : BasePoolable
    {
        if (list == null)
        {
            return;
        }
        
        int count = list.Count;
        for (int index = 0; index < count; index++)
        {
            list[index].Dispose();
        }
        
        list.Clear();
    }

    public static void TryFreeValues<T>(this List<T> list)
    {
        if (list == null)
        {
            return;
        }
        
        int count = list.Count;
        for (int index = 0; index < count; index++)
        {
            if (list[index] is BasePoolable poolable)
            {
                poolable.Dispose();
            }
        }
        
        list.Clear();
    }
}