using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class NineSliceComponent : ImageComponent
{
    public partial string Png { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.Slice))]
    public partial UiBorderWidth Slice { get; set; }
    
    public override ComponentType ComponentType => ComponentType.NineSlice;

    public NineSliceComponent()
    {
        FillCenterTracked.OverrideDefault(false);
        ImageTypeTracked.OverrideDefault(Image.Type.Sliced);
    }
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.Image.PngName, PngTracked, mode);
        writer.AddField(JsonDefaults.Image.SliceName, SliceTracked, mode);
    }
}