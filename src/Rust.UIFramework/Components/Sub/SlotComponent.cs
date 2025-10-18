using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ISlotComponent))]
[GenerateBuilderMethods]
public partial class SlotComponent : SubComponent, ISlotComponent
{
    public override Utf8String Type => JsonDefaults.Slot.Type;
    public override ComponentType ComponentType => ComponentType.Slot;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddTextField(JsonDefaults.Slot.FilterName, _filter, mode);
    }
}