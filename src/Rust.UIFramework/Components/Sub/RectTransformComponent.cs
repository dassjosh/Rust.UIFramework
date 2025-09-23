using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Rotation;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Components;

public class RectTransformComponent : SubComponent
{
    public UiPosition Position;
    public UiOffset Offset;
    public UiRotation Rotation;
    public UiPadding Padding;
    public string ChangeParent;
    public int TransformIndex;
    
    public override Utf8String Type => JsonDefaults.Common.RectTransformName;
    public override bool AllowMultiple => false;
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        UiOffset computedOffset = Offset + Padding;
        
        writer.AddField(JsonDefaults.RectTransform.AnchorMinName, Position.Min, JsonDefaults.RectTransform.AnchorMin);
        writer.AddField(JsonDefaults.RectTransform.AnchorMaxName, Position.Max, JsonDefaults.RectTransform.AnchorMax);
        writer.AddField(JsonDefaults.RectTransform.OffsetMinName, computedOffset.Min, JsonDefaults.RectTransform.OffsetMin);
        writer.AddField(JsonDefaults.RectTransform.OffsetMaxName, computedOffset.Max, JsonDefaults.RectTransform.OffsetMax);
        writer.AddField(JsonDefaults.RectTransform.RotationName, Rotation.Rotation, JsonDefaults.RectTransform.Rotation);
        writer.AddField(JsonDefaults.RectTransform.SetTransformIndexName, TransformIndex, JsonDefaults.RectTransform.SetTransformIndex);
        if (!string.IsNullOrEmpty(ChangeParent))
        {
            writer.AddFieldRaw(JsonDefaults.RectTransform.SetParentName, ChangeParent);
        }
    }

    public override void Reset()
    {
        base.Reset();
        Position = UiPosition.Full;
        Offset = default;
        Rotation = default;
        Padding = default;
        ChangeParent = default;
        TransformIndex = -1;
    }
}