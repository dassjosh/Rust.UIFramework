using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ScrollViewContentComponent : IScrollViewContentComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> _position = new(Oxide.Ext.UiFramework.Positions.UiPosition.Full);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> _offset = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _pivot = new(Oxide.Ext.UiFramework.Json.JsonDefaults.ScrollView.Pivot);

	public Oxide.Ext.UiFramework.Positions.UiPosition Position { get => _position.Value; set => _position.Value = value; }
	public Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get => _offset.Value; set => _offset.Value = value; }
	public UnityEngine.Vector2 Pivot { get => _pivot.Value; set => _pivot.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> IScrollViewContentComponentTrackable.Position => _position;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> IScrollViewContentComponentTrackable.Offset => _offset;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IScrollViewContentComponentTrackable.Pivot => _pivot;

	public Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetPosition(in Oxide.Ext.UiFramework.Positions.UiPosition position)
	{
		Position = position;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetOffset(in Oxide.Ext.UiFramework.Offsets.UiOffset offset)
	{
		Offset = offset;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetPivot(in UnityEngine.Vector2 pivot)
	{
		Pivot = pivot;
		return this;
	}
	public IScrollViewContentComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_position.HasChanged || _offset.HasChanged || _pivot.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_position.ResetHasChanged();
		_offset.ResetHasChanged();
		_pivot.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_position.Reset();
		_offset.Reset();
		_pivot.Reset();
	}
}


