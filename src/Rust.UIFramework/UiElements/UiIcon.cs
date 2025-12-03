using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiIcon : BaseUiComponent, IMaterial<UiIcon>, IFadeIn<UiIcon>, IUiColor<UiIcon>
{
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
    public readonly RawImageComponent RawImage;
    
    public UiIcon() : this(new RawImageComponent()) { }

    private UiIcon(RawImageComponent component) : base(component)
    {
        RawImage = component;
    }
        
    public UiIcon Init<T>(T icon, UiColor color) where T : struct, Enum
    {
        Color = color;
        SetIcon(icon);
        return this;
    }
    
    public UiIcon SetIcon<T>(T icon) where T : struct, Enum
    {
        Singleton<UiIconLib>.Instance.PopulateIconData(this, icon);
        return this;
    }
}