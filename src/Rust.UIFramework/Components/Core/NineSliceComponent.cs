using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class NineSliceComponent : ImageComponent
{
    private readonly TrackedValue<string> _png = new();
    private readonly TrackedValue<UiBorderWidth> _slice = new(JsonDefaults.Image.Slice);
    
    public string Png { get => _png.Value; set => _png.Value = value; }
    public UiBorderWidth Slice { get => _slice.Value; set => _slice.Value = value; }
    
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
        _png.Reset();
        _slice.Reset();
        FillCenter = false;
        ImageType = Image.Type.Sliced;
    }
}