using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class NineSliceComponent : ImageComponent
{
    public string Png;
    public UiBorderWidth Slice;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        base.WriteComponentFields(writer);
        writer.AddFieldRaw(JsonDefaults.Image.PngName, Png);
        writer.AddField(JsonDefaults.Image.SliceName, Slice, JsonDefaults.Image.Slice);
    }

    public override void Reset()
    {
        base.Reset();
        Png = null;
        Slice = JsonDefaults.Image.Slice;
        FillCenter = false;
    }
}