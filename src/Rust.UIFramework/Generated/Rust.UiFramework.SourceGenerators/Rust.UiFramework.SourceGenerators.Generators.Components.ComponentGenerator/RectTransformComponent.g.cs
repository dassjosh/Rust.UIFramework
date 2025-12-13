using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class RectTransformComponent : IRectTransformComponent, IRectTransformComponentTrackable
{
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> _position = new(Oxide.Ext.UiFramework.Positions.UiPosition.Full);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> _offset = new(Oxide.Ext.UiFramework.Offsets.UiOffset.None, Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.FpOffset);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _positionPadding = new(Oxide.Ext.UiFramework.Types.UiPadding.None);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> _offsetPadding = new(Oxide.Ext.UiFramework.Types.UiPadding.None);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> _positionScale = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Scale);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> _offsetScale = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Scale);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> _positionTranslate = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Translate);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> _offsetTranslate = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Translate);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> _rotation = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.Rotation);
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<string> _changeParent = new();
	protected readonly Oxide.Ext.UiFramework.Types.Tracked<int> _transformIndex = new(Oxide.Ext.UiFramework.Json.JsonDefaults.RectTransform.SetTransformIndex);

	public partial Oxide.Ext.UiFramework.Positions.UiPosition Position { get => _position.Value; set => _position.Value = value; }
	public partial Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get => _offset.Value; set => _offset.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiPadding PositionPadding { get => _positionPadding.Value; set => _positionPadding.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiPadding OffsetPadding { get => _offsetPadding.Value; set => _offsetPadding.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiScale PositionScale { get => _positionScale.Value; set => _positionScale.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiScale OffsetScale { get => _offsetScale.Value; set => _offsetScale.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiTranslate PositionTranslate { get => _positionTranslate.Value; set => _positionTranslate.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiTranslate OffsetTranslate { get => _offsetTranslate.Value; set => _offsetTranslate.Value = value; }
	public partial Oxide.Ext.UiFramework.Types.UiRotation Rotation { get => _rotation.Value; set => _rotation.Value = value; }
	public partial string ChangeParent { get => _changeParent.Value; set => _changeParent.Value = value; }
	public partial int TransformIndex { get => _transformIndex.Value; set => _transformIndex.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> IRectTransformComponentTrackable.Position => _position;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> IRectTransformComponentTrackable.Offset => _offset;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> IRectTransformComponentTrackable.PositionPadding => _positionPadding;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> IRectTransformComponentTrackable.OffsetPadding => _offsetPadding;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> IRectTransformComponentTrackable.PositionScale => _positionScale;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> IRectTransformComponentTrackable.OffsetScale => _offsetScale;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> IRectTransformComponentTrackable.PositionTranslate => _positionTranslate;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> IRectTransformComponentTrackable.OffsetTranslate => _offsetTranslate;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> IRectTransformComponentTrackable.Rotation => _rotation;
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
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetPositionPadding(in Oxide.Ext.UiFramework.Types.UiPadding positionPadding)
	{
		PositionPadding = positionPadding;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetOffsetPadding(in Oxide.Ext.UiFramework.Types.UiPadding offsetPadding)
	{
		OffsetPadding = offsetPadding;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetPositionScale(Oxide.Ext.UiFramework.Types.UiScale positionScale)
	{
		PositionScale = positionScale;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetOffsetScale(Oxide.Ext.UiFramework.Types.UiScale offsetScale)
	{
		OffsetScale = offsetScale;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetPositionTranslate(in Oxide.Ext.UiFramework.Types.UiTranslate positionTranslate)
	{
		PositionTranslate = positionTranslate;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetOffsetTranslate(in Oxide.Ext.UiFramework.Types.UiTranslate offsetTranslate)
	{
		OffsetTranslate = offsetTranslate;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.RectTransformComponent SetRotation(Oxide.Ext.UiFramework.Types.UiRotation rotation)
	{
		Rotation = rotation;
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
	public override bool HasChanged() => false || (_position.HasChanged || _offset.HasChanged || _positionPadding.HasChanged || _offsetPadding.HasChanged || _positionScale.HasChanged || _offsetScale.HasChanged || _positionTranslate.HasChanged || _offsetTranslate.HasChanged || _rotation.HasChanged || _changeParent.HasChanged || _transformIndex.HasChanged) || base.HasChanged();
	public override void ResetHasChanged()
	{
		base.ResetHasChanged();
		_position.ResetHasChanged();
		_offset.ResetHasChanged();
		_positionPadding.ResetHasChanged();
		_offsetPadding.ResetHasChanged();
		_positionScale.ResetHasChanged();
		_offsetScale.ResetHasChanged();
		_positionTranslate.ResetHasChanged();
		_offsetTranslate.ResetHasChanged();
		_rotation.ResetHasChanged();
		_changeParent.ResetHasChanged();
		_transformIndex.ResetHasChanged();
	}
	public override void Reset()
	{
		base.Reset();
		_position.Reset();
		_offset.Reset();
		_positionPadding.Reset();
		_offsetPadding.Reset();
		_positionScale.Reset();
		_offsetScale.Reset();
		_positionTranslate.Reset();
		_offsetTranslate.Reset();
		_rotation.Reset();
		_changeParent.Reset();
		_transformIndex.Reset();
	}
}


