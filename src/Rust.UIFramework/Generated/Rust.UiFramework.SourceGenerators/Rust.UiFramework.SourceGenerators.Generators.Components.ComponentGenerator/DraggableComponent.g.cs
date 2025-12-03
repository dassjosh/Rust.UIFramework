using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class DraggableComponent : IDraggableComponent, IDraggableComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _limitToParent = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.LimitToParent);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _maxDistance = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.MaxDistance);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _allowSwapping = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.AllowSwapping);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _dropAnywhere = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.DropAnywhere);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _dragAlpha = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.DragAlpha);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _parentLimitIndex = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.ParentLimitIndex);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _filter = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _parentPadding = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.ParentPadding);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _anchorOffset = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.AnchorOffset);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _keepOnTop = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.KeepOnTop);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<CommunityEntity.DraggablePositionSendType?> _positionRpc = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.PositionRpc);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _moveToAnchor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.MoveToAnchor);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _rebuildAnchor = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Draggable.RebuildAnchor);

	public partial bool LimitToParent { get => _limitToParent.Value; set => _limitToParent.Value = value; }
	public partial float MaxDistance { get => _maxDistance.Value; set => _maxDistance.Value = value; }
	public partial bool AllowSwapping { get => _allowSwapping.Value; set => _allowSwapping.Value = value; }
	public partial bool DropAnywhere { get => _dropAnywhere.Value; set => _dropAnywhere.Value = value; }
	public partial float DragAlpha { get => _dragAlpha.Value; set => _dragAlpha.Value = value; }
	public partial int ParentLimitIndex { get => _parentLimitIndex.Value; set => _parentLimitIndex.Value = value; }
	public partial string Filter { get => _filter.Value; set => _filter.Value = value; }
	public partial UnityEngine.Vector2 ParentPadding { get => _parentPadding.Value; set => _parentPadding.Value = value; }
	public partial UnityEngine.Vector2 AnchorOffset { get => _anchorOffset.Value; set => _anchorOffset.Value = value; }
	public partial bool KeepOnTop { get => _keepOnTop.Value; set => _keepOnTop.Value = value; }
	public partial CommunityEntity.DraggablePositionSendType? PositionRpc { get => _positionRpc.Value; set => _positionRpc.Value = value; }
	public partial bool MoveToAnchor { get => _moveToAnchor.Value; set => _moveToAnchor.Value = value; }
	public partial bool RebuildAnchor { get => _rebuildAnchor.Value; set => _rebuildAnchor.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.LimitToParent => _limitToParent;
	Oxide.Ext.UiFramework.Types.Tracked<float> IDraggableComponentTrackable.MaxDistance => _maxDistance;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.AllowSwapping => _allowSwapping;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.DropAnywhere => _dropAnywhere;
	Oxide.Ext.UiFramework.Types.Tracked<float> IDraggableComponentTrackable.DragAlpha => _dragAlpha;
	Oxide.Ext.UiFramework.Types.Tracked<int> IDraggableComponentTrackable.ParentLimitIndex => _parentLimitIndex;
	Oxide.Ext.UiFramework.Types.Tracked<string> IDraggableComponentTrackable.Filter => _filter;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IDraggableComponentTrackable.ParentPadding => _parentPadding;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IDraggableComponentTrackable.AnchorOffset => _anchorOffset;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.KeepOnTop => _keepOnTop;
	Oxide.Ext.UiFramework.Types.Tracked<CommunityEntity.DraggablePositionSendType?> IDraggableComponentTrackable.PositionRpc => _positionRpc;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.MoveToAnchor => _moveToAnchor;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IDraggableComponentTrackable.RebuildAnchor => _rebuildAnchor;

	public Oxide.Ext.UiFramework.Components.DraggableComponent SetLimitToParent(bool limitToParent)
	{
		LimitToParent = limitToParent;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetMaxDistance(float maxDistance)
	{
		MaxDistance = maxDistance;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetAllowSwapping(bool allowSwapping)
	{
		AllowSwapping = allowSwapping;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetDropAnywhere(bool dropAnywhere)
	{
		DropAnywhere = dropAnywhere;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetDragAlpha(float dragAlpha)
	{
		DragAlpha = dragAlpha;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetParentLimitIndex(int parentLimitIndex)
	{
		ParentLimitIndex = parentLimitIndex;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetFilter(string filter)
	{
		Filter = filter;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetParentPadding(in UnityEngine.Vector2 parentPadding)
	{
		ParentPadding = parentPadding;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetAnchorOffset(in UnityEngine.Vector2 anchorOffset)
	{
		AnchorOffset = anchorOffset;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetKeepOnTop(bool keepOnTop)
	{
		KeepOnTop = keepOnTop;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetPositionRpc(CommunityEntity.DraggablePositionSendType? positionRpc)
	{
		PositionRpc = positionRpc;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetMoveToAnchor(bool moveToAnchor)
	{
		MoveToAnchor = moveToAnchor;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.DraggableComponent SetRebuildAnchor(bool rebuildAnchor)
	{
		RebuildAnchor = rebuildAnchor;
		return this;
	}
	public IDraggableComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_limitToParent.HasChanged || _maxDistance.HasChanged || _allowSwapping.HasChanged || _dropAnywhere.HasChanged || _dragAlpha.HasChanged || _parentLimitIndex.HasChanged || _filter.HasChanged || _parentPadding.HasChanged || _anchorOffset.HasChanged || _keepOnTop.HasChanged || _positionRpc.HasChanged || _moveToAnchor.HasChanged || _rebuildAnchor.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_limitToParent.ResetHasChanged();
		_maxDistance.ResetHasChanged();
		_allowSwapping.ResetHasChanged();
		_dropAnywhere.ResetHasChanged();
		_dragAlpha.ResetHasChanged();
		_parentLimitIndex.ResetHasChanged();
		_filter.ResetHasChanged();
		_parentPadding.ResetHasChanged();
		_anchorOffset.ResetHasChanged();
		_keepOnTop.ResetHasChanged();
		_positionRpc.ResetHasChanged();
		_moveToAnchor.ResetHasChanged();
		_rebuildAnchor.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_limitToParent.Reset();
		_maxDistance.Reset();
		_allowSwapping.Reset();
		_dropAnywhere.Reset();
		_dragAlpha.Reset();
		_parentLimitIndex.Reset();
		_filter.Reset();
		_parentPadding.Reset();
		_anchorOffset.Reset();
		_keepOnTop.Reset();
		_positionRpc.Reset();
		_moveToAnchor.Reset();
		_rebuildAnchor.Reset();
	}
}


