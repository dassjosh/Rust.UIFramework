using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IGridLayoutComponent : IBaseLayoutComponent
{
	UnityEngine.Vector2 CellSize { get; set; }
	UnityEngine.Vector2 Spacing { get; set; }
	UnityEngine.UI.GridLayoutGroup.Corner StartCorner { get; set; }
	UnityEngine.UI.GridLayoutGroup.Axis StartAxis { get; set; }
	UnityEngine.UI.GridLayoutGroup.Constraint Constraint { get; set; }
	int ConstraintCount { get; set; }
}


