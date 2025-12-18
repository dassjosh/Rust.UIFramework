using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public sealed class PooledListEnumerator<T> : BasePooledListEnumerator<T>
{
    public PooledListEnumerator() { }
    
    internal PooledListEnumerator(ICollection<T> list)
    {
        List = new List<T>(list);
    }
    
    public static PooledListEnumerator<T> Create(IUiFrameworkPlugin plugin, IList<T> list) => plugin.PluginPool.Get<PooledListEnumerator<T>>().Init(list);

    private PooledListEnumerator<T> Init(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            List.Add(list[i]);
        }
        
        return this;
    }

    protected override void LeavePool()
    {
        base.LeavePool();
        List = PluginPool.GetList<T>();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        PluginPool.FreeList((List<T>)List);
    }
}