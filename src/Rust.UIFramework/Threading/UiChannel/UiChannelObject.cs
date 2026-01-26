using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiChannelObject<T> : BasePoolable, IUiChannelObject<T> where T : IChannelObject<T>
{
    private int _index;
    public T Item { get; private set; }

    internal UiChannelObject<T> Init(T item)
    {
        Item = item;
        return this;
    }

    public void EnqueueNext()
    {
        IUiChannel<T> channel = Item.GetChannel(_index);
        if(channel == null)
        {
            TryDispose();
            return;
        }
        
        _index++;
        channel.Enqueue(this);
    }

    protected override void EnterPool()
    {
        _index = 0;
        Item.TryReturnToPool();
    }
}

internal static class UiChannelObjectExt
{
    extension<T>(T item) where T : IChannelObject<T>
    {
        public void Enqueue()
        {
            UiPool.Internal.Get<UiChannelObject<T>>().Init(item).EnqueueNext();
        }
    }
}
