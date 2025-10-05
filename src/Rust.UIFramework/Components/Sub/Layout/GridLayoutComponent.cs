using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class GridLayoutComponent : BaseLayoutComponent
{
    private readonly TrackedValue<Vector2> _cellSize = new(JsonDefaults.GridLayout.CellSize);
    private readonly TrackedValue<Vector2> _spacing = new(JsonDefaults.GridLayout.Spacing);
    private readonly TrackedValue<GridLayoutGroup.Corner> _startCorner = new(JsonDefaults.GridLayout.StartCorner);
    private readonly TrackedValue<GridLayoutGroup.Axis> _startAxis = new(JsonDefaults.GridLayout.StartAxis);
    private readonly TrackedValue<GridLayoutGroup.Constraint> _constraint = new(JsonDefaults.GridLayout.Constraint);
    private readonly TrackedValue<int> _constraintCount = new(JsonDefaults.GridLayout.ConstraintCount);
    
    public Vector2 CellSize { get => _cellSize.Value; set => _cellSize.Value = value; }
    public Vector2 Spacing { get => _spacing.Value; set => _spacing.Value = value; }
    public GridLayoutGroup.Corner StartCorner { get => _startCorner.Value; set => _startCorner.Value = value; }
    public GridLayoutGroup.Axis StartAxis { get => _startAxis.Value; set => _startAxis.Value = value; }
    public GridLayoutGroup.Constraint Constraint { get => _constraint.Value; set => _constraint.Value = value; }
    public int ConstraintCount { get => _constraintCount.Value; set => _constraintCount.Value = value; }

    public override Utf8String Type => JsonDefaults.GridLayout.Type;
    public override ComponentType ComponentType => ComponentType.GridLayout;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.GridLayout.CellSizeName, _cellSize, mode);
        writer.AddField(JsonDefaults.GridLayout.SpacingName, _spacing, mode);
        writer.AddField(JsonDefaults.GridLayout.StartCornerName, _startCorner, mode);
        writer.AddField(JsonDefaults.GridLayout.StartAxisName, _startAxis, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintName, _constraint, mode);
        writer.AddField(JsonDefaults.GridLayout.ConstraintCountName, _constraintCount, mode);
    }
    
    public GridLayoutComponent SetCellSize(Vector2 cellSize)
    {
        CellSize = cellSize;
        return this;
    }

    public GridLayoutComponent SetSpacing(Vector2 spacing)
    {
        Spacing = spacing;
        return this;
    }

    public GridLayoutComponent SetStartCorner(GridLayoutGroup.Corner startCorner)
    {
        StartCorner = startCorner;
        return this;
    }

    public GridLayoutComponent SetStartAxis(GridLayoutGroup.Axis startAxis)
    {
        StartAxis = startAxis;
        return this;
    }
    
    public GridLayoutComponent SetConstraint(GridLayoutGroup.Constraint constraint)
    {
        Constraint = constraint;
        return this;
    }
    
    public GridLayoutComponent SetConstraintCount(int constraintCount)
    {
        ConstraintCount = constraintCount;
        return this;
    }
    
    public override bool HasChanged()
    {
        return _cellSize.HasChanged
               || _spacing.HasChanged
               || _startCorner.HasChanged
               || _startAxis.HasChanged
               || _constraint.HasChanged
               || _constraintCount.HasChanged;
    }

    public override void Reset()
    {
        base.Reset();
        _cellSize.Reset();
        _spacing.Reset();
        _startCorner.Reset();
        _startAxis.Reset();
        _constraint.Reset();
        _constraintCount.Reset();
    }
}