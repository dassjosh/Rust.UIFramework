using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ITextComponent))]
[GenerateBuilderMethods]
public partial class TextComponent : CoreComponent, IGraphicalComponent, ITextComponent
{
    public override Utf8String Type => JsonDefaults.Text.Type;
    public override ComponentType ComponentType => ComponentType.Text;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddTextField(JsonDefaults.Text.TextName, _text, mode);
        writer.AddField(JsonDefaults.Text.FontSizeName, _fontSize, mode);
        writer.AddField(JsonDefaults.Text.FontName, _font, mode);
        writer.AddField(JsonDefaults.Text.AlignName, _align, mode);
        writer.AddField(JsonDefaults.Text.VerticalOverflowName, _verticalOverflow, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
        
        if (_placeholderFor.ShouldSerialize(mode) && PlaceholderFor.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
}