using System.Collections;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

public abstract class BasePooledEnumerator<T> : BasePoolable, IEnumerator<T>, IEnumerable<T>
{
    public abstract T Current { get; protected set; }

    object IEnumerator.Current => Current;
    
    public abstract bool MoveNext();

    public virtual void Reset()
    {
        Current = default;
    }
    public IEnumerator<T> GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}