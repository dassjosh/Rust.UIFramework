using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;

public partial class GridLayoutComponent : IGridLayoutComponent, IGridLayoutComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _cellSize = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.CellSize);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> _spacing = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.Spacing);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Corner> _startCorner = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.StartCorner);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Axis> _startAxis = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.StartAxis);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Constraint> _constraint = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.Constraint);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _constraintCount = new(Oxide.Ext.UiFramework.Json.JsonDefaults.GridLayout.ConstraintCount);

	public partial UnityEngine.Vector2 CellSize { get => _cellSize.Value; set => _cellSize.Value = value; }
	public partial UnityEngine.Vector2 Spacing { get => _spacing.Value; set => _spacing.Value = value; }
	public partial UnityEngine.UI.GridLayoutGroup.Corner StartCorner { get => _startCorner.Value; set => _startCorner.Value = value; }
	public partial UnityEngine.UI.GridLayoutGroup.Axis StartAxis { get => _startAxis.Value; set => _startAxis.Value = value; }
	public partial UnityEngine.UI.GridLayoutGroup.Constraint Constraint { get => _constraint.Value; set => _constraint.Value = value; }
	public partial int ConstraintCount { get => _constraintCount.Value; set => _constraintCount.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IGridLayoutComponentTrackable.CellSize => _cellSize;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> IGridLayoutComponentTrackable.Spacing => _spacing;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Corner> IGridLayoutComponentTrackable.StartCorner => _startCorner;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Axis> IGridLayoutComponentTrackable.StartAxis => _startAxis;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.GridLayoutGroup.Constraint> IGridLayoutComponentTrackable.Constraint => _constraint;
	Oxide.Ext.UiFramework.Types.Tracked<int> IGridLayoutComponentTrackable.ConstraintCount => _constraintCount;

	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetCellSize(UnityEngine.Vector2 cellSize)
	{
		CellSize = cellSize;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetSpacing(UnityEngine.Vector2 spacing)
	{
		Spacing = spacing;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetStartCorner(UnityEngine.UI.GridLayoutGroup.Corner startCorner)
	{
		StartCorner = startCorner;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetStartAxis(UnityEngine.UI.GridLayoutGroup.Axis startAxis)
	{
		StartAxis = startAxis;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetConstraint(UnityEngine.UI.GridLayoutGroup.Constraint constraint)
	{
		Constraint = constraint;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.GridLayoutComponent SetConstraintCount(int constraintCount)
	{
		ConstraintCount = constraintCount;
		return this;
	}
	public IGridLayoutComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_cellSize.HasChanged || _spacing.HasChanged || _startCorner.HasChanged || _startAxis.HasChanged || _constraint.HasChanged || _constraintCount.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_cellSize.ResetHasChanged();
		_spacing.ResetHasChanged();
		_startCorner.ResetHasChanged();
		_startAxis.ResetHasChanged();
		_constraint.ResetHasChanged();
		_constraintCount.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_cellSize.Reset();
		_spacing.Reset();
		_startCorner.Reset();
		_startAxis.Reset();
		_constraint.Reset();
		_constraintCount.Reset();
	}
}


