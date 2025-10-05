using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class RectTransformComponent : SubComponent
{
    private readonly TrackedValue<UiPosition> _position = new(UiPosition.Full);
    private readonly TrackedValue<UiOffset> _offset = new();
    private readonly TrackedValue<UiRotation> _rotation = new();
    private readonly TrackedValue<UiPadding> _padding = new();
    private readonly TrackedValue<string> _changeParent = new();
    private readonly TrackedValue<int> _transformIndex = new(JsonDefaults.RectTransform.SetTransformIndex);
    
    public UiPosition Position { get => _position.Value; set => _position.Value = value; }
    public UiOffset Offset { get => _offset.Value; set => _offset.Value = value; }
    public UiRotation Rotation { get => _rotation.Value; set => _rotation.Value = value; }
    public UiPadding Padding { get => _padding.Value; set => _padding.Value = value; }
    public string ChangeParent { get => _changeParent.Value; set => _changeParent.Value = value; }
    public int TransformIndex { get => _transformIndex.Value; set => _transformIndex.Value = value; }
    
    public override Utf8String Type => JsonDefaults.Common.RectTransformName;
    public override ComponentType ComponentType => ComponentType.RectTransform;
    public override bool AllowMultiple => false;


    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(_position, mode);

        if (_padding.ShouldSerialize(mode))
        {
            Offset += _padding.Value;
        }
        
        writer.AddField(_offset, mode);
        writer.AddField(JsonDefaults.RectTransform.RotationName, _rotation, mode);
        writer.AddField(JsonDefaults.RectTransform.SetTransformIndexName, _transformIndex, mode);
    }
    
    public override bool HasChanged()
    {
        return _position.HasChanged || _offset.HasChanged || _rotation.HasChanged || _padding.HasChanged || _changeParent.HasChanged || _transformIndex.HasChanged;
    }

    public override void Reset()
    {
        base.Reset();
        _position.Reset();
        _offset.Reset();
        _rotation.Reset();
        _padding.Reset();
        _changeParent.Reset();
        _transformIndex.Reset();
    }
}