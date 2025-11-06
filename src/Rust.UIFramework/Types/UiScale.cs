using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiScale(float Horizontal, float Vertical)
{
    public bool HasScale => !Mathf.Approximately(Horizontal, 1f) || !Mathf.Approximately(Vertical, 1f);
    
    public static UiScale Lerp(UiScale start, UiScale end, float progress)
    {
        return new UiScale(Mathf.Lerp(start.Horizontal, end.Horizontal, progress), Mathf.Lerp(start.Vertical, end.Vertical, progress));
    }
}