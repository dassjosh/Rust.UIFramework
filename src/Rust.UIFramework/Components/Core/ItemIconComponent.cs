using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ItemIconComponent : ImageComponent
{
    public partial int ItemId { get; set; }
    public partial ulong SkinId { get; set; }
    
    public override ComponentType ComponentType => ComponentType.ItemIcon;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.ItemIcon.ItemIdName, ItemIdTracked, mode);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinIdTracked, mode);
    }
}