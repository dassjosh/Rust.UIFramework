using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ImageComponentSerializer : ImageComponentSerializer<ImageComponent>;

public abstract class ImageComponentSerializer<T> : CoreComponentSerializer<T> where T : ImageComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, component.Sprite, defaults.Sprite);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, component.Material, defaults.Material);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color, defaults.Color);
        writer.AddField(JsonDefaults.Image.ImageTypeName, component.ImageType, defaults.ImageType);
        writer.AddField(JsonDefaults.Image.FillCenterName, component.FillCenter, defaults.FillCenter);
        if (component.PlaceholderFor.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, component.PlaceholderFor.Name);
        }
    }
}