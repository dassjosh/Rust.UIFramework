using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IRectTransformComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> Position { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> Offset { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> PositionPadding { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiPadding> OffsetPadding { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> PositionScale { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiScale> OffsetScale { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> PositionTranslate { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiTranslate> OffsetTranslate { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiRotation> Rotation { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> ChangeParent { get; }
	Oxide.Ext.UiFramework.Types.Tracked<int> TransformIndex { get; }
}


