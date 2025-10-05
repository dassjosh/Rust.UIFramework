using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class NineSliceComponent : ImageComponent
{
    private readonly TrackedValue<UiBorderWidth> _slice = new(JsonDefaults.Image.Slice);

    public string Png;
    public UiBorderWidth Slice  { get => _slice.Value; set => _slice.Value = value; }
    
    public override ComponentType ComponentType => ComponentType.NineSlice;

    public NineSliceComponent()
    {
        _fillCenter.OverrideDefault(false);
    }

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddFieldRaw(JsonDefaults.Image.PngName, Png); //PNG needs to always be provided for update else the slice won't change
        writer.AddField(JsonDefaults.Image.SliceName, _slice, mode);
    }

    public override void Reset()
    {
        base.Reset();
        Png = null;
        _slice.Reset();
        _fillCenter.Reset();
    }
}