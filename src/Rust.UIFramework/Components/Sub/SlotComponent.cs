using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class SlotComponent : SubComponent
{
    public string Filter = JsonDefaults.Common.NullValue;

    public override Utf8String Type => JsonDefaults.Slot.Type;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Slot.FilterName, Filter, JsonDefaults.Common.NullValue);
    }

    public override void Reset()
    {
        base.Reset();
        Filter = null;
    }
}