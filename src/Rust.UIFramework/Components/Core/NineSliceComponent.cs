using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class NineSliceComponent : ImageComponent
{
    public string Png;
    public UiBorderWidth Slice;
    
    public override ComponentType ComponentType => ComponentType.NineSlice;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddFieldRaw(JsonDefaults.Image.PngName, Png);
        writer.AddFieldRaw(JsonDefaults.Image.SliceName, Slice);
    }

    public override void Reset()
    {
        base.Reset();
        Png = null;
        Slice = JsonDefaults.Image.Slice;
        FillCenter = false;
        ImageType = Image.Type.Sliced;
    }
}