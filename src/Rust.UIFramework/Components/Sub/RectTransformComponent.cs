using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(RectTransformComponentSerializer))]
public class RectTransformComponent : SubComponent
{
    public UiPosition Position;
    public UiOffset Offset;
    public UiRotation Rotation;
    public UiPadding Padding;
    public string ChangeParent;
    public int TransformIndex;
    
    public override Utf8String Type => JsonDefaults.Common.RectTransformName;
    public override ComponentType ComponentType => ComponentType.RectTransform;
    public override bool AllowMultiple => false;

    public override void Reset()
    {
        base.Reset();
        Position = UiPosition.Full;
        Offset = default;
        Rotation = default;
        Padding = default;
        ChangeParent = default;
        TransformIndex = JsonDefaults.RectTransform.SetTransformIndex;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is RectTransformComponent component)
        {
            Position = component.Position;
            Offset = component.Offset;
            Rotation = component.Rotation;
            Padding = component.Padding;
            ChangeParent = component.ChangeParent;
            TransformIndex = component.TransformIndex;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        RectTransformComponent typedOther = (RectTransformComponent)other!;
        return Position == typedOther.Position 
               && Offset == typedOther.Offset 
               && Rotation == typedOther.Rotation 
               && Padding == typedOther.Padding 
               && ChangeParent == typedOther.ChangeParent 
               && TransformIndex == typedOther.TransformIndex;
    }
}