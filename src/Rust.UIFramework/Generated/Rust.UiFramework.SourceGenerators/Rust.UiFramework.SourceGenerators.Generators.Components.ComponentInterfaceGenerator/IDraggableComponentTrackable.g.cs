using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IDraggableComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<bool> LimitToParent { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> MaxDistance { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> AllowSwapping { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> DropAnywhere { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> DragAlpha { get; }
	Oxide.Ext.UiFramework.Types.Tracked<int> ParentLimitIndex { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Filter { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> ParentPadding { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> AnchorOffset { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> KeepOnTop { get; }
	Oxide.Ext.UiFramework.Types.Tracked<CommunityEntity.DraggablePositionSendType?> PositionRpc { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> MoveToAnchor { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> RebuildAnchor { get; }
}


