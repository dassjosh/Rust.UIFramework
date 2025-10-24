using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

[IncludeInParent]
public interface IUiBaseImage : IBaseUiComponent
{
    UiReference PlaceholderFor { get; set; }
    bool FillCenter { get; set; }
}