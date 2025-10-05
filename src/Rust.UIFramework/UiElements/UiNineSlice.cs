using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiNineSlice : BaseUiImage<UiNineSlice>
{
    public string Png { get => Image.Png; set => Image.Png = value; }
    public UiBorderWidth Slice { get => Image.Slice; set => Image.Slice = value; }
    
    public readonly NineSliceComponent Image;
    
    public UiNineSlice() : this(new NineSliceComponent()) { }
    
    private UiNineSlice(NineSliceComponent component) : base(component)
    {
        Image = component;
    }
    
    public UiNineSlice Init(string png, in UiBorderWidth slice, bool fillCenter, UiColor color, Image.Type type)
    {
        Png = png;
        Slice = slice;
        FillCenter = fillCenter;
        Color = color;
        ImageType = type;
        return this;
    }

    public UiNineSlice SetPng(string png)
    {
        Png = png;
        return this;
    }
    
    public UiNineSlice SetSlice(in UiBorderWidth slice)
    {
        Slice = slice;
        return this;
    }
    
    public UiNineSlice SetFillCenter(bool fillCenter)
    {
        FillCenter = fillCenter;
        return this;
    }
}