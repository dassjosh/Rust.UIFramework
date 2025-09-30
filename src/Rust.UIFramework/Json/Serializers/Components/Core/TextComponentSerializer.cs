using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class TextComponentSerializer : TextComponentSerializer<TextComponent>;

public abstract class TextComponentSerializer<T> : CoreComponentSerializer<T> where T : TextComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.AddTextField(JsonDefaults.Text.TextName, component.Text, defaults.Text);
        writer.AddField(JsonDefaults.Text.FontSizeName, component.FontSize, defaults.FontSize);
        writer.AddField(JsonDefaults.Text.FontName, component.Font, defaults.Font);
        writer.AddField(JsonDefaults.Text.AlignName, component.Align, defaults.Align);
        writer.AddField(JsonDefaults.Text.VerticalOverflowName, component.VerticalOverflow, defaults.VerticalOverflow);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color, defaults.Color);
        
        if (component.PlaceholderFor.IsValidReference())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, component.PlaceholderFor.Name);
        }
    }
}