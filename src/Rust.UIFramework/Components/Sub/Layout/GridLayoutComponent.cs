using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(GridLayoutComponentSerializer))]
public class GridLayoutComponent : BaseLayoutComponent
{
    public Vector2 CellSize;
    public Vector2 Spacing;
    public GridLayoutGroup.Corner StartCorner;
    public GridLayoutGroup.Axis StartAxis;
    public GridLayoutGroup.Constraint Constraint;
    public int ConstraintCount;

    public override Utf8String Type => JsonDefaults.GridLayout.Type;
    public override ComponentType ComponentType => ComponentType.GridLayout;
    
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

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is GridLayoutComponent component) 
        {
            CellSize = component.CellSize;
            Spacing = component.Spacing;
            StartCorner = component.StartCorner;
            StartAxis = component.StartAxis;
            Constraint = component.Constraint;
            ConstraintCount = component.ConstraintCount;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        GridLayoutComponent typedOther = (GridLayoutComponent)other!;
        return CellSize == typedOther.CellSize 
               && Spacing == typedOther.Spacing 
               && StartCorner == typedOther.StartCorner 
               && StartAxis == typedOther.StartAxis 
               && Constraint == typedOther.Constraint 
               && ConstraintCount == typedOther.ConstraintCount;
    }
}