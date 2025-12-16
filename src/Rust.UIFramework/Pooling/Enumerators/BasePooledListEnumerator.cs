using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

public abstract class BasePooledListEnumerator<T> : BasePooledEnumerator<T>
{
    protected IList<T> List;
    private int _currentIndex = -1;
    public override T Current { get; protected set; }
    
    public override bool MoveNext()
    {
        int nextIndex = _currentIndex + 1;
        if (nextIndex < List.Count)
        {
            Current = List[nextIndex];
            _currentIndex = nextIndex;
            return true;
        }

        Current = default;
        return false;
    }
    
    public override void Reset()
    {
        _currentIndex = -1;
    }

    protected override void EnterPool()
    {
        _currentIndex = -1;
    }
}