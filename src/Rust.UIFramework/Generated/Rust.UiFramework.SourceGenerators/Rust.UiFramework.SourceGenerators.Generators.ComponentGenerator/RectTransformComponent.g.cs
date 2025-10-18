using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class RectTransformComponent : IRectTransformComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> _position = new(Oxide.Ext.UiFramework.Positions.UiPosition.Full);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> _offset = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> _rotation = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Rotation);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _padding = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _changeParent = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _transformIndex = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.SetTransformIndex);

	public Oxide.Ext.UiFramework.Positions.UiPosition Position { get => _position.Value; set => _position.Value = value; }
	public Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get => _offset.Value; set => _offset.Value = value; }
	public Oxide.Ext.UiFramework.Types.UiRotation Rotation { get => _rotation.Value; set => _rotation.Value = value; }
	public Oxide.Ext.UiFramework.Types.UiPadding Padding { get => _padding.Value; set => _padding.Value = value; }
	public string ChangeParent { get => _changeParent.Value; set => _changeParent.Value = value; }
	public int TransformIndex { get => _transformIndex.Value; set => _transformIndex.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> IRectTransformComponentTrackable.Position => _position;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> IRectTransformComponentTrackable.Offset => _offset;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> IRectTransformComponentTrackable.Rotation => _rotation;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> IRectTransformComponentTrackable.Padding => _padding;
	Oxide.Ext.UiFramework.Types.Tracked<string> IRectTransformComponentTrackable.ChangeParent => _changeParent;
	Oxide.Ext.UiFramework.Types.Tracked<int> IRectTransformComponentTrackable.TransformIndex => _transformIndex;

	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetPosition(in Oxide.Ext.UiFramework.Positions.UiPosition position)
	{
		Position = position;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetOffset(in Oxide.Ext.UiFramework.Offsets.UiOffset offset)
	{
		Offset = offset;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetRotation(Oxide.Ext.UiFramework.Types.UiRotation rotation)
	{
		Rotation = rotation;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetPadding(in Oxide.Ext.UiFramework.Types.UiPadding padding)
	{
		Padding = padding;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetChangeParent(string changeParent)
	{
		ChangeParent = changeParent;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetTransformIndex(int transformIndex)
	{
		TransformIndex = transformIndex;
		return this;
	}
	public IRectTransformComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_position.HasChanged || _offset.HasChanged || _rotation.HasChanged || _padding.HasChanged || _changeParent.HasChanged || _transformIndex.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_position.ResetHasChanged();
		_offset.ResetHasChanged();
		_rotation.ResetHasChanged();
		_padding.ResetHasChanged();
		_changeParent.ResetHasChanged();
		_transformIndex.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_position.Reset();
		_offset.Reset();
		_rotation.Reset();
		_padding.Reset();
		_changeParent.Reset();
		_transformIndex.Reset();
	}
}


