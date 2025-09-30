using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(NineSliceComponentSerializer))]
public class NineSliceComponent : ImageComponent
{
    public string Png;
    public UiBorderWidth Slice;
    
    public override ComponentType ComponentType => ComponentType.NineSlice;

    public override void Reset()
    {
        base.Reset();
        Png = null;
        Slice = JsonDefaults.Image.Slice;
        FillCenter = false;
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        NineSliceComponent typedOther = (NineSliceComponent)other!;
        return Png == typedOther.Png 
               && Slice == typedOther.Slice;
    }
}