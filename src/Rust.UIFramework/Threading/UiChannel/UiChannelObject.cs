using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading.UiChannel;

internal interface IUiChannelObject
{
    void EnqueueNext();
}

internal interface IUiChannelObject<out T> : IUiChannelObject
{
    T Item { get; }
}

internal class UiChannelObject<T> : BasePoolable, IUiChannelObject<T> where T : IChannelObject
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
        IUiChannel channel = Item.GetChannel(_index);
        if(channel == null)
        {
            TryDispose();
            return;
        }
        
        channel.Enqueue(this);
        _index++;
    }

    protected override void EnterPool()
    {
        _index = 0;
        Item.TryReturnToPool();
    }
}

internal static class UiChannelObjectExt
{
    extension<T>(T item) where T : IChannelObject
    {
        public void Enqueue()
        {
            UiPool.Internal.Get<UiChannelObject<T>>().Init(item).EnqueueNext();
        }
    }
}
