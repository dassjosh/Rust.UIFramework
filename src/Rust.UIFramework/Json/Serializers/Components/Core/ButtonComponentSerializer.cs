using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ButtonComponentSerializer : CoreComponentSerializer<ButtonComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, ButtonComponent component, ButtonComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, component.Sprite, defaults.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, component.Material, defaults.Material);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color, defaults.Color);
        writer.AddField(JsonDefaults.Image.ImageTypeName, component.ImageType, defaults.ImageType);
        switch (component.ButtonType)
        {
            case ButtonType.Command:
                writer.AddCommand(JsonDefaults.Common.CommandName, component.Command, defaults.Command);
                break;
            case ButtonType.Close:
                writer.AddField(JsonDefaults.Button.CloseName, component.Command, defaults.Command);
                break;
        }

        if (component.ColorBlock != null)
        {
            UiFrameworkSerializer.Serialize(writer, component.ColorBlock, defaults.ColorBlock, mode);
        }
    }
}