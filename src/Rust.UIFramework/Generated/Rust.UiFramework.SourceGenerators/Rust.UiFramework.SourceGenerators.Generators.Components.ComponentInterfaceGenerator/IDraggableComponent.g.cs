using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IDraggableComponent : IBaseTypedComponent
{
	bool LimitToParent { get; set; }
	float MaxDistance { get; set; }
	bool AllowSwapping { get; set; }
	bool DropAnywhere { get; set; }
	float DragAlpha { get; set; }
	int ParentLimitIndex { get; set; }
	string Filter { get; set; }
	UnityEngine.Vector2 ParentPadding { get; set; }
	UnityEngine.Vector2 AnchorOffset { get; set; }
	bool KeepOnTop { get; set; }
	CommunityEntity.DraggablePositionSendType? PositionRpc { get; set; }
	bool MoveToAnchor { get; set; }
	bool RebuildAnchor { get; set; }

	Oxide.Ext.UiFramework.Components.DraggableComponent SetLimitToParent(bool limitToParent);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetMaxDistance(float maxDistance);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetAllowSwapping(bool allowSwapping);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetDropAnywhere(bool dropAnywhere);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetDragAlpha(float dragAlpha);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetParentLimitIndex(int parentLimitIndex);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetFilter(string filter);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetParentPadding(in UnityEngine.Vector2 parentPadding);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetAnchorOffset(in UnityEngine.Vector2 anchorOffset);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetKeepOnTop(bool keepOnTop);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetPositionRpc(CommunityEntity.DraggablePositionSendType? positionRpc);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetMoveToAnchor(bool moveToAnchor);
	Oxide.Ext.UiFramework.Components.DraggableComponent SetRebuildAnchor(bool rebuildAnchor);
}


