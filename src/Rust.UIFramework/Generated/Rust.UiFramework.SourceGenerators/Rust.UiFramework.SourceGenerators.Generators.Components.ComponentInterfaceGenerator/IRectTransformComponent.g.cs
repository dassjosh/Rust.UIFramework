using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IRectTransformComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Positions.UiPosition Position { get; set; }
	Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get; set; }
	Oxide.Ext.UiFramework.Types.UiPadding PositionPadding { get; set; }
	Oxide.Ext.UiFramework.Types.UiPadding OffsetPadding { get; set; }
	Oxide.Ext.UiFramework.Types.UiScale PositionScale { get; set; }
	Oxide.Ext.UiFramework.Types.UiScale OffsetScale { get; set; }
	Oxide.Ext.UiFramework.Types.UiTranslate PositionTranslate { get; set; }
	Oxide.Ext.UiFramework.Types.UiTranslate OffsetTranslate { get; set; }
	Oxide.Ext.UiFramework.Types.UiRotation Rotation { get; set; }
	string ChangeParent { get; set; }
	int TransformIndex { get; set; }
}


