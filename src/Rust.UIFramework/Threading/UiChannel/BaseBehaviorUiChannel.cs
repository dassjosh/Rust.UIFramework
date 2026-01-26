using System.Collections;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseBehaviorUiChannel<T> : BaseUiChannel<T> where T : IChannelObject<T>
{
    private readonly Worker _worker;
    private readonly int _maxConcurrency;
    private int _activeCoroutines;

    protected BaseBehaviorUiChannel(int maxConcurrency)
    {
        GameObject go = new(GetType().GetRealTypeName());
        _worker = go.AddComponent<Worker>();
        _maxConcurrency = maxConcurrency;
    }
    
    public override void Enqueue(IUiChannelObject<T> item)
    {
        base.Enqueue(item);
        TryStartNext();
    }
    
    private void TryStartNext()
    {
        if (_activeCoroutines >= _maxConcurrency || Queue.IsEmpty)
        {
            return;
        }

        if (Queue.TryDequeue(out IUiChannelObject<T> item))
        {
            _activeCoroutines++;
            _worker.StartCoroutine(ProcessItem(item.Item).SafeCoroutine(() =>
            {
                _activeCoroutines--;
                TryStartNext();
                item.EnqueueNext();
            }));
        }
    }
    
    public abstract IEnumerator ProcessItem(T item);

    private sealed class Worker : FacepunchBehaviour;
    
    internal override void OnServerShutdown()
    {
        base.OnServerShutdown();
        _worker.StopAllCoroutines();
    }
}