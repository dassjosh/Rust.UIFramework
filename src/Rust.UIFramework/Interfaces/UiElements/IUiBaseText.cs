using Oxide.Ext.UiFramework.Components;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

[IncludeInParent]
public interface IUiBaseText : IBaseUiComponent
{
    int FontSize { get; set; }
    string Font { get; set; }
    TextAnchor Align { get; set; }
    [PropertyName(nameof(TextComponent.Text))]
    string TextValue { get; set; }
    VerticalWrapMode VerticalOverflow { get; set; }
}