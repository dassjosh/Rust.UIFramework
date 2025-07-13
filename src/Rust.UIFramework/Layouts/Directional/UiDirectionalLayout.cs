using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Padding;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiDirectionalLayout : BaseUiLayout, IFixedElementsLayout
{
    public int NumElements { get; set; }
    public LayoutDirection Direction;
    public LayoutAlignment Alignment;
    public LayoutPadding LayoutPadding;
    public UiPadding Padding;
    public readonly List<LayoutState> Elements = [];

    public static UiDirectionalLayout Create(UiPluginPool pool, in UiReference reference, int numElements, LayoutDirection direction, LayoutAlignment alignment, LayoutPadding layoutPadding, in UiPadding padding)
    {
        UiDirectionalLayout layout = CreateBase<UiDirectionalLayout>(pool, reference);
        layout.NumElements = numElements;
        layout.Direction = direction;
        layout.Alignment = alignment;
        layout.LayoutPadding = layoutPadding;
        layout.Padding = padding;
        return layout;
    }

    public UiDirectionalLayout SetNumElements(int numElements)
    {
        NumElements = numElements;
        return this;
    }

    public UiDirectionalLayout SetDirection(LayoutDirection direction)
    {
        Direction = direction;
        return this;
    }

    public UiDirectionalLayout SetAlignment(LayoutAlignment alignment)
    {
        Alignment = alignment;
        return this;
    }

    public UiDirectionalLayout SetLayoutPadding(LayoutPadding layoutPadding)
    {
        LayoutPadding = layoutPadding;
        return this;
    }

    public UiDirectionalLayout SetPadding(in UiPadding padding)
    {
        Padding = padding;
        return this;
    }

    public override void AddElement(BaseUiComponent element) => AddElement(element, 1f);

    public void AddElement(BaseUiComponent element, float elementSpan)
    {
        Elements.Add(new LayoutState(element, elementSpan));
    }

    public override void CalculateElementPositions()
    {
        float totalSpan = Math.Max(NumElements, Elements.Sum(e => e.ElementSpan));
        float scale = GetScrollViewScale(totalSpan, NumElements);
        float currentElement = GetElementOffset(totalSpan) * scale;

        UiOffset padding = Padding.ToOffset();
        
        for (int index = 0; index < Elements.Count; index++)
        {
            LayoutState state = Elements[index];
            state.Element.SetPosition(GetUiPosition(state, currentElement, totalSpan, scale), padding);
            currentElement += state.ElementSpan;
        }

        ScaleScrollView(Direction, scale);
    }
    
    private float GetElementOffset(float numElements) => GetAlignmentOffset(Alignment, numElements, Mathf.Max(Elements.Count, NumElements));
    
    private UiPosition GetUiPosition(in LayoutState state, float currentElement, float numElements, float scale)
    {
        float startPos = currentElement / numElements * scale;
        float endPos = ((currentElement + state.ElementSpan) / numElements) * scale;
        
        UiPosition pos = Direction switch
        {
            LayoutDirection.Vertical => new UiPosition(startPos, 0, endPos, 1),
            LayoutDirection.Horizontal => new UiPosition(0, 1f - endPos, 1, 1f - startPos),
            _ => throw new ArgumentOutOfRangeException(nameof(Direction))
        };
        
        return Direction == LayoutDirection.Horizontal 
            ? pos.Shrink(LayoutPadding.Horizontal * scale, LayoutPadding.Vertical)
            : pos.Shrink(LayoutPadding.Horizontal, LayoutPadding.Vertical * scale);
    }
    
    public readonly struct LayoutState(BaseUiComponent element, float elementSpan)
    {
        public readonly BaseUiComponent Element = element;
        public readonly float ElementSpan = elementSpan;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Elements.Clear();
    }
}