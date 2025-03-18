using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiFlexBoxLayout : BaseLayout
{
    public FlexDirection Direction;
    public FlexWrap Wrap;
    public FlexCrossAlignment CrossAlignment;
    public FlexJustifyContent DefaultJustifyContent;
    public UiPadding Padding;
    public float Gap;
    public readonly List<LayoutState> Elements = [];
    
    private readonly List<List<LayoutState>> _lines = [];
    private readonly Dictionary<FlexJustifyContent, List<int>> _groups = new();

    public static UiFlexBoxLayout Create(
        in UiReference reference,
        FlexDirection direction,
        FlexWrap wrap,
        FlexCrossAlignment crossAlignment,
        FlexJustifyContent defaultJustifyContent,
        in UiPadding padding,
        float gap = 0f)
    {
        UiFlexBoxLayout layout = CreateBase<UiFlexBoxLayout>(reference);
        layout.Direction = direction;
        layout.Wrap = wrap;
        layout.CrossAlignment = crossAlignment;
        layout.DefaultJustifyContent = defaultJustifyContent;
        layout.Padding = padding;
        layout.Gap = gap;
        return layout;
    }

    public override void AddElement(BaseUiComponent element) => AddElement(element, 0f);

    public void AddElement(BaseUiComponent element, float flexBasis = 0f, float flexGrow = 1f, float flexShrink = 1f, FlexJustifyContent? justifyContent = null)
    {
        Elements.Add(new LayoutState(element, flexBasis, flexGrow, flexShrink, justifyContent ?? DefaultJustifyContent));
    }

    public override void CalculateElementPositions()
    {
        UiOffset padding = Padding.ToOffset();
        GenerateElementLines();
        
        for (int lineIndex = 0; lineIndex < _lines.Count; lineIndex++)
        {
            HandleLinePositioning(_lines[lineIndex], lineIndex, padding);
        }
    }

    private void HandleLinePositioning(List<LayoutState> line, int lineIndex, in UiOffset padding)
    {
        float crossAxisSize = _lines.Count;
        float totalFlexBasis = GetTotalFlexBasis(line);
        float totalGap = (line.Count - 1) * Gap;
        float totalMainAxisSpan = totalFlexBasis + totalGap;
        float availableSpace = 1f - totalMainAxisSpan;

        GenerateJustifyContentGroups(line);

        foreach ((FlexJustifyContent justifyContent, List<int> indices) in _groups)
        {
            float groupFlexBasis = 0f;
            float groupFlexGrow = 0f;
            float groupFlexShrink = 0f;
            
            // Calculate group totals
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                LayoutState state = line[index];
                groupFlexBasis += state.FlexBasis;
                groupFlexGrow += state.FlexGrow;
                groupFlexShrink += state.FlexShrink;
            }

            // Calculate group's share of available space
            float groupShareOfAvailableSpace = availableSpace * (groupFlexBasis / totalFlexBasis);
            
            // Calculate offset for this justify content group
            float groupOffset = CalculateJustifyContentOffset(groupShareOfAvailableSpace, indices.Count, justifyContent);
            float currentMainPos = groupOffset;
            
            // Position elements in this group
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                LayoutState state = line[index];
                float elementSpan = CalculateElementSpan(state, groupShareOfAvailableSpace, groupFlexGrow, groupFlexShrink);
                SetElementPosition(state, currentMainPos, elementSpan, lineIndex, crossAxisSize, padding);
                currentMainPos += elementSpan + Gap;
            }
        }
    }
    
    private void GenerateJustifyContentGroups(List<LayoutState> line)
    {
        for (int i = 0; i < line.Count; i++)
        {
            FlexJustifyContent justifyContent = line[i].JustifyContent;
            
            if (!_groups.TryGetValue(justifyContent, out List<int> indices))
            {
                _groups[justifyContent] = indices = UiFrameworkPool.GetList<int>();
            }
            
            indices.Add(i);
        }
    }
    
    private static float GetTotalFlexBasis(List<LayoutState> lines)
    {
        float totalFlexBasis = 0f;

        for (int index = 0; index < lines.Count; index++)
        {
            LayoutState state = lines[index];
            totalFlexBasis += state.FlexBasis;
        }

        return totalFlexBasis;
    }

    private static float CalculateElementSpan(in LayoutState state, float availableSpace, float totalFlexGrow, float totalFlexShrink)
    {
        float elementSpan = state.FlexBasis;

        if (availableSpace > 0 && totalFlexGrow > 0)
        {
            // Distribute extra space based on FlexGrow
            elementSpan += state.FlexGrow / totalFlexGrow * availableSpace;
        }
        else if (availableSpace < 0 && totalFlexShrink > 0)
        {
            // Reduce size based on FlexShrink
            elementSpan += state.FlexShrink / totalFlexShrink * availableSpace;
        }

        return elementSpan;
    }

    private void SetElementPosition(in LayoutState state, float currentMainPos, float elementSpan, int lineIndex, float crossAxisSize, in UiOffset padding)
    {
        (float crossStart, float crossEnd) = CalculateCrossAlignment(lineIndex, crossAxisSize);

        UiPosition position = Direction switch
        {
            FlexDirection.Row or FlexDirection.RowReverse => new UiPosition(
                currentMainPos, crossStart,
                currentMainPos + elementSpan, crossEnd),

            FlexDirection.Column or FlexDirection.ColumnReverse => new UiPosition(
                crossStart, currentMainPos,
                crossEnd, currentMainPos + elementSpan),

            _ => throw new ArgumentOutOfRangeException(nameof(Direction)),
        };

        state.Element.SetPosition(position, padding);
    }

    private (float crossStart, float crossEnd) CalculateCrossAlignment(int lineIndex, float crossAxisSize)
    {
        return CrossAlignment switch
        {
            FlexCrossAlignment.Start => (lineIndex / crossAxisSize, (lineIndex + 1) / crossAxisSize),
            FlexCrossAlignment.Center => (
                lineIndex / crossAxisSize + 1f / (2 * crossAxisSize),
                (lineIndex + 1) / crossAxisSize - 1f / (2 * crossAxisSize)),
            FlexCrossAlignment.End => (
                (lineIndex + 1) / crossAxisSize - 1f / crossAxisSize,
                (lineIndex + 1) / crossAxisSize),
            FlexCrossAlignment.Stretch => (lineIndex / crossAxisSize, (lineIndex + 1) / crossAxisSize),
            _ => throw new ArgumentOutOfRangeException(nameof(CrossAlignment)),
        };
    }

    private void GenerateElementLines()
    {
        List<LayoutState> currentLine = UiFrameworkPool.GetList<LayoutState>();
        float currentLineSpan = 0f;

        for (int index = 0; index < Elements.Count; index++)
        {
            LayoutState state = Elements[index];
            float elementSpan = state.FlexBasis;

            if (Wrap == FlexWrap.Wrap && currentLineSpan + elementSpan + (currentLine.Count > 0 ? Gap : 0f) > 1f)
            {
                _lines.Add(currentLine);
                currentLine = UiFrameworkPool.GetList<LayoutState>();
                currentLineSpan = 0f;
            }

            currentLine.Add(state);
            currentLineSpan += elementSpan + (currentLine.Count > 1 ? Gap : 0f);
        }

        if (currentLine.Count > 0)
        {
            _lines.Add(currentLine);
        }
        else
        {
            UiFrameworkPool.FreeList(currentLine);
        }
    }

    private static float CalculateJustifyContentOffset(float availableSpace, int elementCount, FlexJustifyContent justifyContent)
    {
        return justifyContent switch
        {
            FlexJustifyContent.Start => 0f,
            FlexJustifyContent.End => availableSpace,
            FlexJustifyContent.Center => availableSpace / 2f,
            FlexJustifyContent.SpaceBetween => elementCount > 1 ? 0f : availableSpace,
            FlexJustifyContent.SpaceAround => availableSpace / (elementCount * 2),
            FlexJustifyContent.SpaceEvenly => availableSpace / (elementCount + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(justifyContent))
        };
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        for (int index = 0; index < _lines.Count; index++)
        {
            UiFrameworkPool.FreeList(_lines[index]);
        }

        _lines.Clear();
        
        foreach (List<int> indices in _groups.Values)
        {
            UiFrameworkPool.FreeList(indices);
        }
        
        _groups.Clear();
    }

    public readonly struct LayoutState(BaseUiComponent element, float flexBasis, float flexGrow, float flexShrink, FlexJustifyContent justifyContent)
    {
        public readonly BaseUiComponent Element = element;
        public readonly float FlexBasis = flexBasis;
        public readonly float FlexGrow = flexGrow;
        public readonly float FlexShrink = flexShrink;
        public readonly FlexJustifyContent JustifyContent = justifyContent;
    }
}