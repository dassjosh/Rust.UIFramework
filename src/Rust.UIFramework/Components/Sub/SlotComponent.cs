using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(SlotComponentSerializer))]
public class SlotComponent : SubComponent
{
    private readonly TrackedValue<string> _filter = new();
    
    public string Filter { get => _filter.Value; set => _filter.Value = value; }

    public override Utf8String Type => JsonDefaults.Slot.Type;
    public override ComponentType ComponentType => ComponentType.Slot;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddTextField(JsonDefaults.Slot.FilterName, _filter, mode);
    }
    
    public override bool HasChanged()
    {
        return _filter.HasChanged;
    }

    public override void Reset()
    {
        base.Reset();
        _filter.Reset();
    }
    
    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is SlotComponent component)
        {
            Filter = component.Filter;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        SlotComponent typedOther = (SlotComponent)other!;
        return Filter == typedOther.Filter;
    }
}