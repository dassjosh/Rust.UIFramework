using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiDockLayout : BaseUiLayout
{
    public UiPadding Padding;
    public readonly List<LayoutState> Elements = [];
    
    public UiDockLayout SetPadding(in UiPadding padding)
    {
        Padding = padding;
        return this;
    }
    
    public override void AddElement(BaseUiComponent element)
    {
        AddElement(element, DockPosition.Center, 0f);
    }

    public void AddElement(BaseUiComponent element, DockPosition position, float dockSize)
    {
        Elements.Add(new LayoutState(element, position, dockSize));
    }

    public override void CalculateElementPositions()
    {
        UiPosition remainingArea = new(0, 0, 1, 1);

        foreach (LayoutState state in Elements)
        {
            UiPosition dockedPosition = CalculateDockedPosition(state, ref remainingArea);
            state.Element.SetPosition(dockedPosition).SetOffsetPadding(Padding);
        }
    }

    private static UiPosition CalculateDockedPosition(in LayoutState state, ref UiPosition remainingArea)
    {
        switch (state.DockPosition)
        {
            case DockPosition.Top:
                UiPosition topDock = remainingArea.SetY(remainingArea.Min.y, remainingArea.Min.y + state.DockSize);
                remainingArea = remainingArea.WithYMin(topDock.Max.y);
                return topDock;

            case DockPosition.Bottom:
                UiPosition bottomDock = remainingArea.SetY(remainingArea.Max.y - state.DockSize, remainingArea.Max.y);
                remainingArea = remainingArea.WithYMax(bottomDock.Min.y);
                return bottomDock;

            case DockPosition.Left:
                UiPosition leftDock = remainingArea.SetX(remainingArea.Min.x, remainingArea.Min.x + state.DockSize);
                remainingArea = remainingArea.WithXMin(leftDock.Max.x);
                return leftDock;

            case DockPosition.Right:
                UiPosition rightDock = remainingArea.SetX(remainingArea.Max.x - state.DockSize, remainingArea.Max.x);
                remainingArea = remainingArea.WithXMax(rightDock.Min.x);
                return rightDock;

            case DockPosition.Center:
                UiPosition centerDock = remainingArea;
                remainingArea = UiPosition.None; // No further adjustments needed
                return centerDock;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Elements.Clear();
        Padding = default;
    }

    public readonly struct LayoutState(BaseUiComponent element, DockPosition dockPosition, float dockSize)
    {
        public readonly BaseUiComponent Element = element;
        public readonly DockPosition DockPosition = dockPosition;
        public readonly float DockSize = dockSize;
    }
}