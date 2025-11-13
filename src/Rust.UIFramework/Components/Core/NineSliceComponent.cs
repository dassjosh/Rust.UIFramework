using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(INineSliceComponent))]
[GenerateBuilderMethods]
public partial class NineSliceComponent : ImageComponent, INineSliceComponent
{
    public override ComponentType ComponentType => ComponentType.NineSlice;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.Image.PngName, _png, mode);
        writer.AddField(JsonDefaults.Image.SliceName, _slice, mode);
    }

    public override void Reset()
    {
        base.Reset();
        FillCenter = false;
        ImageType = Image.Type.Sliced;
        base.ResetHasChanged();
    }
}