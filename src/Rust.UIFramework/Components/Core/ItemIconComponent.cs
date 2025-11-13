using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IItemIconComponent))]
[GenerateBuilderMethods]
public partial class ItemIconComponent : ImageComponent, IItemIconComponent
{
    public override ComponentType ComponentType => ComponentType.ItemIcon;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.ItemIcon.ItemIdName, _itemId, mode);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, _skinId, mode);
    }
}