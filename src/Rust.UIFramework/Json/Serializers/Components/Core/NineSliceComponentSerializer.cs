using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class NineSliceComponentSerializer : ImageComponentSerializer<NineSliceComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, NineSliceComponent component, NineSliceComponent defaults, SerializeMode mode)
    {
        base.SerializeComponent(writer, component, defaults, mode);
        writer.AddFieldRaw(JsonDefaults.Image.PngName, component.Png); //PNG needs to always be provided for update else the slice won't change
        writer.AddField(JsonDefaults.Image.SliceName, component.Slice, defaults.Slice);
    }
}