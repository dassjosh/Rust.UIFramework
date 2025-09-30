using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(SlotComponentSerializer))]
public class SlotComponent : SubComponent
{
    public string Filter = JsonDefaults.Common.NullValue;

    public override Utf8String Type => JsonDefaults.Slot.Type;
    public override ComponentType ComponentType => ComponentType.Slot;
    public override bool AllowMultiple => false;

    public override void Reset()
    {
        base.Reset();
        Filter = null;
    }
    
    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is SlotComponent component)
        {
            Filter = component.Filter;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        SlotComponent typedOther = (SlotComponent)other!;
        return Filter == typedOther.Filter;
    }
}