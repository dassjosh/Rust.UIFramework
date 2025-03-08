using System;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiDirectionalLayout : BaseLayout
{
    public LayoutDirection Direction;
    public float CurrentElement;
    public float DirectionSpacing;
    public float NonDirectionSpacing;
    public UiPadding? Padding;

    public static UiDirectionalLayout Create(in UiReference reference, int numElements, LayoutDirection direction, float directionSpacing, float nonDirectionSpacing, in UiPadding? padding)
    {
        UiDirectionalLayout layout = CreateBase<UiDirectionalLayout>(reference, numElements);
        layout.NumElements = numElements;
        layout.Direction = direction;
        layout.CurrentElement = 0;
        layout.DirectionSpacing = directionSpacing;
        layout.NonDirectionSpacing = nonDirectionSpacing;
        layout.Padding = padding;
        return layout;
    }
    
    public override void Add(BaseUiComponent component) => Add(component, 1f);

    public void Add(BaseUiComponent component, float directionSpan)
    {
        if (CurrentElement + directionSpan > NumElements)
        {
            // Cannot add more components to this column
            return;
        }

        component.Position = GetPosition(directionSpan);
        component.Offset = Padding?.ToOffset() ?? default;
        
        CurrentElement += directionSpan;
    }
    
    public override void OffsetElements(float numElements)
    {
        CurrentElement += numElements;
    }
    
    private UiPosition GetPositionLeftToRight(float startPos, float endPos)
    {
        UiPosition pos = new(startPos, 0, endPos, 1);
        pos = pos.Shrink(DirectionSpacing, NonDirectionSpacing);
        return pos;
    }
    
    private UiPosition GetPositionRightToLeft(float startPos, float endPos)
    {
        UiPosition pos = new(1f - endPos, 0, 1f - startPos, 1);
        pos = pos.Shrink(DirectionSpacing, NonDirectionSpacing);
        return pos;
    }
    
    private UiPosition GetPositionTopToBottom(float startPos, float endPos)
    {
        UiPosition pos = new(0, 1f - endPos, 1, 1f - startPos);
        pos = pos.Shrink(NonDirectionSpacing, DirectionSpacing);
        return pos;
    }
    
    private UiPosition GetPositionBottomToTop(float startPos, float endPos)
    {
        UiPosition pos = new(0, startPos, 1, endPos);
        pos = pos.Shrink(NonDirectionSpacing, DirectionSpacing);
        return pos;
    }
    
    private UiPosition GetPosition(float elementSpan)
    {
        float startPos = CurrentElement / NumElements;
        float endPos = (CurrentElement + elementSpan) / NumElements;
        
        return Direction switch
        {
            LayoutDirection.TopToBottom => GetPositionTopToBottom(startPos, endPos),
            LayoutDirection.LeftToRight => GetPositionLeftToRight(startPos, endPos),
            LayoutDirection.RightToLeft => GetPositionRightToLeft(startPos, endPos),
            LayoutDirection.BottomToTop => GetPositionBottomToTop(startPos, endPos),
            _ => throw new ArgumentOutOfRangeException(nameof(Direction))
        };
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        CurrentElement = 0f;
        Padding = null;
    }
}