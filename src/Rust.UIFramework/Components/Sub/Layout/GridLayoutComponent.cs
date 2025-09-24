using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class GridLayoutComponent : BaseLayoutComponent
{
    public Vector2 CellSize;
    public Vector2 Spacing;
    public GridLayoutGroup.Corner StartCorner;
    public GridLayoutGroup.Axis StartAxis;
    public GridLayoutGroup.Constraint Constraint;
    public int ConstraintCount;

    public override Utf8String Type => JsonDefaults.GridLayout.Type;
    
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
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        base.WriteComponentFields(writer);
        writer.AddField(JsonDefaults.GridLayout.CellSizeName, CellSize, JsonDefaults.GridLayout.CellSize);
        writer.AddField(JsonDefaults.GridLayout.SpacingName, Spacing, JsonDefaults.GridLayout.Spacing);
        writer.AddField(JsonDefaults.GridLayout.StartCornerName, StartCorner, JsonDefaults.GridLayout.StartCorner);
        writer.AddField(JsonDefaults.GridLayout.StartAxisName, StartAxis, JsonDefaults.GridLayout.StartAxis);
        writer.AddField(JsonDefaults.GridLayout.ConstraintName, Constraint, JsonDefaults.GridLayout.Constraint);
        writer.AddField(JsonDefaults.GridLayout.ConstraintCountName, ConstraintCount, JsonDefaults.GridLayout.ConstraintCount);
    }
    
    public override void Reset()
    {
        base.Reset();
        CellSize = JsonDefaults.GridLayout.CellSize;
        Spacing = JsonDefaults.GridLayout.Spacing;
        StartCorner = JsonDefaults.GridLayout.StartCorner;
        StartAxis = JsonDefaults.GridLayout.StartAxis;
        Constraint = JsonDefaults.GridLayout.Constraint;
        ConstraintCount = JsonDefaults.GridLayout.ConstraintCount;
    }
}