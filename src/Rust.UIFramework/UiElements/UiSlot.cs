using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiSlot : BaseUiImage
{
    public readonly SlotComponent Slot = new();

    public static UiSlot Create(in UiPosition pos, in UiOffset offset, UiColor color, string filter = null)
    {
        UiSlot slot = CreateBase<UiSlot>(pos, offset);
        slot.Image.Color = color;
        slot.Slot.Filter = filter;
        return slot;
    }

    public void SetFilter(string filter)
    {
        Slot.Filter = filter;
    }

    public void SetEnabled(bool enabled)
    {
        Slot.Enabled = enabled;
    }

    protected override void WriteComponents(JsonFrameworkWriter writer)
    {
        Image.WriteComponent(writer);
        Slot.WriteComponent(writer);
        base.WriteComponents(writer);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Slot.Reset();
    }
}
