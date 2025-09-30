using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ScrollViewContentComponentSerializer))]
public class ScrollViewContentComponent : ChildComponent
{
    public UiPosition Position;
    public UiOffset Offset;
    public Vector2 Pivot;
    
    public override ComponentType ComponentType => ComponentType.ScrollView;
    
    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null, in Vector2? pivot = null)
    {
        if (position.HasValue)
        {
            Position = position.Value;
        }

        if (offset.HasValue)
        {
            Offset = offset.Value;
        }
        
        if (pivot.HasValue)
        {
            Pivot = pivot.Value;
        }
    }

    public override void Reset()
    {
        Position = UiPosition.Full;
        Offset = default;
        Pivot = JsonDefaults.ScrollView.Pivot;
    }

    public override void CopyFrom(object value)
    {
        if (value is ScrollViewContentComponent component)
        {
            Position = component.Position;
            Offset = component.Offset;
            Pivot = component.Pivot;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        ScrollViewContentComponent typedOther = (ScrollViewContentComponent)other!;
        return Position == typedOther.Position 
               && Offset == typedOther.Offset 
               && Pivot == typedOther.Pivot;
    }
}