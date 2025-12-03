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
}


