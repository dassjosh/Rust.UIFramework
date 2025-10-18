using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IGridLayoutComponentTrackable : IBaseLayoutComponentTrackable
{

	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> CellSize { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> Spacing { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Corner> StartCorner { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Axis> StartAxis { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Constraint> Constraint { get; }
	Oxide.Ext.UiFramework.Types.Tracked<int> ConstraintCount { get; }

}


